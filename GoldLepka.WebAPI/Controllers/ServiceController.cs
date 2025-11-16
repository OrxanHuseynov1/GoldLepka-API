using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GoldLepka.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ServiceController : ControllerBase
{
    private readonly IServiceService _serviceService;

    public ServiceController(IServiceService serviceService)
    {
        _serviceService = serviceService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _serviceService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _serviceService.GetByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] ServicePostDto dto)
    {
        if (dto.ImageFile == null) return BadRequest("Şəkil faylı əlavə edilməlidir.");
        await _serviceService.CreateAsync(dto);
        return Ok("Successfully created");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromForm] ServicePutDto dto)
    {
        dto.Id = id; 
        await _serviceService.UpdateAsync(dto);
        return Ok("Successfully updated");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _serviceService.DeleteAsync(id);
        return NoContent();
    }
}
