﻿using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MovieApp.BL.DTOs.MovieDtos;
using MovieApp.BL.DTOs.ReactionDtos;
using MovieApp.BL.DTOs.RecommendationDtos;
using MovieApp.BL.DTOs.RentalDtos;
using MovieApp.BL.Exceptions.AuthException;
using MovieApp.BL.Exceptions.Common;
using MovieApp.BL.Extensions;
using MovieApp.BL.ExternalServices.Interfaces;
using MovieApp.BL.Services.Interfaces;
using MovieApp.BL.Utilities;
using MovieApp.BL.Utilities.Enums;
using MovieApp.Core.Entities;
using MovieApp.Core.Repositories;

namespace MovieApp.BL.Services.Implements;
public class MovieService : IMovieService
{
    readonly IMovieRepository _repo;
    readonly IActorRepository _actRepo;
    readonly ILikeDislikeService _like; 
    readonly ICacheService _cache; 
    readonly IMapper _mapper;
    readonly ICurrentUser _user;
    readonly IFileService _fileService;
    readonly IRatingService _ratingService;
    readonly IRentalService _rental; 

    private readonly string[] _includeProperties =
    {
        "MovieSubtitles", "Ratings", "Director",
        "Reviews", "Rentals", "AudioTracks", "Recommendations",
        "Actors.Actor", "Genres.Genre"
    };

    private async Task<string> GetUserIdAsync()
    {
        var userId = _user.GetId();
        if (userId == null)
            throw new AuthorisationException<User>();
        return await Task.FromResult(userId);
    }

    public MovieService(IMovieRepository repo, IMapper mapper, IActorRepository actRepo, IFileService fileService, ICacheService cache, ICurrentUser user, IRatingService ratingService, ILikeDislikeService like, IRentalService rental)
    {
        _rental = rental; 
        _like = like; 
        _ratingService = ratingService; 
        _user = user; 
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

    public Task<IEnumerable<MovieGetDto>> GetByGenre(string genre)
        => FilterAsync(x => x.Genres.Any(y => y.Genre.Name == genre), $"movies_by_genre_{genre}");

    public Task<IEnumerable<MovieGetDto>> GetByDirectorAsync(int directorId)
        => FilterAsync(x => x.DirectorId == directorId, $"movies_by_director_{directorId}");

    public Task<IEnumerable<MovieGetDto>> GetByActorAsync(int actorId)
        => FilterAsync(x => x.Actors.Any(y => y.ActorId == actorId), $"movies_by_actor_id_{actorId}");

    public async Task<IEnumerable<MovieGetDto>> GetByRating(double rating)
    {
        var movies = await _repo.GetWhereAsync(x => x.AvgRating > rating, _includeProperties);
        var filteredMovies = movies.Where(m => m.Ratings.Any() ? m.Ratings.Average(r => r.Score) >= rating : false);
        if (!filteredMovies.Any())
            throw new NotFoundException<Movie>();

        return _mapper.Map<IEnumerable<MovieGetDto>>(filteredMovies);
    }

    public async Task<IEnumerable<MovieGetDto>> GetByReleaseDateAsync(DateOnly releaseDate)
        => await FilterGetByAsync(x => x.ReleaseDate >= releaseDate);

    public async Task<IEnumerable<MovieGetDto>> GetByDurationRangeAsync(int minDuration, int maxDuration)
        => await FilterGetByAsync(x => x.Duration >= minDuration && x.Duration <= maxDuration);

    public async Task<IEnumerable<MovieGetDto>> GetByTitleAsync(string title)
        => await FilterGetByAsync(x => x.Title.Contains(title));

    public Task<IEnumerable<MovieGetDto>> SortByTitleAsync(bool ascending = true)
        => FilterSortAsync(x => x.Title, $"movies_sorted_by_title_{ascending}", ascending);

    public Task<IEnumerable<MovieGetDto>> SortByReleaseDateAsync(bool ascending = true)
        => FilterSortAsync(x => x.ReleaseDate, $"movies_sorted_by_release_date_{ascending}", ascending);

    public Task<IEnumerable<MovieGetDto>> SortByRatingAsync(bool ascending = true)
        => FilterSortAsync(x => x.AvgRating, $"movies_sorted_by_rating_{ascending}", ascending);


    //Yuxaridaki methodlar ucun umumi bir method
    public async Task<IEnumerable<MovieGetDto>> FilterGetByAsync(Expression<Func<Movie, bool>> expression)
    {
        var movies = await _repo.GetWhereAsync(expression, _includeProperties);
        if (!movies.Any()) throw new NotFoundException<Movie>();
        return _mapper.Map<IEnumerable<MovieGetDto>>(movies);
    }
    public async Task<IEnumerable<MovieGetDto>> FilterAsync(Expression<Func<Movie, bool>> predicate, string cacheKey)
    {
        return await _cache.GetOrSetAsync(cacheKey, async () =>
        {
            return await FilterGetByAsync(predicate); 
        }, TimeSpan.FromMinutes(5));
    }
    public async Task<IEnumerable<MovieGetDto>> FilterSortAsync<TKey>( Func<Movie, TKey> predicate, string cacheKey, bool ascending = true)
    {
        return await _cache.GetOrSetAsync(cacheKey, async () =>
        {
            var movies = await _repo.GetAllAsync(_includeProperties);
            if (!movies.Any()) throw new NotFoundException<Movie>();

            var sortedMovies = ascending
                ? movies.OrderBy(predicate)
                : movies.OrderByDescending(predicate);

            return _mapper.Map<IEnumerable<MovieGetDto>>(sortedMovies);
        }, TimeSpan.FromMinutes(5));
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
        movie.Genres = dto.GenreIds.ToMovieGenres();

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
            movie.Genres = dto.GenreIds.ToMovieGenres();
        }

        await _repo.AddRangeAsync(movies);
        await _repo.SaveAsync();
        await _cache.RemoveAsync("all_movies");
        return movies.Select(m => m.Id);
    }

