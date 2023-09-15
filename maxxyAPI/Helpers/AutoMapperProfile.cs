using AutoMapper;
using maxxyAPI.DTOs;
using maxxyAPI.Entities;

namespace maxxyAPI.Helpers
{
    public class AutoMapperProfile
    {
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<Post, PostDto>()
                    .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                    .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId));
                CreateMap<PostDto, Post>()
                    .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                    .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId)); ;
                CreateMap<ImageDto, Image>();
                CreateMap<Image, ImageDto>();
            }
        }
    }
}
