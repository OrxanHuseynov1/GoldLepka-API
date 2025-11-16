using Domain.Entities.Common;

namespace Domain.Entities;

public class Service : BaseEntity
{
    public string ImageUrl { get; set; }
    public string Name { get; set; }
    public string Name_en { get; set; }
    public string Name_ru { get; set; }
    public string Name_ar { get; set; }
    public string About {  get; set; }
    public string About_en {  get; set; }
    public string About_ru {  get; set; }
    public string About_ar {  get; set; }
}
