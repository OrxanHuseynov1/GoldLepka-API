using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.MappingProfiles;
public class ArticleProfile : Profile
{
    public ArticleProfile()
    {
        // GET Mapping (Hamısını göndəririk)
        CreateMap<Article, ArticleGetDTO>()
             .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate));

        // POST Mapping (Yalnız AZ mətni gəlir)
        CreateMap<ArticlePostDTO, Article>()
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        // PUT Mapping (Yalnız AZ mətni gəlir)
        CreateMap<ArticlePutDTO, Article>()
            .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore());

        // Qeyd: Question_en, Answer_en və s. servisdə təyin olunacaq.
    }
}