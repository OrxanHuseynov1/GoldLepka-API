using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GoldLepka.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CatalogsController : ControllerBase
{
    private readonly ICatalogService _service;

    public CatalogsController(ICatalogService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<CatalogGetDto>>> GetAll()
    {
        var catalogs = await _service.GetAllAsync();
        return Ok(catalogs);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CatalogGetDto>> GetById(Guid id)
    {
        var catalog = await _service.GetByIdAsync(id);
        if (catalog == null) return NotFound();
        return Ok(catalog);
    }

    [HttpPost]
    public async Task<ActionResult<CatalogGetDto>> Create(CatalogPostDto dto)
    {
        // Tərcümə Service-də avtomatik icra edilir
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CatalogGetDto>> Update(Guid id, CatalogPutDto dto)
    {
        // Tərcümə Service-də avtomatik yenilənir
        var updated = await _service.UpdateAsync(id, dto);
        if (updated == null) return NotFound();
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await _service.DeleteAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
}