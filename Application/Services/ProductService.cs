using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using DAL.SqlServer.Context;
using Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class ProductService : IProductService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly ITranslationService _translationService;
    private readonly IWebHostEnvironment _env;

    public ProductService(AppDbContext context, IMapper mapper, ITranslationService translationService, IWebHostEnvironment env)
    {
        _context = context;
        _mapper = mapper;
        _translationService = translationService;
        _env = env;
    }

    // Tərcümə köməkçisi (Eyni qalır)
    private async Task<(Dictionary<string, string> NameTrans, Dictionary<string, string> MaterialTrans)> GetProductTranslationsAsync(string name, string material)
    {
        var targetLangs = new List<string> { "en", "ru", "ar" };

        var nameTransTask = _translationService.TranslateTextAsync(name, targetLangs);
        var materialTransTask = _translationService.TranslateTextAsync(material, targetLangs);

        await Task.WhenAll(nameTransTask, materialTransTask);

        return (nameTransTask.Result, materialTransTask.Result);
    }

    // Faylları silən köməkçi metod (Eyni qalır)
    private void DeleteFile(string relativePath)
    {
        if (string.IsNullOrEmpty(relativePath)) return;

        var fullPath = Path.Combine(_env.WebRootPath, relativePath.TrimStart('/'));

        if (File.Exists(fullPath))
        {
            try
            {
                File.Delete(fullPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting file {fullPath}: {ex.Message}");
            }
        }
    }

    // Faylları yükləyən köməkçi metod (Eyni qalır)
    private async Task<List<string>> UploadFilesAsync(List<IFormFile> files)
    {
        var urls = new List<string>();
        string uploadsFolder = Path.Combine(_env.WebRootPath, "uploads/products");
        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        foreach (var file in files)
        {
            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            urls.Add($"/uploads/products/{uniqueFileName}");
        }
        return urls;
    }


    // YENİLƏNİB: Kataloq məlumatını daxil edirik
    public async Task<List<ProductGetDto>> GetAllAsync()
    {
        var products = await _context.Products
            .Include(p => p.Catalog) // Kataloq məlumatını yüklə
            .ToListAsync();

        return _mapper.Map<List<ProductGetDto>>(products);
    }

    // YENİLƏNİB: Kataloq məlumatını daxil edirik
    public async Task<ProductGetDto> GetByIdAsync(Guid id)
    {
        var product = await _context.Products
            .Include(p => p.Catalog) // Kataloq məlumatını yüklə
            .FirstOrDefaultAsync(p => p.Id == id);

        return product == null ? null : _mapper.Map<ProductGetDto>(product);
    }

    // CreateAsync (Eyni qalır, CatalogId DTO-dan gəlir)
    public async Task<ProductGetDto> CreateAsync(ProductPostDto dto)
    {
        var (nTrans, mTrans) = await GetProductTranslationsAsync(dto.Name, dto.MaterialName);

        var imageUrls = await UploadFilesAsync(dto.ImageFiles);

        var product = _mapper.Map<Product>(dto);
        product.ImageUrls = imageUrls;

        product.Name_en = nTrans.GetValueOrDefault("en");
        product.Name_ru = nTrans.GetValueOrDefault("ru");
        product.Name_ar = nTrans.GetValueOrDefault("ar");

        product.MaterialName_en = mTrans.GetValueOrDefault("en");
        product.MaterialName_ru = mTrans.GetValueOrDefault("ru");
        product.MaterialName_ar = mTrans.GetValueOrDefault("ar");

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        // Yeni yaratdığımız məhsulu kataloq məlumatı ilə yükləyirik
        var createdProduct = await _context.Products.Include(p => p.Catalog).FirstOrDefaultAsync(p => p.Id == product.Id);

        return _mapper.Map<ProductGetDto>(createdProduct);
    }

    // UpdateAsync (Eyni qalır, əlavə Include etmək lazım deyil)
    public async Task<ProductGetDto> UpdateAsync(Guid id, ProductPutDto dto)
    {
        var product = await _context.Products.Include(p => p.Catalog).FirstOrDefaultAsync(p => p.Id == id);
        if (product == null) return null;

        bool requiresTranslation = product.Name != dto.Name || product.MaterialName != dto.MaterialName;

        if (requiresTranslation)
        {
            var (nTrans, mTrans) = await GetProductTranslationsAsync(dto.Name, dto.MaterialName);

            product.Name_en = nTrans.GetValueOrDefault("en");
            product.Name_ru = nTrans.GetValueOrDefault("ru");
            product.Name_ar = nTrans.GetValueOrDefault("ar");

            product.MaterialName_en = mTrans.GetValueOrDefault("en");
            product.MaterialName_ru = mTrans.GetValueOrDefault("ru");
            product.MaterialName_ar = mTrans.GetValueOrDefault("ar");
        }

        // Fayl Silinməsi Mantığı (Update)
        if (dto.ImagesToDelete != null && dto.ImagesToDelete.Any())
        {
            foreach (var urlToDelete in dto.ImagesToDelete)
            {
                DeleteFile(urlToDelete);
                product.ImageUrls.Remove(urlToDelete);
            }
        }

        // Yeni Fayl Yüklənməsi
        if (dto.NewImageFiles != null && dto.NewImageFiles.Any())
        {
            var newUrls = await UploadFilesAsync(dto.NewImageFiles);
            product.ImageUrls.AddRange(newUrls);
        }

        _mapper.Map(dto, product);

        // Kataloq ID-sinin yenilənməsini təmin edir
        product.CatalogId = dto.CatalogId;

        await _context.SaveChangesAsync();

        // Yenilənmiş məhsulun kataloq məlumatını daxil edirik
        var updatedProduct = await _context.Products.Include(p => p.Catalog).FirstOrDefaultAsync(p => p.Id == id);
        return _mapper.Map<ProductGetDto>(updatedProduct);
    }

    // DeleteAsync (Eyni qalır)
    public async Task<bool> DeleteAsync(Guid id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return false;

        foreach (var url in product.ImageUrls.ToList())
        {
            DeleteFile(url);
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }
}