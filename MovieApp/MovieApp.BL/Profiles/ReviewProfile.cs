using AutoMapper;
using MovieApp.BL.DTOs.ReactionDtos;
using MovieApp.BL.DTOs.ReviewDtos;
using MovieApp.Core.Entities;

namespace MovieApp.BL.Profiles;
public class ReviewProfile : Profile
{
	public ReviewProfile()
	{
        CreateMap<Review, ReviewGetDto>()
               .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? src.User.UserName : "Unknown"))
               .ForMember(dest => dest.IsUpdated, opt => opt.MapFrom(src => src.UpdatedTime != null))
               .ForMember(dest => dest.Replies, opt => opt.MapFrom(src => src.Replies != null ? src.Replies : new List<Review>()));

        CreateMap<ReviewCreateDto, Review>();
        CreateMap<ReviewUpdateDto, Review>();
        CreateMap<Review, ReactionCountDto>();

    }
}

