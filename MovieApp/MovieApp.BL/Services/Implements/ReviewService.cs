using AutoMapper;
using MovieApp.BL.DTOs.ReviewDtos;
using MovieApp.BL.Exceptions.AuthException;
using MovieApp.BL.Exceptions.Common;
using MovieApp.BL.ExternalServices.Interfaces;
using MovieApp.BL.Services.Interfaces;
using MovieApp.BL.Utilities.Enums;
using MovieApp.Core.Entities;
using MovieApp.Core.Repositories;

namespace MovieApp.BL.Services.Implements;
public class ReviewService : IReviewService
{
	readonly IReviewRepository _repo;
	readonly ICurrentUser _user;
    readonly ICacheService _cache;
    readonly IMapper _mapper;
    readonly ILikeDislikeService _like;


    readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(10);
    private readonly string[] _includeProperties =
    {
        "Movie", "Serie", "Episode", "User",
        "Rating", "Review"
    };

    public ReviewService(IReviewRepository repo, ICurrentUser user, ICacheService cache, IMapper mapper, ILikeDislikeService like)
    {
        _like = like;
        _cache = cache; 
		_user = user;
        _mapper = mapper;
        _repo = repo;
	}

    public async Task<bool> AddReviewAsync(ReviewCreateDto dto)
	{
		var userId = _user.GetId();
		if (userId == null) throw new AuthorisationException<User>();

        var review = _mapper.Map<Review>(dto);
        review.UserId = userId;
        review.ReviewDate = DateTime.UtcNow;

        await _repo.AddAsync(review);
		return await _repo.SaveAsync() > 0; 
	}

	public async Task<bool> UpdateReviewAsync(int reviewId, ReviewUpdateDto dto)
	{
		var review = await _repo.GetByIdAsync(reviewId, false, _includeProperties);
		if (review == null) throw new NotFoundException<Review>();

        var userId = _user.GetId();
        if (review.UserId != userId)
            throw new ForbiddenException<Review>();

		review.Content = dto.NewContent;
		review.UpdatedTime = DateTime.UtcNow;

		return await _repo.SaveAsync() > 0; 
    }

	public async Task<bool> DeleteReviewAsync(int reviewId)
	{
        var review = await _repo.GetByIdAsync(reviewId, false, _includeProperties);
        if (review == null) throw new NotFoundException<Review>();

        var userId = _user.GetId();
        if (review.UserId != userId)
            throw new ForbiddenException<Review>();

		_repo.Delete(review);
		return await _repo.SaveAsync() > 0; 
    }

    public async Task<ReviewGetDto> GetReviewByIdAsync(int reviewId)
    {
        var review = await _repo.GetByIdAsync(reviewId, false, _includeProperties);
        if (review == null)
            throw new NotFoundException<Review>();

        return _mapper.Map<ReviewGetDto>(review);
    }

    public async Task<IEnumerable<ReviewGetDto>> GetReviewsByMovieAsync(int movieId)
    {
        string cacheKey = $"reviews_movie_{movieId}";
        var reviews = await _cache.GetOrSetAsync(cacheKey, async () =>
        {
            return await _repo.GetWhereAsync(r => r.MovieId == movieId, _includeProperties);
        }, _cacheDuration);

        return _mapper.Map<IEnumerable<ReviewGetDto>>(reviews);
    }


    public async Task<IEnumerable<ReviewGetDto>> GetReviewsBySerieAsync(int serieId)
    {
        string cacheKey = $"reviews_serie_{serieId}";
        var reviews =  await _cache.GetOrSetAsync(cacheKey, async () =>
        {
            return await _repo.GetWhereAsync(r => r.SerieId == serieId, _includeProperties);
        }, _cacheDuration);

        return _mapper.Map<IEnumerable<ReviewGetDto>>(reviews);
    }

    public async Task<IEnumerable<ReviewGetDto>> GetReviewsByEpisodeAsync(int episodeId)
    {
        string cacheKey = $"reviews_episode_{episodeId}";
        var reviews =  await _cache.GetOrSetAsync(cacheKey, async () =>
        {
            return await _repo.GetWhereAsync(r => r.EpisodeId == episodeId, _includeProperties);
        }, _cacheDuration);

        return _mapper.Map<IEnumerable<ReviewGetDto>>(reviews);
    }

    public async Task<IEnumerable<ReviewGetDto>> GetReviewsByUserAsync(string userId)
    {
        string cacheKey = $"reviews_user_{userId}";
        var reviews =  await _cache.GetOrSetAsync(cacheKey, async () =>
        {
            return await _repo.GetWhereAsync(r => r.UserId == userId, _includeProperties);
        }, _cacheDuration);

        return _mapper.Map<IEnumerable<ReviewGetDto>>(reviews);
    }

    public async Task<bool> AddReplyAsync(ReviewCreateDto dto)
    {
        var userId = _user.GetId();
        if (userId == null) throw new AuthorisationException<User>();

        var parentReview = await _repo.GetByIdAsync(dto.ParentReviewId.Value);
        if (parentReview == null) throw new NotFoundException<Review>();


        var reply = _mapper.Map<Review>(dto);
        reply.ParentReviewId = dto.ParentReviewId;
        reply.UserId = userId;
        reply.ReviewDate = DateTime.UtcNow;

        await _repo.AddAsync(reply);
        return await _repo.SaveAsync() > 0;
    }

    public async Task<bool> UpdateReplyAsync(ReviewUpdateDto dto, int reviewId)
    {
        var reply = await _repo.GetByIdAsync(reviewId);
        if (reply == null) throw new NotFoundException<Review>();

        var userId = _user.GetId();
        if (reply.UserId != userId)
            throw new ForbiddenException<Review>();

        reply.Content = dto.NewContent;
        reply.ParentReviewId = dto.ParentReviewId; 
        reply.UpdatedTime = DateTime.UtcNow;

        return await _repo.SaveAsync() > 0;
    }

    public async Task<IEnumerable<Review>> GetRepliesByReviewAsync(int parentReviewId)
    {
        return await _repo.GetWhereAsync(r => r.ParentReviewId == parentReviewId, _includeProperties);
    }


    //Like And Dislike
    public async Task<bool> LikeReviewAsync(int reviewId)
        => await _like.LikeAsync(EReactionEntityType.Review, reviewId);

    public async Task<bool> DislikeReviewAsync(int reviewId)
        => await _like.DislikeAsync(EReactionEntityType.Review, reviewId);

    public async Task<bool> UndoLikeReviewAsync(int reviewId)
        => await _like.UndoLikeAsync(EReactionEntityType.Review, reviewId);

    public async Task<bool> UndoDislikeReviewAsync(int reviewId)
        => await _like.UndoDislikeAsync(EReactionEntityType.Review, reviewId);

    public async Task<(int LikeCount, int DislikeCount)> GetReviewReactionCountAsync(int reviewId)
        => await _like.GetLikeDislikeCountAsync(EReactionEntityType.Review, reviewId);
}

