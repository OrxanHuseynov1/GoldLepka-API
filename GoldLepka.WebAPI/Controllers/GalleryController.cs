using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GoldLepka.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GalleryController : ControllerBase
{
    private readonly IGalleryService _galleryService;

    public GalleryController(IGalleryService galleryService)
    {
        _galleryService = galleryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _galleryService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _galleryService.GetByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    // FAYL DTO İÇİNDƏ GƏLDİYİNƏ GÖRƏ, İNDİ SADƏCƏ DTO-NU QƏBUL EDİRİK
    public async Task<IActionResult> Create([FromForm] GalleryPostDto dto)
    {
        if (dto.ImageFile == null) return BadRequest("Şəkil faylı əlavə edilməlidir.");
        await _galleryService.CreateAsync(dto);
        return Ok("Successfully created");
    }

    [HttpPut("{id}")]
    // FAYL DTO İÇİNDƏ GƏLDİYİNƏ GÖRƏ, SADƏCƏ DTO-NU QƏBUL EDİRİK
    public async Task<IActionResult> Update(Guid id, [FromForm] GalleryPutDto dto)
    {
        // ImageFile null ola bilər, Service bunu idarə edir.
        await _galleryService.UpdateAsync(dto);
        return Ok("Successfully updated");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _galleryService.DeleteAsync(id);
        return NoContent();
    }
}