    public async Task<bool> UpdateAsync(MovieUpdateDto dto, int movieId)
    {
        var movie = await _repo.GetByIdAsync(movieId, _includeProperties);
        if (movie == null)
            throw new NotFoundException<Movie>();

        _mapper.Map(dto, movie);

        movie.Actors = dto.ActorIds.ToMovieActors();
        movie.Genres = dto.GenreIds.ToMovieGenres();

        _repo.UpdateAsync(movie);
        bool updated = await _repo.SaveAsync() > 0;

        if (updated)
        {
            await _cache.RemoveAsync($"movie_{movieId}");
            await _cache.RemoveAsync("all_movies");
        }

        return updated;
    }

    public async Task<bool> UpdateMediaUrlAsync(int movieId, IFormFile file, EMediaType type)
    {
        var movie = await _repo.GetByIdAsync(movieId);
        if (movie == null)
            throw new NotFoundException<Movie>();

        string folder = type == EMediaType.Poster ? "movies/posters" : "movies/trailers";
        string contentType = type == EMediaType.Poster ? "image/" : "video/";
        int maxSize = type == EMediaType.Poster ? 15 : 1024;

        string updatedUrl = await _fileService.ProcessImageAsync(file, folder, contentType, maxSize,
            type == EMediaType.Poster ? movie.PosterUrl : movie.TrailerUrl);

        if (type == EMediaType.Poster)
            movie.PosterUrl = updatedUrl;
        else
            movie.TrailerUrl = updatedUrl;

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
    public async Task<bool> DeleteAsync(string ids, EDeleteType deleteType)
    {
        var idArray = FileHelper.ParseIds(ids);
        if (idArray.Length == 0)
            throw new ArgumentException("Hec bir id daxil edilmiyib.");

        await EnsureMovieExist(idArray);

        if (deleteType == EDeleteType.Hard)
        {
            foreach (var id in idArray)
            {
                var data = await _repo.GetByIdAsync(id, false);
                if (data != null)
                {
                    await _fileService.DeleteImageIfNotDefault(data.PosterUrl, "movies/posters");
                    await _fileService.DeleteImageIfNotDefault(data.TrailerUrl, "movies/trailers");
                }
            }
        }

        switch (deleteType)
        {
            case EDeleteType.Soft:
                await _repo.SoftDeleteRangeAsync(idArray);
                break;
            case EDeleteType.Hard:
                await _repo.DeleteRangeAsync(idArray);
                break;
            case EDeleteType.Reverse:
                await _repo.ReverseSoftDeleteRangeAsync(idArray);
                break;
        }

        bool success = idArray.Length == await _repo.SaveAsync();

        if (success)
        {
            foreach (var id in idArray)
                await _cache.RemoveAsync($"movie_{id}");

            await _cache.RemoveAsync("all_movies");
        }

        return success;
    }

    //PRIVATE DELETE TEST
    private async Task EnsureMovieExist(int[] ids)
    {
        var existingCount = await _repo.CountAsync(ids);
        if (existingCount != ids.Length)
            throw new NotFoundException<Movie>();
    }

    //RATING
    public async Task<bool> RateMovieAsync(int movieId, int score)
        => await _ratingService.RateMovieAsync(movieId, score);

    public async Task<bool> UpdateRatingAsync(int movieId, int newScore)
        => await _ratingService.UpdateRatingAsync(movieId, newScore);

    public async Task<bool> DeleteRatingAsync(int movieId)
        => await _ratingService.DeleteRatingAsync(movieId);

    public async Task<double> GetAverageRatingAsync(int movieId)
        => await _ratingService.GetAverageRatingForMovieAsync(movieId);


    //Like And Dislike
    public async Task<bool> LikeMovieAsync(int movieId)
        => await _like.LikeAsync(EReactionEntityType.Movie, movieId);

    public async Task<bool> DislikeMovieAsync(int movieId)
        => await _like.DislikeAsync(EReactionEntityType.Movie, movieId);

    public async Task<bool> UndoLikeMovieAsync(int movieId)
        => await _like.UndoLikeAsync(EReactionEntityType.Movie, movieId);

    public async Task<bool> UndoDislikeMovieAsync(int movieId)
        => await _like.UndoDislikeAsync(EReactionEntityType.Movie, movieId);

    public async Task<ReactionCountDto> GetMovieReactionCountAsync(int movieId)
        => await _like.GetLikeDislikeCountAsync(EReactionEntityType.Movie, movieId);


    public async Task<int> GetTotalMovieCountAsync()
    {
        var cacheKey = "TotalMovieCount";
        return await _cache.GetOrSetAsync(cacheKey, async () => await _repo.GetTotalMovieCountAsync(), TimeSpan.FromMinutes(10));
    }

    public async Task<int> GetTotalWatchCountAsync(int movieId)
    {
        var cacheKey = $"TotalWatchCount_{movieId}";
        return await _cache.GetOrSetAsync(cacheKey, async () =>
        {
            var movie = await _repo.GetByIdAsync(movieId, _includeProperties);
            return movie?.WatchCount ?? 0;
        }, TimeSpan.FromMinutes(10));
    }

    public async Task<IEnumerable<MovieGetDto>> GetTopRatedMoviesAsync(int count)
    {
        var cacheKey = $"TopRatedMovies_{count}";
        return await _cache.GetOrSetAsync(cacheKey, async () =>
        {
            var movies = await _repo.GetTopRatedMoviesAsync(count);
            return _mapper.Map<IEnumerable<MovieGetDto>>(movies);
        }, TimeSpan.FromMinutes(10));
    }

    public async Task<IEnumerable<MovieGetDto>> GetMostWatchedMoviesAsync(int count)
    {
        var cacheKey = $"MostWatchedMovies_{count}";
        return await _cache.GetOrSetAsync(cacheKey, async () =>
        {
            var movies = await _repo.GetMostWatchedMoviesAsync(count);
            return _mapper.Map<IEnumerable<MovieGetDto>>(movies);
        }, TimeSpan.FromMinutes(10));
    }

    public async Task<IEnumerable<RecommendationGetDto>> GetRecommendationsAsync()
    {
        var userId = await GetUserIdAsync();
        if (string.IsNullOrEmpty(userId)) throw new AuthorisationException<User>();

        var cacheKey = $"Recommendations_{userId}";
        return await _cache.GetOrSetAsync(cacheKey, async () =>
        {
            var recommendations = await _repo.GetRecommendationsAsync(userId);
            return _mapper.Map<IEnumerable<RecommendationGetDto>>(recommendations);
        }, TimeSpan.FromMinutes(10));
    }

    public async Task<IEnumerable<MovieGetDto>> GetPopularMoviesAsync()
    {
        var cacheKey = "PopularMovies";
        return await _cache.GetOrSetAsync(cacheKey, async () =>
        {
            var movies = await _repo.GetPopularMoviesAsync();
            return _mapper.Map<IEnumerable<MovieGetDto>>(movies);
        }, TimeSpan.FromMinutes(10));
    }

    public async Task<IEnumerable<MovieGetDto>> GetRecentlyAddedMoviesAsync()
    {
        var cacheKey = "RecentlyAddedMovies";
        return await _cache.GetOrSetAsync(cacheKey, async () =>
        {
            var movies = await _repo.GetRecentlyAddedMoviesAsync();
            return _mapper.Map<IEnumerable<MovieGetDto>>(movies);
        }, TimeSpan.FromMinutes(10));
    }

    public async Task<bool> RentMovieAsync(int movieId)
    {
        var movie = await _repo.GetByIdAsync(movieId, false, _includeProperties);
        if (movie == null)
            throw new NotFoundException<Movie>();

        var rentalCreateDto = new RentalCreateDto
        {
            MovieId = movieId,
            RentalDate = DateTime.UtcNow,
            ReturnDate = DateTime.UtcNow.AddDays(7), 
            Price = movie.RentalPrice,
        };

        await _rental.RentMovieAsync(rentalCreateDto);
        return true;
    }

    public async Task<IEnumerable<MovieGetDto>> GetUserRentedMoviesAsync()
    {
        var rentals = await _rental.GetUserRentalsAsync();
        var rentedMovies = rentals.Select(r => r.MovieTitle);
        return _mapper.Map<IEnumerable<MovieGetDto>>(rentedMovies);
    }
}