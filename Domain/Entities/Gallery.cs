using Domain.Entities.Common;

namespace Domain.Entities;

public class Gallery : BaseEntity
{
    public string ImageUrl { get; set; }

    // Azərbaycan Dili (Əsas Dil)
    public string Caption { get; set; }

    public string? Caption_en { get; set; } // English
    public string? Caption_ru { get; set; } // Russian
    public string? Caption_ar { get; set; } // Arabic
}