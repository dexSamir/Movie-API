using System.ComponentModel.DataAnnotations;
using System.IO;
using AutoMapper;
using MovieApp.BL.DTOs.MovieDtos;
using MovieApp.BL.Exceptions.Common;
using MovieApp.BL.Exceptions.Image;
using MovieApp.BL.Extensions;
using MovieApp.BL.Services.Interfaces;
using MovieApp.Core.Entities;
using MovieApp.Core.Entities.Relational;
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

    // GET AND SORTINGS 
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


    //UPDATE AND CREATES
    public async Task<int> CreateAsync(MovieCreateDto dto)
    {
        var movie = _mapper.Map<Movie>(dto);

        if (dto.ActorIds != null && dto.ActorIds.Any())
            movie.Actors = dto.ActorIds.Select(actorId => new MovieActor { ActorId = actorId }).ToList();

        if (dto.SubtitleIds != null && dto.SubtitleIds.Any())
            movie.MovieSubtitles = dto.SubtitleIds.Select(subtitleId => new MovieSubtitle { SubtitleId = subtitleId }).ToList();

        if (dto.GenreIds != null && dto.GenreIds.Any())
            movie.Genres = dto.GenreIds.Select(genreId => new MovieGenre { GenreId = genreId }).ToList();

        if (dto.AudioTrackIds != null && dto.AudioTrackIds.Any())
            movie.AudioTracks = dto.AudioTrackIds.Select(audioTrackId => new AudioTrack { Id = audioTrackId }).ToList();

        await _repo.AddAsync(movie);
        await _repo.SaveAsync();

        return movie.Id;
    }

    public async Task<IEnumerable<int>> CreateRangeAsync(IEnumerable<MovieCreateDto> dtos)
    {
        if (dtos == null || !dtos.Any())
            throw new ArgumentException("Movie list cannot be empty.", nameof(dtos));

        var movies = _mapper.Map<IEnumerable<Movie>>(dtos);

        foreach (var movie in movies)
        {
            var dto = dtos.First(d => d.Title == movie.Title); 

            if (dto.ActorIds != null && dto.ActorIds.Any())
                movie.Actors = dto.ActorIds.Select(actorId => new MovieActor { ActorId = actorId }).ToList();

            if (dto.SubtitleIds != null && dto.SubtitleIds.Any())
                movie.MovieSubtitles = dto.SubtitleIds.Select(subtitleId => new MovieSubtitle { SubtitleId = subtitleId }).ToList();

            if (dto.GenreIds != null && dto.GenreIds.Any())
                movie.Genres = dto.GenreIds.Select(genreId => new MovieGenre { GenreId = genreId }).ToList();

            if (dto.AudioTrackIds != null && dto.AudioTrackIds.Any())
                movie.AudioTracks = dto.AudioTrackIds.Select(audioTrackId => new AudioTrack { Id = audioTrackId }).ToList();
        }

        await _repo.AddRangeAsync(movies);
        await _repo.SaveAsync();

        return movies.Select(m => m.Id);
    }

    public async Task<bool> UpdateAsync(MovieUpdateDto dto, int movieId)
    {
        var movie = await _repo.GetByIdAsync(movieId, "Actors", "MovieSubtitles", "Genres", "AudioTracks");
        if (movie == null)
            throw new NotFoundException<Movie>();

        _mapper.Map(dto, movie);

        if (dto.ActorIds != null && dto.ActorIds.Any())
            movie.Actors = dto.ActorIds.Select(actorId => new MovieActor { ActorId = actorId }).ToList();

        if (dto.SubtitleIds != null && dto.SubtitleIds.Any())
            movie.MovieSubtitles = dto.SubtitleIds.Select(subtitleId => new MovieSubtitle { SubtitleId = subtitleId }).ToList();

        if (dto.GenreIds != null && dto.GenreIds.Any())
            movie.Genres = dto.GenreIds.Select(genreId => new MovieGenre { GenreId = genreId }).ToList();

        if (dto.AudioTrackIds != null && dto.AudioTrackIds.Any())
            movie.AudioTracks = dto.AudioTrackIds.Select(audioTrackId => new AudioTrack { Id = audioTrackId }).ToList();

        if (dto.PosterFile != null)
        {
            if (!dto.PosterFile.IsValidType("image/"))
                throw new UnsupportedFileTypeException("Poster must be an image file.");

            if (!dto.PosterFile.IsValidSize(50)) 
                throw new ValidationException("Poster file size must be less than 50 MB.");

            if (!string.IsNullOrEmpty(movie.PosterUrl))
            {
                var oldPosterPath = Path.Combine("wwwroot", "imgs", "movies", "posters", movie.PosterUrl);
                FileExtension.DeleteFile(oldPosterPath);
            }

            movie.PosterUrl = await dto.PosterFile.UploadAsync("wwwroot", "imgs", "movies", "posters");
        }

        if (dto.TrailerFile != null)
        {
            if (!dto.TrailerFile.IsValidType("video/"))
                throw new ValidationException("Trailer must be a video file.");

            if (!dto.TrailerFile.IsValidSize(1024)) 
                throw new ValidationException("Trailer file size must be less than 1 GB.");

            if (!string.IsNullOrEmpty(movie.TrailerUrl))
            {
                var oldTrailerPath = Path.Combine("wwwroot", "imgs", "movies", "trailers", movie.TrailerUrl);
                FileExtension.DeleteFile(oldTrailerPath);
            }

            movie.TrailerUrl = await dto.TrailerFile.UploadAsync("wwwroot", "imgs", "movies", "trailers");
        }

        _repo.UpdateAsync(movie);
        return await _repo.SaveAsync() > 0;
    }

    public async Task<bool> UpdateAverageRatingAsync(int movieId)
    {
        var movie = await _repo.GetByIdAsync(movieId, "Ratings");

        if (movie == null)
            throw new NotFoundException<Movie>();

        var newRating = movie.AvgRating = movie.Ratings.Any() ? movie.Ratings.Average(r => r.Score) : 0;

        return await _repo.UpdatePropertyAsync(movie, x => x.AvgRating, newRating);
    }
}

