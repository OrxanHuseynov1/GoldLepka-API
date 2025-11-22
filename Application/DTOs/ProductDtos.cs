using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Application.DTOs;

public class ProductGetDto
{
    public Guid Id { get; set; }
    public List<string> ImageUrls { get; set; } = new List<string>();

    // AZ Mətn (Əsas)
    public string Name { get; set; }
    public string MaterialName { get; set; }

    // Tərcümə Olunmuş (Name)
    public string? Name_en { get; set; }
    public string? Name_ru { get; set; }
    public string? Name_ar { get; set; }

    // Tərcümə Olunmuş (MaterialName)
    public string? MaterialName_en { get; set; }
    public string? MaterialName_ru { get; set; }
    public string? MaterialName_ar { get; set; }

    public string Code { get; set; }
    public decimal Width { get; set; }
    public decimal Height { get; set; }

    public Guid CatalogId { get; set; }

    // YENİ: Kataloqun adını göstərmək üçün sahə (Mapping Profilində doldurulacaq)
    public string CatalogName { get; set; }
    // Kataloq adının tərcümə edilmiş formaları (opsiyonel)
    public string? CatalogName_en { get; set; }
    public string? CatalogName_ru { get; set; }
    public string? CatalogName_ar { get; set; }
}

public class ProductPostDto
{
    public List<IFormFile> ImageFiles { get; set; } = new List<IFormFile>();

    public string Name { get; set; }
    public string MaterialName { get; set; }

    public string Code { get; set; }
    public decimal Width { get; set; }
    public decimal Height { get; set; }

    public Guid CatalogId { get; set; }
}

public class ProductPutDto
{
    public Guid Id { get; set; }
    public List<IFormFile>? NewImageFiles { get; set; }

    public List<string>? ImagesToDelete { get; set; }

    public string Name { get; set; }
    public string MaterialName { get; set; }

    public string Code { get; set; }
    public decimal Width { get; set; }
    public decimal Height { get; set; }

    public Guid CatalogId { get; set; }
}