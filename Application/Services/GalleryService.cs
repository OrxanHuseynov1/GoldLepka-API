using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using DAL.SqlServer.Context;
using Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services;

public class GalleryService : IGalleryService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _env;
    private readonly ITranslationService _translationService; // Python Service

    public GalleryService(AppDbContext context, IMapper mapper, IWebHostEnvironment env, ITranslationService translationService)
    {
        _context = context;
        _mapper = mapper;
        _env = env;
        _translationService = translationService;
    }

    private async Task<Dictionary<string, string>> GetCaptionTranslationsAsync(string azCaption)
    {
        var targetLangs = new List<string> { "en", "ru", "ar" };
        return await _translationService.TranslateTextAsync(azCaption, targetLangs);
    }


    public async Task<List<GalleryGetDto>> GetAllAsync()
    {
        var items = await _context.Galleries.ToListAsync();
        return _mapper.Map<List<GalleryGetDto>>(items);
    }

    public async Task<GalleryGetDto> GetByIdAsync(Guid id)
    {
        var item = await _context.Galleries.FindAsync(id);
        if (item == null) throw new Exception("Gallery item not found");
        return _mapper.Map<GalleryGetDto>(item);
    }

    // CREATE: Metod imzası dəyişdi
    public async Task CreateAsync(GalleryPostDto dto)
    {
        // 1. Tərcüməni İcra Et
        var translations = await GetCaptionTranslationsAsync(dto.Caption);

        // 2. Faylı Yüklə (dto.ImageFile mövcud olduğu güman edilir)
        if (dto.ImageFile == null) throw new ArgumentNullException(nameof(dto.ImageFile), "Şəkil faylı boş ola bilməz.");

        string uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.ImageFile.FileName);
        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await dto.ImageFile.CopyToAsync(stream); // DTO-dan faylı kopyala
        }

        var entity = new Gallery
        {
            ImageUrl = "/uploads/" + uniqueFileName,
            Caption = dto.Caption,
            Caption_en = translations.GetValueOrDefault("en"),
            Caption_ru = translations.GetValueOrDefault("ru"),
            Caption_ar = translations.GetValueOrDefault("ar")
        };

        await _context.Galleries.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    // UPDATE: Metod imzası dəyişdi
    public async Task UpdateAsync(GalleryPutDto dto)
    {
        var entity = await _context.Galleries.FindAsync(dto.Id);
        if (entity == null) throw new Exception("Gallery item not found");

        // Tərcüməni yalnız mətn dəyişibsə yenidən icra et
        if (entity.Caption != dto.Caption)
        {
            var translations = await GetCaptionTranslationsAsync(dto.Caption);
            entity.Caption_en = translations.GetValueOrDefault("en");
            entity.Caption_ru = translations.GetValueOrDefault("ru");
            entity.Caption_ar = translations.GetValueOrDefault("ar");
        }

        // Fayl yenilənməsi: DTO daxilindən faylı yoxla
        if (dto.ImageFile != null)
        {
            string uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.ImageFile.FileName);
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await dto.ImageFile.CopyToAsync(stream); // DTO-dan faylı kopyala
            }

            if (!string.IsNullOrEmpty(entity.ImageUrl))
            {
                var oldPath = Path.Combine(_env.WebRootPath, entity.ImageUrl.TrimStart('/'));
                if (File.Exists(oldPath)) File.Delete(oldPath);
            }

            entity.ImageUrl = "/uploads/" + uniqueFileName;
        }

        entity.Caption = dto.Caption;

        _context.Galleries.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _context.Galleries.FindAsync(id);
        if (entity == null) throw new Exception("Gallery item not found");

        if (!string.IsNullOrEmpty(entity.ImageUrl))
        {
            var oldPath = Path.Combine(_env.WebRootPath, entity.ImageUrl.TrimStart('/'));
            if (File.Exists(oldPath)) File.Delete(oldPath);
        }

        _context.Galleries.Remove(entity);
        await _context.SaveChangesAsync();
    }
}