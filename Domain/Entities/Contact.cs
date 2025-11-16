using Domain.Entities.Common;

namespace Domain.Entities;

public class Contact : BaseEntity
{
    public string PhoneNumber { get; set; }
    public string Mail { get; set; }
    public string ShopAdress { get; set; } // Web Sitede Konumu gostermek ucun misal : https://maps.google.com/?q=40.399139,49.865417
    public string ShopAdressText { get; set; } // AZ Mətn

    // YENİ: Tərcümə Olunmuş Mətn Sahələri (Nullable)
    public string? ShopAdressText_en { get; set; } // English
    public string? ShopAdressText_ru { get; set; } // Russian
    public string? ShopAdressText_ar { get; set; } // Arabic

    public string WhatsappNumber { get; set; }

    public bool InstegramIsActive { get; set; }
    public string InstegramAccountName { get; set; }

    public bool TikTokIsActive { get; set; }
    public string TikTokAccountName { get; set; }

    public bool FacebookIsActive { get; set; }
    public string FacebookAccountName { get; set; }
}