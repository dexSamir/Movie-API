using Microsoft.AspNetCore.Http;
using MovieApp.BL.DTOs.MovieDtos;
using MovieApp.BL.DTOs.RecommendationDtos;
using MovieApp.BL.Utilities.Enums;

namespace MovieApp.BL.Services.Interfaces;
public interface IMovieService
{
	Task<IEnumerable<MovieGetDto>> GetAllAsync();
	Task<MovieGetDto> GetByIdAsync(int id);
	Task<IEnumerable<MovieGetDto>> GetByGenre(string genre);
    Task<IEnumerable<MovieGetDto>> GetByRating(double rating);
    Task<IEnumerable<MovieGetDto>> GetByReleaseDateAsync(DateOnly releaseDate);
    Task<IEnumerable<MovieGetDto>> GetByDirectorAsync(int directorId);
    Task<IEnumerable<MovieGetDto>> GetByActorAsync(int actorId);
    Task<IEnumerable<MovieGetDto>> GetByDurationRangeAsync(int minDuration, int maxDuration);
    Task<IEnumerable<MovieGetDto>> GetByTitleAsync(string title);

    Task<IEnumerable<MovieGetDto>> SortByTitleAsync(bool ascending = true);
    Task<IEnumerable<MovieGetDto>> SortByReleaseDateAsync(bool ascending = true);
    Task<IEnumerable<MovieGetDto>> SortByRatingAsync(bool ascending = true);

    Task<int> CreateAsync(MovieCreateDto dto);
    Task<IEnumerable<int>> CreateRangeAsync(IEnumerable<MovieCreateDto> dtos); 
    Task<bool> UpdateAsync(MovieUpdateDto dto, int movieId);

    Task<bool> UpdatePosterUrlAsync(int movieId, IFormFile posterUrl);
    Task<bool> UpdateTrailerUrlAsync(int movieId, IFormFile trailerUrl);

    Task<bool> RateMovieAsync(int movieId, int score);
    Task<bool> AddReviewAsync(int movieId, string comment);
    Task<bool> UpdateRatingAsync(int ratingId, int newScore);
    Task<bool> DeleteRatingAsync(int ratingId);
    Task<double> GetAverageRatingAsync(int movieId);

    Task<bool> LikeMovieAsync(int movieId);
    Task<bool> DislikeMovieAsync(int movieId);
    Task<bool> UndoLikeMovieAsync(int movieId);
    Task<bool> UndoDislikeMovieAsync(int movieId);
    Task<(int LikeCount, int DislikeCount)> GetMovieReactionCountAsync(int movieId);

    Task<bool> DeleteAsync(string ids, EDeleteType deleteType);

    Task<bool> UpdateAverageRatingAsync(int movieId);

    Task<int> GetTotalMovieCountAsync();
    Task<int> GetTotalWatchCountAsync(int movieId);
    Task<IEnumerable<MovieGetDto>> GetTopRatedMoviesAsync(int count);
    Task<IEnumerable<MovieGetDto>> GetMostWatchedMoviesAsync(int count);

    Task<IEnumerable<RecommendationGetDto>> GetRecommendationsAsync();
    Task<IEnumerable<MovieGetDto>> GetPopularMoviesAsync();
    Task<IEnumerable<MovieGetDto>> GetRecentlyAddedMoviesAsync();

    //////////////////////////////////////////////////////////////////////////////////////////////////////


    Task<bool> AddToFavoritesAsync(int movieId);
    Task<bool> RemoveFromFavoritesAsync(int movieId);
    Task<IEnumerable<MovieGetDto>> GetFavoritesAsync();

    Task<bool> AddToWatchHistoryAsync(int movieId);
    Task<IEnumerable<MovieGetDto>> GetUserWatchHistoryAsync();

    Task<string> GetMovieDownloadUrlAsync(int movieId);
    Task<bool> ShareMovieAsync(int movieId, string socialMediaPlatform);

    Task<bool> NotifyUsersAboutNewMovieAsync(int movieId);
    Task<bool> SubscribeToMovieNotificationsAsync(int movieId);
    Task<bool> UnsubscribeFromMovieNotificationsAsync(int movieId);

    Task<IEnumerable<MovieGetDto>> SearchMoviesAsync(MovieSearchCriteria criteria);

    Task<bool> RentMovieAsync(int movieId);
    Task<IEnumerable<MovieGetDto>> GetUserRentedMoviesAsync();
    Task<bool> ExtendRentalPeriodAsync(int movieId, int additionalDays);

    //Task<IEnumerable<WatchHistoryDto>> GetWatchHistoryAsync(int userId);
}

