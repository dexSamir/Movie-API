using AutoMapper;
using MovieApp.BL.DTOs.GenreDtos;
using MovieApp.Core.Entities;

namespace MovieApp.BL.Profiles;
public class GenreProfile : Profile
{
	public GenreProfile()
	{
		CreateMap<GenreCreateDto, Genre>();
		CreateMap<Genre, GenreGetDto>();
	}
}

