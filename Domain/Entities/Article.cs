using Domain.Entities.Common;

namespace Domain.Entities;

public class Article : BaseEntity
{
    public string ImageUrl { get; set; }

    // Azərbaycanca (Əsas Dil)
    public string Question { get; set; }
    public string Answer { get; set; }

    public string? Question_en { get; set; } // English
    public string? Answer_en { get; set; }

    public string? Question_ru { get; set; } // Russian
    public string? Answer_ru { get; set; }

    public string? Question_ar { get; set; } // Arabic
    public string? Answer_ar { get; set; }

    public DateTime CreatedDate { get; set; }
}