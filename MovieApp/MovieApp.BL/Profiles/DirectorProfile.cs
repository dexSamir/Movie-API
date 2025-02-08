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
             .ForMember(dest => dest.BirthDate, opt => opt.MapFrom((src, dest) =>
                 string.IsNullOrWhiteSpace(src.BirthDate) ? dest.BirthDate : DateOnly.Parse(src.BirthDate)
             ))
             .ForAllMembers(opt => opt.Condition((src, dest, srcMember) =>
                 srcMember != null && (srcMember is not string || !string.IsNullOrWhiteSpace(srcMember.ToString()))
             ));

        CreateMap<Director, DirectorGetDto>(); 
    }
}

