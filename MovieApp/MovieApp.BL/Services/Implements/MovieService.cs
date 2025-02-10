using System.IO;
using AutoMapper;
using MovieApp.BL.DTOs.MovieDtos;
using MovieApp.BL.Exceptions.Common;
using MovieApp.BL.Services.Interfaces;
using MovieApp.Core.Entities;
using MovieApp.Core.Repositories;

namespace MovieApp.BL.Services.Implements;
public class MovieService : IMovieService
{
    readonly IMovieRepository _repo;
    readonly IActorRepository _actRepo;
    readonly IMapper _mapper;

    private readonly string[] _includeProperties =
        {
            "Actors", "MovieSubtitles", "Genres", "Ratings",
            "Reviews", "Rentals", "AudioTracks", "Recommendations"
        };

    public MovieService(IMovieRepository repo, IMapper mapper, IActorRepository actRepo)
    {
        _actRepo = actRepo;
        _mapper = mapper;
        _repo = repo;
    }


    public async Task<IEnumerable<MovieGetDto>> GetAllAsync()
    {
        var movies = await _repo.GetAllAsync(_includeProperties);

        return _mapper.Map<IEnumerable<MovieGetDto>>(movies);
    }

    public async Task<MovieGetDto> GetByIdAsync(int id)
    {
        var data = await _repo.GetByIdAsync(id, _includeProperties);

        if (data == null)
            throw new NotFoundException<Movie>();
        return _mapper.Map<MovieGetDto>(data);
    }

    public async Task<IEnumerable<MovieGetDto>> GetByGenre(string genre)
    {
        var datas = await _repo.
            GetWhereAsync(x =>
            x.Genres.Any(y => y.Genre.Name == genre), _includeProperties);

        if (!datas.Any())
            throw new NotFoundException<Movie>();

        return _mapper.Map<IEnumerable<MovieGetDto>>(datas);
    }

    public async Task<IEnumerable<MovieGetDto>> GetByRating(double rating)
    {
        var movies = await _repo.GetWhereAsync(x => x.AvgRating > rating, _includeProperties);
        var filteredMovies = movies.Where(m => m.Ratings.Any() ? m.Ratings.Average(r => r.Score) >= rating : false);
        if (!filteredMovies.Any())
            throw new NotFoundException<Movie>();

        return _mapper.Map<IEnumerable<MovieGetDto>>(filteredMovies);
    }

    public async Task<IEnumerable<MovieGetDto>> GetByReleaseDateAsync(DateOnly releaseDate)
    {
        var datas = await _repo.GetWhereAsync(x => x.ReleaseDate >= releaseDate, _includeProperties);
        if (!datas.Any())
            throw new NotFoundException<Movie>();

        return _mapper.Map<IEnumerable<MovieGetDto>>(datas);
    }

    public async Task<IEnumerable<MovieGetDto>> GetByDirectorAsync(int directorId)
    {
        var datas = await _repo.GetWhereAsync(x => x.DirectorId == directorId, _includeProperties);
        if (!datas.Any())
            throw new NotFoundException<Movie>();

        return _mapper.Map<IEnumerable<MovieGetDto>>(datas);
    }

    public async Task<IEnumerable<MovieGetDto>> GetByActorAsync(int actorId)
    {
        var datas = await _repo.GetWhereAsync(x => x.Actors.Any(y => y.ActorId == actorId), _includeProperties);

        if (!datas.Any())
            throw new NotFoundException<Movie>();

        return _mapper.Map<IEnumerable<MovieGetDto>>(datas);
    }

    public async Task<IEnumerable<MovieGetDto>> GetByDurationRangeAsync(int minDuration, int maxDuration)
    {
        var datas = await _repo.GetWhereAsync(x => x.Duration >= minDuration && x.Duration <= maxDuration, _includeProperties);
        if (!datas.Any())
            throw new NotFoundException<Movie>();
        return _mapper.Map<IEnumerable<MovieGetDto>>(datas);
    }

    public async Task<IEnumerable<MovieGetDto>> GetByTitleAsync(string title)
    {
        var datas = await _repo.GetWhereAsync(x => x.Title.Contains(title), _includeProperties);
        if (!datas.Any())
            throw new NotFoundException<Movie>();
        return _mapper.Map<IEnumerable<MovieGetDto>>(datas);
    }

    public async Task<bool> UpdateAverageRatingAsync(int movieId)
    {
        var movie = await _repo.GetByIdAsync(movieId, "Ratings");

        if (movie == null)
            throw new NotFoundException<Movie>();

        var newRating = movie.AvgRating = movie.Ratings.Any() ? movie.Ratings.Average(r => r.Score) : 0;

        return await _repo.UpdatePropertyAsync(movie, x => x.AvgRating, newRating);
    }


    public async Task<IEnumerable<MovieGetDto>> SortByTitleAsync(bool ascending = true)
    {
        var movies = await _repo.GetAllAsync(_includeProperties);

        if (movies == null)
            throw new NotFoundException<Movie>();

        var sortedMovies = ascending ?
            movies.OrderBy(x => x.Title) :
            movies.OrderByDescending(x => x.Title);
        return _mapper.Map<IEnumerable<MovieGetDto>>(sortedMovies); 
    }

    public async Task<IEnumerable<MovieGetDto>> SortByReleaseDateAsync(bool ascending = true)
    {
        var movies = await _repo.GetAllAsync(_includeProperties);

        if (movies == null)
            throw new NotFoundException<Movie>();

        var sortedMovies = ascending ?
            movies.OrderBy(x => x.ReleaseDate) :
            movies.OrderByDescending(x => x.ReleaseDate);
        return _mapper.Map<IEnumerable<MovieGetDto>>(sortedMovies);
    }
    public async Task<IEnumerable<MovieGetDto>> SortByRatingAsync(bool ascending = true)
    {
        var movies = await _repo.GetAllAsync(_includeProperties);

        if (movies == null)
            throw new NotFoundException<Movie>();

        var sortedMovies = ascending ?
            movies.OrderBy(x => x.AvgRating) :
            movies.OrderByDescending(x => x.AvgRating);
        return _mapper.Map<IEnumerable<MovieGetDto>>(sortedMovies);
    }
}

