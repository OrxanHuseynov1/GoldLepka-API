using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using DAL.SqlServer.Context;
using Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Application.Services;

public class ServiceService : IServiceService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _env;
    private readonly ITranslationService _translationService;

    public ServiceService(AppDbContext context, IMapper mapper, IWebHostEnvironment env, ITranslationService translationService)
    {
        _context = context;
        _mapper = mapper;
        _env = env;
        _translationService = translationService;
    }

    private async Task<Dictionary<string, string>> GetNameAndAboutTranslationsAsync(string azName, string azAbout)
    {
        var targetLangs = new List<string> { "en", "ru", "ar" };
        var nameTranslations = await _translationService.TranslateTextAsync(azName, targetLangs);
        var aboutTranslations = await _translationService.TranslateTextAsync(azAbout, targetLangs);

        return new Dictionary<string, string>
        {
            { "Name_en", nameTranslations.GetValueOrDefault("en") },
            { "Name_ru", nameTranslations.GetValueOrDefault("ru") },
            { "Name_ar", nameTranslations.GetValueOrDefault("ar") },
            { "About_en", aboutTranslations.GetValueOrDefault("en") },
            { "About_ru", aboutTranslations.GetValueOrDefault("ru") },
            { "About_ar", aboutTranslations.GetValueOrDefault("ar") }
        };
    }

    public async Task<List<ServiceGetDto>> GetAllAsync()
    {
        var items = await _context.Services.ToListAsync();
        return _mapper.Map<List<ServiceGetDto>>(items);
    }

    public async Task<ServiceGetDto> GetByIdAsync(Guid id)
    {
        var item = await _context.Services.FindAsync(id);
        if (item == null) throw new Exception("Service not found");
        return _mapper.Map<ServiceGetDto>(item);
    }

    public async Task CreateAsync(ServicePostDto dto)
    {
        var translations = await GetNameAndAboutTranslationsAsync(dto.Name, dto.About);

        if (dto.ImageFile == null) throw new ArgumentNullException(nameof(dto.ImageFile));

        string uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
        if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

        string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.ImageFile.FileName);
        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await dto.ImageFile.CopyToAsync(stream);
        }

        var entity = new Service
        {
            ImageUrl = "/uploads/" + uniqueFileName,
            Name = dto.Name,
            Name_en = translations["Name_en"],
            Name_ru = translations["Name_ru"],
            Name_ar = translations["Name_ar"],
            About = dto.About,
            About_en = translations["About_en"],
            About_ru = translations["About_ru"],
            About_ar = translations["About_ar"]
        };

        await _context.Services.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ServicePutDto dto)
    {
        var entity = await _context.Services.FindAsync(dto.Id);
        if (entity == null) throw new Exception("Service not found");

        if (entity.Name != dto.Name || entity.About != dto.About)
        {
            var translations = await GetNameAndAboutTranslationsAsync(dto.Name, dto.About);
            entity.Name_en = translations["Name_en"];
            entity.Name_ru = translations["Name_ru"];
            entity.Name_ar = translations["Name_ar"];
            entity.About_en = translations["About_en"];
            entity.About_ru = translations["About_ru"];
            entity.About_ar = translations["About_ar"];
        }

        if (dto.ImageFile != null)
        {
            string uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.ImageFile.FileName);
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await dto.ImageFile.CopyToAsync(stream);
            }

            if (!string.IsNullOrEmpty(entity.ImageUrl))
            {
                var oldPath = Path.Combine(_env.WebRootPath, entity.ImageUrl.TrimStart('/'));
                if (File.Exists(oldPath)) File.Delete(oldPath);
            }

            entity.ImageUrl = "/uploads/" + uniqueFileName;
        }

        entity.Name = dto.Name;
        entity.About = dto.About;

        _context.Services.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _context.Services.FindAsync(id);
        if (entity == null) throw new Exception("Service not found");

        if (!string.IsNullOrEmpty(entity.ImageUrl))
        {
            var oldPath = Path.Combine(_env.WebRootPath, entity.ImageUrl.TrimStart('/'));
            if (File.Exists(oldPath)) File.Delete(oldPath);
        }

        _context.Services.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
