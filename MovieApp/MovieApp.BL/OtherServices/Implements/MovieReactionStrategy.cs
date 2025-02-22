using MovieApp.BL.Exceptions.Common;
using MovieApp.BL.Exceptions.LikeOrDislike;
using MovieApp.BL.ExternalServices.Interfaces;
using MovieApp.BL.OtherServices.Interfaces;
using MovieApp.BL.Utilities.Enums;
using MovieApp.Core.Entities;
using MovieApp.Core.Repositories;

namespace MovieApp.BL.OtherServices.Implements;
public class MovieReactionStrategy : IReactionStrategy
{
    public EReactionEntityType EntityType => EReactionEntityType.Movie;

    readonly IMovieRepository _movieRepo;
    readonly ICacheService _cacheService;
    readonly ILikeDislikeRepository _likeRepo;

    public MovieReactionStrategy(IMovieRepository movieRepo, ILikeDislikeRepository likeRepo, ICacheService cacheService)
    {
        _cacheService = cacheService; 
        _movieRepo = movieRepo;
        _likeRepo = likeRepo;
    }

    public async Task<(int LikeCount, int DislikeCount)> GetReactionCountAsync(int entityId)
    {
        var movie = await _movieRepo.GetByIdAsync(entityId);
        if (movie == null)
            throw new NotFoundException<Movie>();

        return (movie.LikeCount, movie.DislikeCount);
    }

    public async Task<bool> LikeAsync(int entityId, string userId)
    {
        var movie = await _movieRepo.GetByIdAsync(entityId, false);
        if (movie == null)
            throw new NotFoundException<Movie>();

        if (await _likeRepo.HasUserReactedAsync(entityId, userId, true, typeof(Movie)))
            throw new AlreadyLikedException<Movie>();

        if (await _likeRepo.HasUserReactedAsync(entityId, userId, false, typeof(Movie)))
        {
            await _likeRepo.RemoveUserReactionAsync(entityId, userId, false, typeof(Movie));
            movie.DislikeCount = Math.Max(0, movie.DislikeCount - 1);
        }

        movie.LikeCount++;
        await _likeRepo.AddUserReactionAsync(entityId, userId, true, typeof(Movie));

        bool result = await _movieRepo.SaveAsync() > 0;

        if (result)
            await _cacheService.RemoveAsync($"movie_{entityId}");

        return result;
    }

    public async Task<bool> DislikeAsync(int entityId, string userId)
    {
        var movie = await _movieRepo.GetByIdAsync(entityId, false);
        if (movie == null)
            throw new NotFoundException<Movie>();

        if (await _likeRepo.HasUserReactedAsync(entityId, userId, false, typeof(Movie)))
            throw new AlreadyDislikedException<Movie>();

        if (await _likeRepo.HasUserReactedAsync(entityId, userId, true, typeof(Movie)))
        {
            await _likeRepo.RemoveUserReactionAsync(entityId, userId, true, typeof(Movie));
            movie.LikeCount = Math.Max(0, movie.LikeCount - 1);
        }

        await _likeRepo.AddUserReactionAsync(entityId, userId, false, typeof(Movie));
        movie.DislikeCount++;
        bool result = await _movieRepo.SaveAsync() > 0;

        if (result)
            await _cacheService.RemoveAsync($"movie_{entityId}");

        return result;
    }

    public async Task<bool> UndoLikeAsync(int entityId, string userId)
    {
        var movie = await _movieRepo.GetByIdAsync(entityId, false);
        if (movie == null)
            throw new NotFoundException<Movie>();

        if (!await _likeRepo.HasUserReactedAsync(entityId, userId, true, typeof(Movie)))
            throw new NotLikedException<Movie>();

        await _likeRepo.RemoveUserReactionAsync(entityId, userId, true, typeof(Movie));
        movie.LikeCount = Math.Max(0, movie.LikeCount - 1);

        bool result = await _movieRepo.SaveAsync() > 0;

        if (result)
            await _cacheService.RemoveAsync($"movie_{entityId}");

        return result;
    }

    public async Task<bool> UndoDislikeAsync(int entityId, string userId)
    {
        var movie = await _movieRepo.GetByIdAsync(entityId, false);
        if (movie == null)
            throw new NotFoundException<Movie>();

        if (!await _likeRepo.HasUserReactedAsync(entityId, userId, false, typeof(Movie)))
            throw new NotDislikedException<Movie>();

        await _likeRepo.RemoveUserReactionAsync(entityId, userId, false, typeof(Movie));
        movie.LikeCount = Math.Max(0, movie.LikeCount - 1);

        bool result = await _movieRepo.SaveAsync() > 0;

        if (result)
            await _cacheService.RemoveAsync($"movie_{entityId}");

        return result;
    }
}

