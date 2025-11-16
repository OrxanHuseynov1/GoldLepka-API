using Application.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces;

public interface IArticleService
{
    Task<IEnumerable<ArticleGetDTO>> GetAllAsync();
    Task<ArticleGetDTO> GetByIdAsync(Guid id);
    Task<ArticleGetDTO> CreateAsync(ArticlePostDTO dto, string uploadsFolderPath);
    Task<ArticleGetDTO> UpdateAsync(Guid id, ArticlePutDTO dto, string uploadsFolderPath);
    Task<bool> DeleteAsync(Guid id);
}
