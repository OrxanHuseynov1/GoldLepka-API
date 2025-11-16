using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.MappingProfiles;
public class ProductProfile : Profile
{
    public ProductProfile()
    {
        // Product -> ProductGetDto Mapping (Eyni qalır)
        CreateMap<Product, ProductGetDto>()
            .ForMember(dest => dest.CatalogName,
                       opt => opt.MapFrom(src => src.Catalog.Name))
            .ForMember(dest => dest.CatalogName_en,
                       opt => opt.MapFrom(src => src.Catalog.Name_en))
            .ForMember(dest => dest.CatalogName_ru,
                       opt => opt.MapFrom(src => src.Catalog.Name_ru))
            .ForMember(dest => dest.CatalogName_ar,
                       opt => opt.MapFrom(src => src.Catalog.Name_ar));


        // ProductPostDto -> Product Mapping (Eyni qalır)
        CreateMap<ProductPostDto, Product>()
            .ForMember(dest => dest.ImageUrls, opt => opt.Ignore())
            .ForMember(dest => dest.Name_en, opt => opt.Ignore())
            .ForMember(dest => dest.Name_ru, opt => opt.Ignore())
            .ForMember(dest => dest.Name_ar, opt => opt.Ignore())
            .ForMember(dest => dest.MaterialName_en, opt => opt.Ignore())
            .ForMember(dest => dest.MaterialName_ru, opt => opt.Ignore())
            .ForMember(dest => dest.MaterialName_ar, opt => opt.Ignore());

        // ProductPutDto -> Product Mapping (DÜZƏLİŞ BURADA)
        CreateMap<ProductPutDto, Product>()
            // YENİ DÜZƏLİŞ: Id-ni yeniləməkdən qaçınırıq
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ImageUrls, opt => opt.Ignore())
            .ForMember(dest => dest.Name_en, opt => opt.Ignore())
            .ForMember(dest => dest.Name_ru, opt => opt.Ignore())
            .ForMember(dest => dest.Name_ar, opt => opt.Ignore())
            .ForMember(dest => dest.MaterialName_en, opt => opt.Ignore())
            .ForMember(dest => dest.MaterialName_ru, opt => opt.Ignore())
            .ForMember(dest => dest.MaterialName_ar, opt => opt.Ignore());
    }
}