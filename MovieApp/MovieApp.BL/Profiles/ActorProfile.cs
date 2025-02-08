using AutoMapper;
using MovieApp.BL.DTOs.ActorDtos;
using MovieApp.Core.Entities;

namespace MovieApp.BL.Profiles;
public class ActorProfile : Profile
{
	public ActorProfile()
	{
        CreateMap<ActorCreateDto, Actor>()
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => DateOnly.Parse(src.BirthDate)));

        CreateMap<ActorUpdateDto, Actor>()
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom((src, dest) =>
                string.IsNullOrWhiteSpace(src.BirthDate) ? dest.BirthDate : DateOnly.Parse(src.BirthDate)
            ))
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) =>
                srcMember != null && (srcMember is not string || !string.IsNullOrWhiteSpace(srcMember.ToString()))
            ));
        CreateMap<Actor, ActorGetDto>();
    }
}

