namespace Application.DTOs;

public class CatalogGetDto
{
    public Guid Id { get; set; }
    // AZ Mətn (Əsas)
    public string Name { get; set; }

    // Tərcümə Olunmuş
    public string? Name_en { get; set; }
    public string? Name_ru { get; set; }
    public string? Name_ar { get; set; }

    public List<ProductGetDto> Products { get; set; } = []; // Product DTO istifadə olunur
}

public class CatalogPostDto
{
    // Yalnız AZ adı post edilir
    public string Name { get; set; }
}

public class CatalogPutDto
{
    public Guid Id { get; set; }
    // Yalnız AZ adı put edilir
    public string Name { get; set; }
}