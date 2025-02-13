namespace MovieApp.BL.Services.Interfaces;
public interface IRatingService
{
    Task<bool> RateMovieAsync(int movieId, int score);
    Task<bool> RateSerieAsync(int serieId, int score);
    Task<bool> RateEpisodeAsync(int episodeId, int score);
    Task<double> GetAverageRatingForMovieAsync(int movieId);
    Task<double> GetAverageRatingForSerieAsync(int serieId);
    Task<double> GetAverageRatingForEpisodeAsync(int episodeId);
}

