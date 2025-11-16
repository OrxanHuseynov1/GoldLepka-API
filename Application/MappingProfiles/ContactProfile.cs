using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.MappingProfiles;

public class ContactProfile : Profile
{
    public ContactProfile()
    {
        CreateMap<Contact, ContactGetDto>();
        CreateMap<ContactPostDto, Contact>();
        CreateMap<ContactPutDto, Contact>();
    }
}
