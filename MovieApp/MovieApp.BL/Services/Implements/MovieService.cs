using System.IO;
using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using MovieApp.BL.DTOs.MovieDtos;
using MovieApp.BL.Exceptions.Common;
using MovieApp.BL.Extensions;
using MovieApp.BL.ExternalServices.Implements;
using MovieApp.BL.ExternalServices.Interfaces;
using MovieApp.BL.Services.Interfaces;
using MovieApp.BL.Utilities;
using MovieApp.Core.Entities;
using MovieApp.Core.Repositories;

namespace MovieApp.BL.Services.Implements;
public class MovieService : IMovieService
{
    readonly IMovieRepository _repo;
    readonly IActorRepository _actRepo;
    readonly ICacheService _cache; 
    readonly IMapper _mapper;
    readonly IFileService _fileService;

    private readonly string[] _includeProperties =
    {
        "Actors", "MovieSubtitles", "Genres", "Ratings",
        "Reviews", "Rentals", "AudioTracks", "Recommendations"
    };

    public MovieService(IMovieRepository repo, IMapper mapper, IActorRepository actRepo, IFileService fileService, ICacheService cache)
    {
        _cache = cache;
        _actRepo = actRepo;
        _fileService = fileService; 
        _mapper = mapper;
        _repo = repo;
    }

    // GET AND SORTINGS 
    public async Task<IEnumerable<MovieGetDto>> GetAllAsync()
    {
        return await _cache.GetOrSetAsync("all_movies", async () =>
        {
            var movies = await _repo.GetAllAsync(_includeProperties);
            return _mapper.Map<IEnumerable<MovieGetDto>>(movies);
        }, TimeSpan.FromMinutes(10));
    }

    public async Task<MovieGetDto> GetByIdAsync(int id)
    {
        return await _cache.GetOrSetAsync($"movie_{id}", async () =>
        {
            var movie = await _repo.GetByIdAsync(id, _includeProperties);
            if (movie == null) throw new NotFoundException<Movie>();
            return _mapper.Map<MovieGetDto>(movie);
        }, TimeSpan.FromMinutes(5));
    }

    public async Task<IEnumerable<MovieGetDto>> GetByGenre(string genre)
    {
        return await _cache.GetOrSetAsync($"movies_by_genre_{genre}", async () =>
        {
            var movies = await _repo.GetWhereAsync(x => x.Genres.Any(y => y.Genre.Name == genre), _includeProperties);
            if (!movies.Any()) throw new NotFoundException<Movie>();
            return _mapper.Map<IEnumerable<MovieGetDto>>(movies);
        }, TimeSpan.FromMinutes(5));
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
        return await _cache.GetOrSetAsync($"movies_by_direcotor_id_{directorId}", async () =>
        {
            var movies = await _repo.GetWhereAsync(x => x.DirectorId == directorId, _includeProperties);
            if (!movies.Any()) throw new NotFoundException<Movie>();
            return _mapper.Map<IEnumerable<MovieGetDto>>(movies);
        }, TimeSpan.FromMinutes(5));
    }

