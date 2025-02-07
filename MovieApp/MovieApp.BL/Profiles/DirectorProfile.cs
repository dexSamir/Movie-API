using AutoMapper;
using MovieApp.BL.DTOs.DirectorDtos;
using MovieApp.Core.Entities;

namespace MovieApp.BL.Profiles;
public class DirectorProfile : Profile
{
	public DirectorProfile()
	{
		CreateMap<DirectorCreateDto, Director>()
			.ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => DateOnly.Parse(src.BirthDate)));

        CreateMap<DirectorUpdateDto, Director>()
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => DateOnly.Parse(src.BirthDate)))
			.ForAllMembers(x=> x.Condition((src, dest, srcMember) => srcMember != null));
		CreateMap<Director, DirectorGetDto>(); 
    }
}

