using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.MappingProfiles;

public class GalleryProfile : Profile
{
    public GalleryProfile()
    {
        CreateMap<Gallery, GalleryGetDto>().ReverseMap();
        CreateMap<Gallery, GalleryPostDto>().ReverseMap();
        CreateMap<Gallery, GalleryPutDto>().ReverseMap();
    }
}
