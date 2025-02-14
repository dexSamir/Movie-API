using MovieApp.Core.Entities;

namespace MovieApp.BL.Services.Interfaces;
public interface IRatingService
{
    Task<bool> RateMovieAsync(int movieId, int score);
    Task<bool> RateSerieAsync(int serieId, int score);
    Task<bool> RateEpisodeAsync(int episodeId, int score);

    Task<bool> UpdateRatingAsync(int ratingId, int newScore);

    Task<bool> DeleteRatingAsync(int ratingId);

    Task<IEnumerable<Rating>> GetUserRatingsAsync(); 
    Task<double> GetAverageRatingForMovieAsync(int movieId);
    Task<double> GetAverageRatingForSerieAsync(int serieId);
    Task<double> GetAverageRatingForEpisodeAsync(int episodeId);
    Task<double> GetHybridRatingForSerieAsync(int serieId); 
}

