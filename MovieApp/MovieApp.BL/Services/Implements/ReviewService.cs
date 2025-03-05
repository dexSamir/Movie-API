using AutoMapper;
using MovieApp.BL.DTOs.ReactionDtos;
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
        "Movie", "Serie", "Episode", "User", "Replies"
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

        Review parent = null; 
        if (dto.ParentReviewId.HasValue && dto.ParentReviewId > 0)
        {
            parent = await _repo.GetByIdAsync(dto.ParentReviewId.Value, false, _includeProperties);
            if (parent is null)
                throw new NotFoundException<Review>();
        }

        var review = _mapper.Map<Review>(dto);
        review.UserId = userId;
        review.IsUpdated = false;

        await _repo.AddAsync(review);
        bool result = await _repo.SaveAsync() > 0;
        if (result)
            await _cache.RemoveAsync($"reviews_movie_{dto.MovieId}");

        return result;
    }

	public async Task<bool> UpdateReviewAsync(int reviewId, ReviewUpdateDto dto)
	{
		var review = await _repo.GetByIdAsync(reviewId, false, _includeProperties);
		if (review == null) throw new NotFoundException<Review>();

        var userId = _user.GetId();
        if (review.UserId != userId)
            throw new ForbiddenException<Review>();

        _mapper.Map(dto, review);

        bool result = await _repo.SaveAsync() > 0;
        if (result)
            await _cache.RemoveAsync($"reviews_movie_{review.MovieId}");

        return result;
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


    //GET Methods

    private ReviewGetDto MapReviewToDto(Review review)
    {
        return new ReviewGetDto
        {
            Id = review.Id,
            Content = review.Content,
            UserName = review.User?.UserName,
            MovieId = review.MovieId,
            SerieId = review.SerieId,
            EpisodeId = review.EpisodeId,
            ParentReviewId = review.ParentReviewId,
            ReviewDate = review.ReviewDate,
            IsUpdated = review.IsUpdated,
            UpdatedTime = review.UpdatedTime,
            LikeCount = review.LikeCount,
            DislikeCount = review.DislikeCount,
            Replies = review.Replies?
                .Where(r => r.ParentReviewId == review.Id)
                .Select(r => new ReviewGetDto
                {
                    Id = r.Id,
                    Content = r.Content,
                    UserName = r.User?.UserName,
                    MovieId = r.MovieId,
                    SerieId = r.SerieId,
                    EpisodeId = r.EpisodeId,
                    ParentReviewId = r.ParentReviewId,
                    ReviewDate = r.ReviewDate,
                    IsUpdated = r.IsUpdated,
                    UpdatedTime = r.UpdatedTime,
                    LikeCount = r.LikeCount,
                    DislikeCount = r.DislikeCount,
                    Replies = null
                })
                .ToList() ?? new List<ReviewGetDto>()
        };
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
        var reviewDtos = await _cache.GetOrSetAsync(cacheKey, async () =>
        {
            var reviews = await _repo.GetWhereAsync(
                r => r.MovieId == movieId && r.ParentReviewId == null,
                "Replies", "Replies.Replies", "Replies.Replies",
                "User", "Movie", "Serie", "Episode"
            );

            return reviews
            .Where(x => x.ParentReviewId == null)
            .Select(x => MapReviewToDto(x)).ToList();

        }, _cacheDuration);

        return reviewDtos; 
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

    public async Task<IEnumerable<ReviewGetDto>> GetReviewsByUserAsync()
    {
        var userId = _user.GetId(); 
        string cacheKey = $"reviews_user_{userId}";
        var reviews =  await _cache.GetOrSetAsync(cacheKey, async () =>
        {
            var reviews = await _repo.GetWhereAsync(
                r => r.UserId == userId && r.ParentReviewId == null,
                "Replies", "Replies.Replies", "Replies.Replies",
                "User", "Movie", "Serie", "Episode"
            );

            return reviews
            .Where(x => x.ParentReviewId == null)
            .Select(x => MapReviewToDto(x)).ToList();

        }, _cacheDuration);

        return reviews;
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

    public async Task<ReactionCountDto> GetReviewReactionCountAsync(int reviewId)
        => await _like.GetLikeDislikeCountAsync(EReactionEntityType.Review, reviewId);
}

