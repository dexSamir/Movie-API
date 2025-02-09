using AutoMapper;
using MovieApp.BL.DTOs.MovieDtos;
using MovieApp.Core.Entities;

namespace MovieApp.BL.Profiles;
public class MovieProfile : Profile
{
	public MovieProfile()
	{
		CreateMap<MovieCreateDto, Movie>();
        CreateMap<MovieUpdateDto, Movie>();
        CreateMap<Movie, MovieGetDto>();

    }
}

