using Microsoft.AspNetCore.Http;

namespace Application.DTOs;

public class DesignerGetDto
{
    public Guid Id { get; set; }

    public string ImageUrl { get; set; }
    public string Name { get; set; }

    public string? Name_en { get; set; }
    public string? Name_ru { get; set; }
    public string? Name_ar { get; set; }

    public List<string> WorksImage { get; set; }
}

public class DesignerPostDto
{
    public IFormFile ImageFile { get; set; }  // Dizaynerin şəkli
    public string Name { get; set; }          // Azərbaycanca ad

    public List<IFormFile> WorksImageFiles { get; set; } = new(); // iş şəkilləri
}


    public class DesignerPutDto
{
    public IFormFile? ImageFile { get; set; }
    public string Name { get; set; }

    public List<IFormFile>? WorksImageFiles { get; set; }
}