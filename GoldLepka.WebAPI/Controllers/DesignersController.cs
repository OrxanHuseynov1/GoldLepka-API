using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GoldLepka.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DesignersController : ControllerBase
{
    private readonly IDesignerService _designerService;
    private readonly IWebHostEnvironment _env;

    public DesignersController(IDesignerService designerService, IWebHostEnvironment env)
    {
        _designerService = designerService;
        _env = env;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var designers = await _designerService.GetAllAsync();
        return Ok(designers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var designer = await _designerService.GetByIdAsync(id);
        if (designer == null) return NotFound();
        return Ok(designer);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] DesignerPostDto dto)
    {
        if (dto.ImageFile == null)
            return BadRequest("Dizaynerin öz şəkli əlavə edilməlidir.");

        var designerFolder = Path.Combine(_env.WebRootPath, "uploads/designers");
        var worksFolder = Path.Combine(_env.WebRootPath, "uploads/designers/works");

        var designer = await _designerService.CreateAsync(dto, designerFolder, worksFolder);

        return Ok(designer);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromForm] DesignerPutDto dto)
    {
        var designerFolder = Path.Combine(_env.WebRootPath, "uploads/designers");
        var worksFolder = Path.Combine(_env.WebRootPath, "uploads/designers/works");

        var updated = await _designerService.UpdateAsync(id, dto, designerFolder, worksFolder);
        if (updated == null) return NotFound();

        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _designerService.DeleteAsync(id);
        if (!deleted) return NotFound();

        return NoContent();
    }
}
