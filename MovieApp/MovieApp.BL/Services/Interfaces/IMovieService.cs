using MovieApp.BL.DTOs.MovieDtos;
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
	Task<bool> UpdateAsync(MovieUpdateDto dto);

    Task<bool> RateMovieAsync(int userId, int movieId, double rating);
    Task<bool> AddReviewAsync(int userId, int movieId, string comment);
    //Task<IEnumerable<ReviewDto>> GetReviewsByMovieAsync(int movieId);
    Task<double> GetAverageRatingAsync(int movieId);

    Task<IEnumerable<MovieGetDto>> GetRecommendationsAsync(int userId);
    Task<IEnumerable<MovieGetDto>> GetPopularMoviesAsync();
    Task<IEnumerable<MovieGetDto>> GetRecentlyAddedMoviesAsync();

    Task<bool> AddToFavoritesAsync(int userId, int movieId);
    Task<bool> RemoveFromFavoritesAsync(int userId, int movieId);
    Task<IEnumerable<MovieGetDto>> GetFavoritesAsync(int userId);

    Task<int> GetTotalMovieCountAsync();
    Task<int> GetTotalWatchCountAsync(int movieId);
    Task<IEnumerable<MovieGetDto>> GetTopRatedMoviesAsync(int count);
    Task<IEnumerable<MovieGetDto>> GetMostWatchedMoviesAsync(int count);

    Task<(int likeCount, int dislikeCount)> GetMovieLikeDislikeCountAsync(int movieId);

    Task<string> GetMovieDownloadUrlAsync(int movieId);
    Task<bool> ShareMovieAsync(int movieId, string socialMediaPlatform);

    Task<bool> UpdatePosterUrlAsync(int movieId, string posterUrl);
    Task<bool> UpdateTrailerUrlAsync(int movieId, string trailerUrl);
    Task<bool> SubscribeToMovieNotificationsAsync(int userId, int movieId);

    Task<bool> NotifyUsersAboutNewMovieAsync(int movieId);
    Task<bool> UnsubscribeFromMovieNotificationsAsync(int userId, int movieId);

    Task<bool> LikeMovieAsync(int userId, int movieId);
    Task<bool> UnlikeMovieAsync(int userId, int movieId);
    Task<bool> DislikeMovieAsync(int userId, int movieId);
    Task<bool> UndislikeMovieAsync(int userId, int movieId);

    Task<IEnumerable<MovieGetDto>> SearchMoviesAsync(MovieSearchCriteria criteria);

    Task<bool> RentMovieAsync(int userId, int movieId);
    Task<IEnumerable<MovieGetDto>> GetUserRentedMoviesAsync(int userId);
    Task<bool> ExtendRentalPeriodAsync(int userId, int movieId, int additionalDays);

    Task<bool> AddToWatchHistoryAsync(int userId, int movieId);
    Task<IEnumerable<MovieGetDto>> GetUserWatchHistoryAsync(int userId);

    Task<bool> DeleteAsync(int id);
    Task<bool> DeleteRangeAsync(string ids);

    Task<bool> SoftDeleteAsync(int id);
    Task<bool> SoftDeleteRangeAsync(string ids);

    Task<bool> ReverseDeleteAsync(int id);
    Task<bool> ReverseDeleteRangeAsync(string ids);

    Task<bool> StartWatchingAsync(int userId, int movieId);
    Task<bool> UpdateWatchProgressAsync(int userId, int movieId, TimeSpan currentTime);
    Task<bool> FinishWatchingAsync(int userId, int movieId);
    //Task<IEnumerable<WatchHistoryDto>> GetWatchHistoryAsync(int userId);
    Task<bool> PauseMovieAsync(int userId, int movieId, TimeSpan pausedAt);
    Task<bool> ResumeWatchingAsync(int userId, int movieId);

    Task<bool> IsUserWatchingAsync(int userId, int movieId);

}

