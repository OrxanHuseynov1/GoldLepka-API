using Application.Interfaces;
using Application.MappingProfiles;
using Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class APRegistration
{
    public static void AddBlServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(cfg => { }, typeof(ContactProfile));
        services.AddAutoMapper(cfg => { }, typeof(UserProfile));
        services.AddAutoMapper(cfg => { }, typeof(GalleryProfile));
        services.AddAutoMapper(cfg => { }, typeof(ArticleProfile));
        services.AddAutoMapper(cfg => { }, typeof(CatalogProfile));
        services.AddAutoMapper(cfg => { }, typeof(ProductProfile));
        services.AddAutoMapper(cfg => { }, typeof(ServiceProfile));
        services.AddAutoMapper(cfg => { }, typeof(DesignerProfile));

        services.AddScoped<IContactService, ContactService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IGalleryService, GalleryService>();
        services.AddScoped<IArticleService, ArticleService>();
        services.AddScoped<ICatalogService, CatalogService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IServiceService, ServiceService>();
        services.AddScoped<IDesignerService, DesignerService>();
        services.AddScoped<ITranslationService, PythonTranslationService>();

    }
}
