using MovieApp.BL.Exceptions.Common;
using MovieApp.BL.Exceptions.LikeOrDislike;
using MovieApp.BL.OtherServices.Interfaces;
using MovieApp.BL.Utilities.Enums;
using MovieApp.Core.Entities;
using MovieApp.Core.Repositories;

namespace MovieApp.BL.OtherServices.Implements;
public class ReviewReactionStrategy : IReactionStrategy 
{
    public EReactionEntityType EntityType => EReactionEntityType.Movie;

    private readonly IReviewRepository _reviewRepo;
    private readonly ILikeDislikeRepository _likeRepo;

    public ReviewReactionStrategy(IReviewRepository reviewRepo, ILikeDislikeRepository likeRepo)
    {
        _reviewRepo = reviewRepo;
        _likeRepo = likeRepo;
    }

    public async Task<(int LikeCount, int DislikeCount)> GetReactionCountAsync(int entityId)
    {
        var movie = await _reviewRepo.GetByIdAsync(entityId);
        if (movie == null)
            throw new NotFoundException<Review>();

        return (movie.LikeCount, movie.DislikeCount);
    }

    public async Task<bool> LikeAsync(int entityId, string userId)
    {
        var movie = await _reviewRepo.GetByIdAsync(entityId);
        if (movie == null)
            throw new NotFoundException<Review>();

        if (await _likeRepo.HasUserReactedAsync(entityId, userId, true))
            throw new AlreadyLikedException<Review>();

        if (await _likeRepo.HasUserReactedAsync(entityId, userId, false))
        {
            await _likeRepo.RemoveUserReactionAsync(entityId, userId, false);
            movie.DislikeCount--;
        }

        await _likeRepo.AddUserReactionAsync(entityId, userId, true);
        movie.LikeCount++;

        return await _reviewRepo.SaveAsync() > 0;
    }

    public async Task<bool> DislikeAsync(int entityId, string userId)
    {
        var movie = await _reviewRepo.GetByIdAsync(entityId);
        if (movie == null)
            throw new NotFoundException<Review>();

        if (await _likeRepo.HasUserReactedAsync(entityId, userId, false))
            throw new AlreadyDislikedException<Review>();

        if (await _likeRepo.HasUserReactedAsync(entityId, userId, true))
        {
            await _likeRepo.RemoveUserReactionAsync(entityId, userId, true);
            movie.LikeCount--;
        }

        await _likeRepo.AddUserReactionAsync(entityId, userId, false);
        movie.DislikeCount++;

        return await _reviewRepo.SaveAsync() > 0;
    }

    public async Task<bool> UndoLikeAsync(int entityId, string userId)
    {
        var movie = await _reviewRepo.GetByIdAsync(entityId);
        if (movie == null)
            throw new NotFoundException<Review>();

        if (!await _likeRepo.HasUserReactedAsync(entityId, userId, true))
            throw new NotLikedException<Review>();

        await _likeRepo.RemoveUserReactionAsync(entityId, userId, true);
        movie.LikeCount--;

        return await _reviewRepo.SaveAsync() > 0;
    }

    public async Task<bool> UndoDislikeAsync(int entityId, string userId)
    {
        var movie = await _reviewRepo.GetByIdAsync(entityId);
        if (movie == null)
            throw new NotFoundException<Review>();

        if (!await _likeRepo.HasUserReactedAsync(entityId, userId, false))
            throw new NotDislikedException<Review>();

        await _likeRepo.RemoveUserReactionAsync(entityId, userId, false);
        movie.DislikeCount--;

        return await _reviewRepo.SaveAsync() > 0;
    }
}

