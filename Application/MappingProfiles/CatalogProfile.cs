using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.MappingProfiles;
public class CatalogProfile : Profile
{
    public CatalogProfile()
    {
        CreateMap<Catalog, CatalogGetDto>();

        CreateMap<CatalogPostDto, Catalog>()
            .ForMember(dest => dest.Name_en, opt => opt.Ignore())
            .ForMember(dest => dest.Name_ru, opt => opt.Ignore())
            .ForMember(dest => dest.Name_ar, opt => opt.Ignore());

        CreateMap<CatalogPutDto, Catalog>()
            // DÜZƏLİŞ: Update zamanı DTO-dan gələn Id-ni ignor edirik
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Name_en, opt => opt.Ignore())
            .ForMember(dest => dest.Name_ru, opt => opt.Ignore())
            .ForMember(dest => dest.Name_ar, opt => opt.Ignore());
    }
}