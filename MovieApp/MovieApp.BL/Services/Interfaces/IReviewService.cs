using MovieApp.BL.DTOs.ReviewDtos;
using MovieApp.Core.Entities;

namespace MovieApp.BL.Services.Interfaces;
public interface IReviewService
{
    Task<bool> AddReviewAsync(ReviewCreateDto dto);
    Task<bool> UpdateReviewAsync(int reviewId, ReviewUpdateDto dto);
	Task<bool> DeleteReviewAsync(int reviewId);

    Task<Review> GetReviewByIdAsync(int reviewId);
    Task<IEnumerable<Review>> GetReviewsByMovieAsync(int movieId);
    Task<IEnumerable<Review>> GetReviewsBySerieAsync(int serieId);
    Task<IEnumerable<Review>> GetReviewsByEpisodeAsync(int episodeId);

    Task<IEnumerable<Review>> GetReviewsByUserAsync(string userId);

    Task<bool> AddReplyAsync(ReviewCreateDto dto);
    Task<bool> UpdateReplyAsync(ReviewUpdateDto dto, int reviewId);

    Task<IEnumerable<Review>> GetRepliesByReviewAsync(int parentReviewId);

    Task<bool> LikeReviewAsync(int reviewId);
    Task<bool> DislikeReviewAsync(int reviewId);
    Task<(int LikeCount, int DislikeCount)> GetReviewLikeDislikeCountAsync(int reviewId);
}

