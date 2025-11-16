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

public class ContactService : IContactService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly ITranslationService _translationService; // Python Service-imiz DI vasitəsilə gəlir

    public ContactService(AppDbContext context, IMapper mapper, ITranslationService translationService)
    {
        _context = context;
        _mapper = mapper;
        _translationService = translationService;
    }

    // YENİ: Tərcüməni yerinə yetirən köməkçi metod
    private async Task<Dictionary<string, string>> GetAdressTextTranslationsAsync(string azText)
    {
        var targetLangs = new List<string> { "en", "ru", "ar" };
        // Tərcümə olunmuş bütün dilləri qaytarır
        return await _translationService.TranslateTextAsync(azText, targetLangs);
    }

    public async Task<List<ContactGetDto>> GetAllAsync()
    {
        var contacts = await _context.Contacts.Where(c => !c.IsDeleted).ToListAsync();
        return _mapper.Map<List<ContactGetDto>>(contacts);
    }

    public async Task<ContactGetDto> GetByIdAsync(Guid id)
    {
        var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
        return _mapper.Map<ContactGetDto>(contact);
    }

    public async Task<ContactGetDto> CreateAsync(ContactPostDto dto)
    {
        // 1. Tərcüməni İcra Et
        var translations = await GetAdressTextTranslationsAsync(dto.ShopAdressText);

        var contact = _mapper.Map<Contact>(dto);

        // 2. Tərcümə olunmuş dəyərləri entity-yə yaz
        contact.ShopAdressText_en = translations.GetValueOrDefault("en");
        contact.ShopAdressText_ru = translations.GetValueOrDefault("ru");
        contact.ShopAdressText_ar = translations.GetValueOrDefault("ar");

        // NULL dəyərlər gələrsə, string? sahələrə yazılacaq (bu, db error-un qarşısını alır)

        _context.Contacts.Add(contact);
        await _context.SaveChangesAsync();
        return _mapper.Map<ContactGetDto>(contact);
    }

    public async Task<ContactGetDto> UpdateAsync(Guid id, ContactPutDto dto)
    {
        var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
        if (contact == null) return null;

        // Tərcüməni yalnız mətn dəyişibsə və ya təzədirsə yenidən icra edirik
        if (contact.ShopAdressText != dto.ShopAdressText)
        {
            var translations = await GetAdressTextTranslationsAsync(dto.ShopAdressText);

            // Tərcümə Olunmuş Dəyərləri yenilə
            contact.ShopAdressText_en = translations.GetValueOrDefault("en");
            contact.ShopAdressText_ru = translations.GetValueOrDefault("ru");
            contact.ShopAdressText_ar = translations.GetValueOrDefault("ar");
        }

        // Məlum DTO dəyərlərini map edirik (AutoMapper digər sahələri də yeniləyir)
        _mapper.Map(dto, contact);

        // Qeyd: Yuxarıdakı Map metodundan sonra, biz hələ də contact.ShopAdressText sahəsini
        // xüsusi olaraq yeniləməliyik ki, DB-yə düzgün dəyər getsin.
        contact.ShopAdressText = dto.ShopAdressText;

        await _context.SaveChangesAsync();
        return _mapper.Map<ContactGetDto>(contact);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
        if (contact == null) return false;
        contact.IsDeleted = true;
        await _context.SaveChangesAsync();
        return true;
    }
}