using AutoMapper;
using MovieApp.BL.DTOs.MovieDtos;
using MovieApp.BL.Services.Interfaces;
using MovieApp.Core.Repositories;

namespace MovieApp.BL.Services.Implements;
public class MovieService : IMovieService
{
    public IMovieRepository _repo;
    public IActorRepository _actRepo;
    public IMapper _mapper;
    public MovieService(IMovieRepository repo, IMapper mapper, IActorRepository actRepo)
    {
        _actRepo = actRepo; 
        _mapper = mapper; 
        _repo = repo; 
    }



    public async Task<IEnumerable<MovieGetDto>> GetAllAsync()
    {
        var movies = await _repo.GetAllAsync("Actors", "MovieSubtitles", "Genres", "Ratings", "Reviews", "Rentals", "AudioTracks", "Recommendations");

        var datas = _mapper.Map<IEnumerable<MovieGetDto>>(movies);
        foreach (var dto in datas)
        {
            var movie = movies.FirstOrDefault(x => x.Id == dto.Id);
            dto.DirectorName = movie.Director.Name ?? "";  
        }
        return datas; 
    }

    public async Task<MovieGetDto> GetByIdAsync(int id)
    {
        var data = await _repo.GetByIdAsync(id, "Actors", "MovieSubtitles", "Genres", "Ratings", "Reviews", "Rentals", "AudioTracks", "Recommendations");

        if(data == null)
            throw new notfoundexception
    }
}

