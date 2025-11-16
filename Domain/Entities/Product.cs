using Domain.Entities.Common;

namespace Domain.Entities;

public class Product : BaseEntity
{
    public List<string> ImageUrls { get; set; } = [];

    // Azərbaycan Dili (Əsas Dil)
    public string Name { get; set; }
    public string MaterialName { get; set; }

    // Tərcümə Olunmuş Sahələr
    public string? Name_en { get; set; }
    public string? Name_ru { get; set; }
    public string? Name_ar { get; set; }

    public string? MaterialName_en { get; set; }
    public string? MaterialName_ru { get; set; }
    public string? MaterialName_ar { get; set; }

    public string Code { get; set; }
    public int Width { get; set; } //sm
    public int Height { get; set; } // sm

    // YENİ: Kataloq Əlaqəsi (Foreign Key)
    public Guid CatalogId { get; set; }
    public Catalog Catalog { get; set; } // Navigation Property
}