using Domain.Entities.Common;

namespace Domain.Entities;

public class Catalog : BaseEntity
{
    // Azərbaycan Dili (Əsas Dil)
    public string Name { get; set; }

    // Tərcümə Olunmuş Sahələr
    public string? Name_en { get; set; }
    public string? Name_ru { get; set; }
    public string? Name_ar { get; set; }

    public List<Product> Products { get; set; } = [];
}