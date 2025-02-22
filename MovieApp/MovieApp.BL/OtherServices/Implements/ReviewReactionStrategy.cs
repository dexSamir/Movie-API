using AutoMapper;
using MovieApp.BL.DTOs.ReactionDtos;
using MovieApp.BL.Exceptions.Common;
using MovieApp.BL.Exceptions.LikeOrDislike;
using MovieApp.BL.OtherServices.Interfaces;
using MovieApp.BL.Utilities.Enums;
using MovieApp.Core.Entities;
using MovieApp.Core.Repositories;

namespace MovieApp.BL.OtherServices.Implements;
public class ReviewReactionStrategy : IReactionStrategy 
{
    public EReactionEntityType EntityType => EReactionEntityType.Review;

    readonly IReviewRepository _reviewRepo;
    readonly ILikeDislikeRepository _likeRepo;
    readonly IMapper _mapper; 
    public ReviewReactionStrategy(IReviewRepository reviewRepo, ILikeDislikeRepository likeRepo, IMapper mapper)
    {
        _mapper = mapper; 
        _reviewRepo = reviewRepo;
        _likeRepo = likeRepo;
    }

    public async Task<ReactionCountDto> GetReactionCountAsync(int entityId)
    {
        var review = await _reviewRepo.GetByIdAsync(entityId);
        if (review == null)
            throw new NotFoundException<Review>();

        return _mapper.Map<ReactionCountDto>(review);
    }

    public async Task<bool> LikeAsync(int entityId, string userId)
    {
        var movie = await _reviewRepo.GetByIdAsync(entityId);
        if (movie == null)
            throw new NotFoundException<Review>();

        if (await _likeRepo.HasUserReactedAsync(entityId, userId, true, typeof(Review)))
            throw new AlreadyLikedException<Review>();

        if (await _likeRepo.HasUserReactedAsync(entityId, userId, false, typeof(Review)))
        {
            await _likeRepo.RemoveUserReactionAsync(entityId, userId, false, typeof(Review));
            movie.DislikeCount--;
        }

        await _likeRepo.AddUserReactionAsync(entityId, userId, true, typeof(Review));
        movie.LikeCount++;

        return await _reviewRepo.SaveAsync() > 0;
    }

    public async Task<bool> DislikeAsync(int entityId, string userId)
    {
        var movie = await _reviewRepo.GetByIdAsync(entityId);
        if (movie == null)
            throw new NotFoundException<Review>();

        if (await _likeRepo.HasUserReactedAsync(entityId, userId, false, typeof(Review)))
            throw new AlreadyDislikedException<Review>();

        if (await _likeRepo.HasUserReactedAsync(entityId, userId, true, typeof(Review)))
        {
            await _likeRepo.RemoveUserReactionAsync(entityId, userId, true, typeof(Review));
            movie.LikeCount--;
        }

        await _likeRepo.AddUserReactionAsync(entityId, userId, false, typeof(Review));
        movie.DislikeCount++;

        return await _reviewRepo.SaveAsync() > 0;
    }

    public async Task<bool> UndoLikeAsync(int entityId, string userId)
    {
        var movie = await _reviewRepo.GetByIdAsync(entityId);
        if (movie == null)
            throw new NotFoundException<Review>();

        if (!await _likeRepo.HasUserReactedAsync(entityId, userId, true, typeof(Review)))
            throw new NotLikedException<Review>();

        await _likeRepo.RemoveUserReactionAsync(entityId, userId, true, typeof(Review));
        movie.LikeCount--;

        return await _reviewRepo.SaveAsync() > 0;
    }

    public async Task<bool> UndoDislikeAsync(int entityId, string userId)
    {
        var movie = await _reviewRepo.GetByIdAsync(entityId);
        if (movie == null)
            throw new NotFoundException<Review>();

        if (!await _likeRepo.HasUserReactedAsync(entityId, userId, false, typeof(Review)))
            throw new NotDislikedException<Review>();

        await _likeRepo.RemoveUserReactionAsync(entityId, userId, false, typeof(Review));
        movie.DislikeCount--;

        return await _reviewRepo.SaveAsync() > 0;
    }
}

