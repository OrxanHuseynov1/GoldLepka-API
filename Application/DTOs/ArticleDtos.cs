using Microsoft.AspNetCore.Http;

namespace Application.DTOs;

public class ArticleGetDTO
{
    public Guid Id { get; set; }
    public string ImageUrl { get; set; }

    // Azərbaycanca
    public string Question { get; set; }
    public string Answer { get; set; }

    // Tərcümə Olunmuş (Yeni)
    public string Question_en { get; set; }
    public string Answer_en { get; set; }

    public string Question_ru { get; set; }
    public string Answer_ru { get; set; }

    public string Question_ar { get; set; }
    public string Answer_ar { get; set; }

    public DateTime CreatedDate { get; set; }
}

public class ArticlePostDTO
{
    public IFormFile ImageFile { get; set; }
    public string Question { get; set; } // AZ Mətn
    public string Answer { get; set; } // AZ Mətn
}

public class ArticlePutDTO
{
    public IFormFile ImageFile { get; set; }
    public string Question { get; set; } // AZ Mətn
    public string Answer { get; set; } // AZ Mətn
}