namespace Application.DTOs;

public class ContactGetDto
{
    public Guid Id { get; set; }
    public string PhoneNumber { get; set; }
    public string Mail { get; set; }
    public string ShopAdress { get; set; }

    // AZ Mətn (Əsas)
    public string ShopAdressText { get; set; }

    // YENİ: Tərcümə Olunmuş Mətn Sahələri (Nullable)
    public string? ShopAdressText_en { get; set; }
    public string? ShopAdressText_ru { get; set; }
    public string? ShopAdressText_ar { get; set; }

    public string WhatsappNumber { get; set; }
    public bool InstegramIsActive { get; set; }
    public string InstegramAccountName { get; set; }
    public bool TikTokIsActive { get; set; }
    public string TikTokAccountName { get; set; }
    public bool FacebookIsActive { get; set; }
    public string FacebookAccountName { get; set; }
}

public class ContactPostDto
{
    public string PhoneNumber { get; set; }
    public string Mail { get; set; }
    public string ShopAdress { get; set; }
    public string ShopAdressText { get; set; } // AZ Mətn post edilir
    public string WhatsappNumber { get; set; }
    public bool InstegramIsActive { get; set; }
    public string InstegramAccountName { get; set; }
    public bool TikTokIsActive { get; set; }
    public string TikTokAccountName { get; set; }
    public bool FacebookIsActive { get; set; }
    public string FacebookAccountName { get; set; }
}

public class ContactPutDto
{
    public string PhoneNumber { get; set; }
    public string Mail { get; set; }
    public string ShopAdress { get; set; }
    public string ShopAdressText { get; set; } // AZ Mətn put edilir (redaktə)
    public string WhatsappNumber { get; set; }
    public bool InstegramIsActive { get; set; }
    public string InstegramAccountName { get; set; }
    public bool TikTokIsActive { get; set; }
    public string TikTokAccountName { get; set; }
    public bool FacebookIsActive { get; set; }
    public string FacebookAccountName { get; set; }
}