using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.MappingProfiles;

public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        // Entity → GetDto
        CreateMap<Service, ServiceGetDto>();

        // PostDto → Entity
        CreateMap<ServicePostDto, Service>()
            .ForMember(dest => dest.ImageUrl, opt => opt.Ignore()) // Fayl backend-də ayrılıqda yüklənəcək
            .ForMember(dest => dest.Name_en, opt => opt.Ignore())
            .ForMember(dest => dest.Name_ru, opt => opt.Ignore())
            .ForMember(dest => dest.Name_ar, opt => opt.Ignore())
            .ForMember(dest => dest.About_en, opt => opt.Ignore())
            .ForMember(dest => dest.About_ru, opt => opt.Ignore())
            .ForMember(dest => dest.About_ar, opt => opt.Ignore());

        // PutDto → Entity
        CreateMap<ServicePutDto, Service>()
            .ForMember(dest => dest.ImageUrl, opt => opt.Ignore()) // Fayl backend-də ayrı yüklənir
            .ForMember(dest => dest.Name_en, opt => opt.Ignore())
            .ForMember(dest => dest.Name_ru, opt => opt.Ignore())
            .ForMember(dest => dest.Name_ar, opt => opt.Ignore())
            .ForMember(dest => dest.About_en, opt => opt.Ignore())
            .ForMember(dest => dest.About_ru, opt => opt.Ignore())
            .ForMember(dest => dest.About_ar, opt => opt.Ignore());
    }
}