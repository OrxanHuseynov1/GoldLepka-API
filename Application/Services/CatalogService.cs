using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using DAL.SqlServer.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services;

public class CatalogService : ICatalogService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly ITranslationService _translationService;

    public CatalogService(AppDbContext context, IMapper mapper, ITranslationService translationService)
    {
        _context = context;
        _mapper = mapper;
        _translationService = translationService;
    }

    private async Task<Dictionary<string, string>> GetNameTranslationsAsync(string azText)
    {
        var targetLangs = new List<string> { "en", "ru", "ar" };
        return await _translationService.TranslateTextAsync(azText, targetLangs);
    }


    public async Task<List<CatalogGetDto>> GetAllAsync()
    {
        var catalogs = await _context.Catalogs.ToListAsync();
        return _mapper.Map<List<CatalogGetDto>>(catalogs);
    }

    public async Task<CatalogGetDto> GetByIdAsync(Guid id)
    {
        var catalog = await _context.Catalogs.FindAsync(id);
        return catalog == null ? null : _mapper.Map<CatalogGetDto>(catalog);
    }

    public async Task<CatalogGetDto> CreateAsync(CatalogPostDto dto)
    {
        var translations = await GetNameTranslationsAsync(dto.Name);

        var catalog = _mapper.Map<Catalog>(dto);

        catalog.Name_en = translations.GetValueOrDefault("en");
        catalog.Name_ru = translations.GetValueOrDefault("ru");
        catalog.Name_ar = translations.GetValueOrDefault("ar");

        _context.Catalogs.Add(catalog);
        await _context.SaveChangesAsync();
        return _mapper.Map<CatalogGetDto>(catalog);
    }

    public async Task<CatalogGetDto> UpdateAsync(Guid id, CatalogPutDto dto)
    {
        var catalog = await _context.Catalogs.FindAsync(id);
        if (catalog == null) return null;

        if (catalog.Name != dto.Name)
        {
            var translations = await GetNameTranslationsAsync(dto.Name);

            catalog.Name_en = translations.GetValueOrDefault("en");
            catalog.Name_ru = translations.GetValueOrDefault("ru");
            catalog.Name_ar = translations.GetValueOrDefault("ar");
        }

        // DÜZƏLİŞ: Id-ni artıq ignore etdiyimiz üçün, map etmə əməliyyatı indi təhlükəsizdir
        _mapper.Map(dto, catalog);

        await _context.SaveChangesAsync();
        return _mapper.Map<CatalogGetDto>(catalog);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var catalog = await _context.Catalogs.FindAsync(id);
        if (catalog == null) return false;

        _context.Catalogs.Remove(catalog);
        await _context.SaveChangesAsync();
        return true;
    }
}