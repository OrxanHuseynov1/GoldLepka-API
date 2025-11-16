using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GoldLepka.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ArticlesController : ControllerBase
{
    private readonly IArticleService _articleService;
    private readonly IWebHostEnvironment _env;

    public ArticlesController(IArticleService articleService, IWebHostEnvironment env)
    {
        _articleService = articleService;
        _env = env;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var articles = await _articleService.GetAllAsync();
        return Ok(articles);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var article = await _articleService.GetByIdAsync(id);
        if (article == null) return NotFound();
        return Ok(article);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] ArticlePostDTO dto)
    {
        if (dto.ImageFile == null) return BadRequest("Şəkil əlavə edilməlidir.");
        var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads/articles");
        var article = await _articleService.CreateAsync(dto, uploadsFolder);
        return Ok(article);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromForm] ArticlePutDTO dto)
    {
        var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads/articles");
        var article = await _articleService.UpdateAsync(id, dto, uploadsFolder);
        if (article == null) return NotFound();
        return Ok(article);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _articleService.DeleteAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
}