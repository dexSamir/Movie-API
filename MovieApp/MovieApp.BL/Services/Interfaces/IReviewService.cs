using MovieApp.BL.DTOs.ReactionDtos;
using MovieApp.BL.DTOs.ReviewDtos;
using MovieApp.Core.Entities;

namespace MovieApp.BL.Services.Interfaces;
public interface IReviewService
{
    Task<bool> AddReviewAsync(ReviewCreateDto dto);
    Task<bool> UpdateReviewAsync(int reviewId, ReviewUpdateDto dto);
	Task<bool> DeleteReviewAsync(int reviewId);

    Task<ReviewGetDto> GetReviewByIdAsync(int reviewId);
    Task<IEnumerable<ReviewGetDto>> GetReviewsByMovieAsync(int movieId);
    Task<IEnumerable<ReviewGetDto>> GetReviewsBySerieAsync(int serieId);
    Task<IEnumerable<ReviewGetDto>> GetReviewsByEpisodeAsync(int episodeId);

    Task<IEnumerable<ReviewGetDto>> GetReviewsByUserAsync();

    Task<bool> AddReplyAsync(ReviewCreateDto dto);
    Task<bool> UpdateReplyAsync(ReviewUpdateDto dto, int reviewId);

    Task<IEnumerable<Review>> GetRepliesByReviewAsync(int parentReviewId);

    Task<bool> LikeReviewAsync(int reviewId);
    Task<bool> DislikeReviewAsync(int reviewId);
    Task<bool> UndoLikeReviewAsync(int reviewId);
    Task<bool> UndoDislikeReviewAsync(int reviewId);
    Task<ReactionCountDto> GetReviewReactionCountAsync(int reviewId);
}

