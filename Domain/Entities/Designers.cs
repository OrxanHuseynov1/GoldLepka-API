using Domain.Entities.Common;

namespace Domain.Entities;

public class Designer : BaseEntity
{
    public string ImageUrl { get; set; }

    public string Name { get; set; }

    // Tərcümələr
    public string? Name_en { get; set; }
    public string? Name_ru { get; set; }
    public string? Name_ar { get; set; }

    public List<string> WorksImage { get; set; } = [];
}
