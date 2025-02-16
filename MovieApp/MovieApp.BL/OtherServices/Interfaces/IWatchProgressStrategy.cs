using MovieApp.BL.Utilities.Enums;

namespace MovieApp.BL.OtherServices.Interfaces;
public interface IWatchProgressStrategy
{
    EWatchProgress EntityType { get; }

    Task<bool> StartWatchingAsync(int movieId, string userId);
    Task<bool> UpdateWatchProgressAsync(int movieId, string userId, TimeSpan currentTime);
    Task<bool> FinishWatchingAsync(int movieId, string userId);
    Task<bool> PauseMovieAsync(int movieId, string userId, TimeSpan pausedAt);
    Task<bool> ResumeWatchingAsync(int movieId, string userId);
    Task<bool> IsUserWatchingAsync(int movieId, string userId);
    Task<double> GetPlaybackSpeedAsync(int movieId, string userId);
    Task<bool> SetPlaybackSpeedAsync(int movieId, string userId, double speed);
    Task<bool> SeekForwardAsync(int movieId, string userId, TimeSpan seekTime);
    Task<bool> SeekBackwardAsync(int movieId, string userId, TimeSpan seekTime);
    Task<TimeSpan> GetCurrentTimeAsync(int movieId, string userId);
}

