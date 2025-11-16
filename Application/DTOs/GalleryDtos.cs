using Microsoft.AspNetCore.Http;

namespace Application.DTOs;

public class GalleryGetDto
{
    public Guid Id { get; set; }
    public string ImageUrl { get; set; }

    // AZ Mətn (Əsas)
    public string Caption { get; set; }

    // YENİ: Tərcümə Olunmuş Mətn Sahələri (Nullable)
    public string? Caption_en { get; set; }
    public string? Caption_ru { get; set; }
    public string? Caption_ar { get; set; }
}

public class GalleryPostDto
{
    // Fayl ötürməyi asanlaşdırmaq üçün IFormFile istifadə etmək daha yaxşıdır
    public IFormFile ImageFile { get; set; }
    public string Caption { get; set; } // AZ Mətn post edilir
}

public class GalleryPutDto
{
    public Guid Id { get; set; }
    // Əgər şəkil yenilənmirsə, bu null ola bilər, yenilənirsə IFormFile olmalıdır.
    public IFormFile? ImageFile { get; set; }
    public string Caption { get; set; } // AZ Mətn put edilir
}