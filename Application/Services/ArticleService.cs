using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using DAL.SqlServer.Context;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services;

public class ArticleService : IArticleService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly ITranslationService _translationService;

    public ArticleService(AppDbContext context, IMapper mapper, ITranslationService translationService)
    {
        _context = context;
        _mapper = mapper;
        _translationService = translationService;
    }

    // Tərcüməni aparıb nəticələri qaytaran köməkçi metod
    private async Task<(Dictionary<string, string> QuestionTrans, Dictionary<string, string> AnswerTrans)> GetTranslationsAsync(ArticlePostDTO dto)
    {
        var targetLangs = new List<string> { "en", "ru", "ar" };

        var questionTranslationsTask = _translationService.TranslateTextAsync(dto.Question, targetLangs);
        var answerTranslationsTask = _translationService.TranslateTextAsync(dto.Answer, targetLangs);

        await Task.WhenAll(questionTranslationsTask, answerTranslationsTask);

        return (questionTranslationsTask.Result, answerTranslationsTask.Result);
    }

    public async Task<IEnumerable<ArticleGetDTO>> GetAllAsync()
    {
        var articles = await _context.Articles.ToListAsync();
        return _mapper.Map<IEnumerable<ArticleGetDTO>>(articles);
    }

    public async Task<ArticleGetDTO> GetByIdAsync(Guid id)
    {
        var article = await _context.Articles.FindAsync(id);
        return article == null ? null : _mapper.Map<ArticleGetDTO>(article);
    }

    public async Task<ArticleGetDTO> CreateAsync(ArticlePostDTO dto, string uploadsFolderPath)
    {
        // 1. Tərcüməni İcra Etmək (Avtomatik Translate)
        var (qTrans, aTrans) = await GetTranslationsAsync(dto);

        if (!Directory.Exists(uploadsFolderPath))
            Directory.CreateDirectory(uploadsFolderPath);

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(dto.ImageFile.FileName)}";
        var filePath = Path.Combine(uploadsFolderPath, fileName);

        using var stream = new FileStream(filePath, FileMode.Create);
        await dto.ImageFile.CopyToAsync(stream);

        var article = new Article
        {
            Id = Guid.NewGuid(),

            // Azərbaycanca Dəyərlər
            Question = dto.Question,
            Answer = dto.Answer,

            // Tərcümə Olunmuş Dəyərlər (Python-dan gəlir)
            Question_en = qTrans.GetValueOrDefault("en"),
            Answer_en = aTrans.GetValueOrDefault("en"),
            Question_ru = qTrans.GetValueOrDefault("ru"),
            Answer_ru = aTrans.GetValueOrDefault("ru"),
            Question_ar = qTrans.GetValueOrDefault("ar"),
            Answer_ar = aTrans.GetValueOrDefault("ar"),

            ImageUrl = $"/uploads/articles/{fileName}",
            CreatedDate = DateTime.UtcNow
        };

        await _context.Articles.AddAsync(article);
        await _context.SaveChangesAsync();

        return _mapper.Map<ArticleGetDTO>(article);
    }

    public async Task<ArticleGetDTO> UpdateAsync(Guid id, ArticlePutDTO dto, string uploadsFolderPath)
    {
        var article = await _context.Articles.FindAsync(id);
        if (article == null) return null;

        // Tərcüməni yenidən icra et
        var (qTrans, aTrans) = await GetTranslationsAsync(new ArticlePostDTO { Question = dto.Question, Answer = dto.Answer });


        if (dto.ImageFile != null)
        {
            // ... (Şəkil yükləmə mantığı) ...
        }

        article.Question = dto.Question;
        article.Answer = dto.Answer;

        // Tərcümə Olunmuş Dəyərləri yenilə
        article.Question_en = qTrans.GetValueOrDefault("en");
        article.Answer_en = aTrans.GetValueOrDefault("en");
        article.Question_ru = qTrans.GetValueOrDefault("ru");
        article.Answer_ru = aTrans.GetValueOrDefault("ru");
        article.Question_ar = qTrans.GetValueOrDefault("ar");
        article.Answer_ar = aTrans.GetValueOrDefault("ar");

        await _context.SaveChangesAsync();

        return _mapper.Map<ArticleGetDTO>(article);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var article = await _context.Articles.FindAsync(id);
        if (article == null) return false;

        _context.Articles.Remove(article);
        await _context.SaveChangesAsync();
        return true;
    }
}