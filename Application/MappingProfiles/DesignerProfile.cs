using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.MappingProfiles
{
    public class DesignerProfile : Profile
    {
        public DesignerProfile()
        {
            // Domain → GET DTO
            CreateMap<Designer, DesignerGetDto>();

            // POST DTO → Domain
            CreateMap<DesignerPostDto, Designer>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
                .ForMember(dest => dest.WorksImage, opt => opt.Ignore()) // şəkillər servisdə əlavə olunacaq
                .ForMember(dest => dest.Name_en, opt => opt.Ignore())
                .ForMember(dest => dest.Name_ru, opt => opt.Ignore())
                .ForMember(dest => dest.Name_ar, opt => opt.Ignore());

            // PUT DTO → Domain
            CreateMap<DesignerPutDto, Designer>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
                .ForMember(dest => dest.WorksImage, opt => opt.Ignore()) // servisdə update olunacaq
                .ForMember(dest => dest.Name_en, opt => opt.Ignore())
                .ForMember(dest => dest.Name_ru, opt => opt.Ignore())
                .ForMember(dest => dest.Name_ar, opt => opt.Ignore());
        }
    }
}
