using MovieApp.BL.Exceptions.Common;
using MovieApp.BL.Exceptions.LikeOrDislike;
using MovieApp.BL.OtherServices.Interfaces;
using MovieApp.BL.Utilities.Enums;
using MovieApp.Core.Entities;
using MovieApp.Core.Repositories;

namespace MovieApp.BL.OtherServices.Implements;
public class MovieReactionStrategy : IReactionStrategy
{
    public EReactionEntityType EntityType => EReactionEntityType.Movie;

    private readonly IMovieRepository _movieRepo;
    private readonly ILikeDislikeRepository _likeRepo;

    public MovieReactionStrategy(IMovieRepository movieRepo, ILikeDislikeRepository likeRepo)
    {
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
        var movie = await _movieRepo.GetByIdAsync(entityId);
        if (movie == null)
            throw new NotFoundException<Movie>();

        if (await _likeRepo.HasUserReactedAsync(entityId, userId, true))
            throw new AlreadyLikedException<Movie>();

        if (await _likeRepo.HasUserReactedAsync(entityId, userId, false))
        {
            await _likeRepo.RemoveUserReactionAsync(entityId, userId, false);
            movie.DislikeCount--;
        }

        await _likeRepo.AddUserReactionAsync(entityId, userId, true);
        movie.LikeCount++;

        return await _movieRepo.SaveAsync() > 0;
    }

    public async Task<bool> DislikeAsync(int entityId, string userId)
    {
        var movie = await _movieRepo.GetByIdAsync(entityId);
        if (movie == null)
            throw new NotFoundException<Movie>();

        if (await _likeRepo.HasUserReactedAsync(entityId, userId, false))
            throw new AlreadyDislikedException<Movie>();

        if (await _likeRepo.HasUserReactedAsync(entityId, userId, true))
        {
            await _likeRepo.RemoveUserReactionAsync(entityId, userId, true);
            movie.LikeCount--;
        }

        await _likeRepo.AddUserReactionAsync(entityId, userId, false);
        movie.DislikeCount++;

        return await _movieRepo.SaveAsync() > 0;
    }

    public async Task<bool> UndoLikeAsync(int entityId, string userId)
    {
        var movie = await _movieRepo.GetByIdAsync(entityId);
        if (movie == null)
            throw new NotFoundException<Movie>();

        if (!await _likeRepo.HasUserReactedAsync(entityId, userId, true))
            throw new NotLikedException<Movie>();

        await _likeRepo.RemoveUserReactionAsync(entityId, userId, true);
        movie.LikeCount--;

        return await _movieRepo.SaveAsync() > 0;
    }

    public async Task<bool> UndoDislikeAsync(int entityId, string userId)
    {
        var movie = await _movieRepo.GetByIdAsync(entityId);
        if (movie == null)
            throw new NotFoundException<Movie>();

        if (!await _likeRepo.HasUserReactedAsync(entityId, userId, false))
            throw new NotDislikedException<Movie>();

        await _likeRepo.RemoveUserReactionAsync(entityId, userId, false);
        movie.DislikeCount--;

        return await _movieRepo.SaveAsync() > 0;
    }
}