    public async Task<IEnumerable<MovieGetDto>> GetByActorAsync(int actorId)
    {
        return await _cache.GetOrSetAsync($"movies_by_actor_id_{actorId}", async () =>
        {
            var movies = await _repo.GetWhereAsync(x => x.Actors.Any(y => y.ActorId == actorId), _includeProperties);
            if (!movies.Any()) throw new NotFoundException<Movie>();
            return _mapper.Map<IEnumerable<MovieGetDto>>(movies);
        }, TimeSpan.FromMinutes(5));
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
        var cacheKey = $"movies_sorted_by_title_{ascending}";
        var cachedData = await _cache.GetStringAsync(cacheKey);
        if (!string.IsNullOrEmpty(cachedData))
            return JsonSerializer.Deserialize<IEnumerable<MovieGetDto>>(cachedData);

        var movies = await _repo.GetAllAsync(_includeProperties);
        var sortedMovies = ascending ? movies.OrderBy(x => x.Title) : movies.OrderByDescending(x => x.Title);
        var mappedMovies = _mapper.Map<IEnumerable<MovieGetDto>>(sortedMovies);

        await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(mappedMovies), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
        });
        return mappedMovies;
    }

    public async Task<IEnumerable<MovieGetDto>> SortByReleaseDateAsync(bool ascending = true)
    {
        var cacheKey = $"movies_sorted_by_release_date_{ascending}";
        var cachedData = await _cache.GetStringAsync(cacheKey);
        if (!string.IsNullOrEmpty(cachedData))
            return JsonSerializer.Deserialize<IEnumerable<MovieGetDto>>(cachedData);

        var movies = await _repo.GetAllAsync(_includeProperties);
        var sortedMovies = ascending ? movies.OrderBy(x => x.ReleaseDate) : movies.OrderByDescending(x => x.ReleaseDate);
        var mappedMovies = _mapper.Map<IEnumerable<MovieGetDto>>(sortedMovies);

        await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(mappedMovies), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
        });
        return mappedMovies;
    }

    public async Task<IEnumerable<MovieGetDto>> SortByRatingAsync(bool ascending = true)
    {
        var cacheKey = $"movies_sorted_by_rating_{ascending}";
        var cachedData = await _cache.GetStringAsync(cacheKey);
        if (!string.IsNullOrEmpty(cachedData))
            return JsonSerializer.Deserialize<IEnumerable<MovieGetDto>>(cachedData);

        var movies = await _repo.GetAllAsync(_includeProperties);
        var sortedMovies = ascending ? movies.OrderBy(x => x.AvgRating) : movies.OrderByDescending(x => x.AvgRating);
        var mappedMovies = _mapper.Map<IEnumerable<MovieGetDto>>(sortedMovies);

        await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(mappedMovies), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
        });
        return mappedMovies;
    }

    public async Task<double> GetAverageRatingAsync(int movieId)
    {
        var movie = await _repo.GetByIdAsync(movieId, "Actors", "MovieSubtitles", "Genres", "AudioTracks");
        if (movie == null)
            throw new NotFoundException<Movie>();

        return movie.AvgRating; 
    }


    //UPDATE AND CREATES
    public async Task<bool> UpdateAverageRatingAsync(int movieId)
    {
        var movie = await _repo.GetByIdAsync(movieId, "Ratings");

        if (movie == null)
            throw new NotFoundException<Movie>();

        var newRating = movie.AvgRating = movie.Ratings.Any() ? movie.Ratings.Average(r => r.Score) : 0;

        return await _repo.UpdatePropertyAsync(movie, x => x.AvgRating, newRating);
    }

    public async Task<int> CreateAsync(MovieCreateDto dto)
    {
        var movie = _mapper.Map<Movie>(dto);

        movie.Actors = dto.ActorIds.ToMovieActors();
        movie.MovieSubtitles = dto.SubtitleIds.ToMovieSubtitles();
        movie.Genres = dto.GenreIds.ToMovieGenres();
        movie.AudioTracks = dto.AudioTrackIds.ToAudioTracks();

        await _repo.AddAsync(movie);
        await _repo.SaveAsync();

        await _cache.RemoveAsync("all_movies");

        return movie.Id;
    }

    public async Task<IEnumerable<int>> CreateRangeAsync(IEnumerable<MovieCreateDto> dtos)
    {
        if (dtos == null || !dtos.Any())
            throw new ArgumentException("Movie list cannot be empty.", nameof(dtos));

        var movies = _mapper.Map<IEnumerable<Movie>>(dtos);

        foreach (var (dto, movie) in dtos.Zip(movies))
        {
            movie.Actors = dto.ActorIds.ToMovieActors();
            movie.MovieSubtitles = dto.SubtitleIds.ToMovieSubtitles();
            movie.Genres = dto.GenreIds.ToMovieGenres();
            movie.AudioTracks = dto.AudioTrackIds.ToAudioTracks();
        }

        await _repo.AddRangeAsync(movies);
        await _repo.SaveAsync();
        await _cache.RemoveAsync("all_movies");
        return movies.Select(m => m.Id);
    }

    public async Task<bool> UpdateAsync(MovieUpdateDto dto, int movieId)
    {
        var movie = await _repo.GetByIdAsync(movieId, "Actors", "MovieSubtitles", "Genres", "AudioTracks");
        if (movie == null)
            throw new NotFoundException<Movie>();

        _mapper.Map(dto, movie);


        movie.Actors = dto.ActorIds.ToMovieActors();
        movie.MovieSubtitles = dto.SubtitleIds.ToMovieSubtitles();
        movie.Genres = dto.GenreIds.ToMovieGenres();
        movie.AudioTracks = dto.AudioTrackIds.ToAudioTracks();

        movie.PosterUrl = await _fileService.ProcessImageAsync(dto.PosterFile, "movies/posters", "image/", 50, movie.PosterUrl);
        movie.TrailerUrl = await _fileService.ProcessImageAsync(dto.TrailerFile, "movies/trailers", "video/", 1024, movie.TrailerUrl);

        _repo.UpdateAsync(movie);
        bool updated = await _repo.SaveAsync() > 0;

        if (updated)
        {
            await _cache.RemoveAsync($"movie_{movieId}");
            await _cache.RemoveAsync("all_movies");
        }

        return updated;
    }

    public async Task<bool> UpdatePosterUrlAsync(int movieId, IFormFile posterFile)
    {
        var movie = await _repo.GetByIdAsync(movieId, _includeProperties);
        if (movie == null)
            throw new NotFoundException<Movie>();

        movie.PosterUrl = await _fileService.ProcessImageAsync(posterFile, "movies/posters", "image/", 15, movie.PosterUrl);

        _repo.UpdateAsync(movie);
        bool updated = await _repo.SaveAsync() > 0;

        if (updated)
        {
            await _cache.RemoveAsync($"movie_{movieId}");
            await _cache.RemoveAsync("all_movies");
        }

        return updated;
    }

    public async Task<bool> UpdateTrailerUrlAsync(int movieId, IFormFile trailerFile)
    {
        var movie = await _repo.GetByIdAsync(movieId, _includeProperties);
        if (movie == null)
            throw new NotFoundException<Movie>();

        movie.TrailerUrl = await _fileService.ProcessImageAsync(trailerFile, "movies/trailers", "video/", 1024, movie.TrailerUrl);

        _repo.UpdateAsync(movie);
        bool updated = await _repo.SaveAsync() > 0;

        if (updated)
        {
            await _cache.RemoveAsync($"movie_{movieId}");
            await _cache.RemoveAsync("all_movies");
        }

        return updated;
    }


    //DELETE
    public async Task<bool> DeleteAsync(int id)
    {
        var data = await _repo.GetByIdAsync(id, false);
        if (data == null)
            throw new NotFoundException<Movie>();

        await _fileService.DeleteImageIfNotDefault(data.PosterUrl, "movies/posters");
        await _fileService.DeleteImageIfNotDefault(data.TrailerUrl, "movies/trailers");

        await _repo.DeleteAsync(id);
        bool deleted = await _repo.SaveAsync() > 0;

        if (deleted)
        {
            await _cache.RemoveAsync($"movie_{id}");
            await _cache.RemoveAsync("all_movies");
        }

        return deleted;
    }

    public async Task<bool> DeleteRangeAsync(string ids)
    {
        var idArray = FileHelper.ParseIds(ids);
        if (idArray.Length == 0)
            throw new ArgumentException("No valid IDs provided");

        await EnsureMovieExist(idArray);

        foreach (var id in idArray)
        {
            var data = await _repo.GetByIdAsync(id, false);
            if (data != null)
            {
                await _fileService.DeleteImageIfNotDefault(data.TrailerUrl, "movies/trailers");
                await _fileService.DeleteImageIfNotDefault(data.PosterUrl, "movies/posters");
            }
        }
        await _repo.DeleteRangeAsync(idArray);
        bool deleted = idArray.Length == await _repo.SaveAsync();

        if (deleted)
        {
            foreach (var id in idArray)
            {
                await _cache.RemoveAsync($"movie_{id}");
            }
            await _cache.RemoveAsync("all_movies");
        }

        return deleted;
    }

    public async Task<bool> ReverseDeleteAsync(int id)
    {
        await EnsureMovieExists(id);
        await _repo.ReverseSoftDeleteAsync(id);
        bool restored = await _repo.SaveAsync() > 0;

        if (restored)
        {
            await _cache.RemoveAsync($"movie_{id}");
            await _cache.RemoveAsync("all_movies");
        }

        return restored;
    }

    public async Task<bool> ReverseDeleteRangeAsync(string ids)
    {
        var idArray = FileHelper.ParseIds(ids);
        await EnsureMovieExist(idArray);

        await _repo.ReverseSoftDeleteRangeAsync(idArray);
        bool restored = idArray.Length == await _repo.SaveAsync();

        if (restored)
        {
            foreach (var id in idArray)
            {
                await _cache.RemoveAsync($"movie_{id}");
            }
            await _cache.RemoveAsync("all_movies");
        }

        return restored;
    }

    public async Task<bool> SoftDeleteAsync(int id)
    {
        var data = await _repo.GetFirstAsync(x => x.Id == id && !x.IsDeleted, false);
        if (data == null)
            throw new NotFoundException<Movie>();

        _repo.SoftDelete(data);
        bool deleted = await _repo.SaveAsync() > 0;

        if (deleted)
        {
            await _cache.RemoveAsync($"movie_{id}");
            await _cache.RemoveAsync("all_movies");
        }

        return deleted;
    }

    public async Task<bool> SoftDeleteRangeAsync(string ids)
    {
        var idArray = FileHelper.ParseIds(ids);
        await EnsureMovieExist(idArray);

        await _repo.SoftDeleteRangeAsync(idArray);
        bool deleted = idArray.Length == await _repo.SaveAsync();

        if (deleted)
        {
            foreach (var id in idArray)
            {
                await _cache.RemoveAsync($"movie_{id}");
            }
            await _cache.RemoveAsync("all_movies");
        }

        return deleted;
    }

    //PRIVATE DELETE
    private async Task EnsureMovieExists(int id)
    {
        if (!await _repo.IsExistAsync(id))
            throw new NotFoundException<Movie>();
    }

    private async Task EnsureMovieExist(int[] ids)
    {
        var existingCount = await _repo.CountAsync(ids);
        if (existingCount != ids.Length)
            throw new NotFoundException<Movie>();
    }


}

