using Microsoft.AspNetCore.Http;

namespace Application.DTOs;

// GET üçün DTO
public class ServiceGetDto
{
    public Guid Id { get; set; }

    public string ImageUrl { get; set; }

    // AZ Mətn (Əsas)
    public string Name { get; set; }
    public string About { get; set; }

    // Tərcümə Olunmuş Mətn Sahələri (Nullable)
    public string? Name_en { get; set; }
    public string? Name_ru { get; set; }
    public string? Name_ar { get; set; }

    public string? About_en { get; set; }
    public string? About_ru { get; set; }
    public string? About_ar { get; set; }
}

// POST üçün DTO
public class ServicePostDto
{
    public IFormFile ImageFile { get; set; }  // Fayl ötürmə
    public string Name { get; set; }          // AZ Mətn
    public string About { get; set; }         // AZ Mətn
}

// PUT üçün DTO
public class ServicePutDto
{
    public Guid Id { get; set; }

    // Fayl yenilənirsə IFormFile, yoxsa null
    public IFormFile? ImageFile { get; set; }

    public string Name { get; set; }   // AZ Mətn
    public string About { get; set; }  // AZ Mətn
}