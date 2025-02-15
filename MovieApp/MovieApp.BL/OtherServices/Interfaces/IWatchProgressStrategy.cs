using MovieApp.BL.Utilities.Enums;

namespace MovieApp.BL.OtherServices.Interfaces;
public interface IWatchProgressStrategy
{
    EWatchProgress EntityType { get; }

    Task<bool> StartWatchingAsync(int movieId);
    Task<bool> UpdateWatchProgressAsync(int movieId, TimeSpan currentTime);
    Task<bool> FinishWatchingAsync(int movieId);
    Task<bool> PauseMovieAsync(int movieId, TimeSpan pausedAt);
    Task<bool> ResumeWatchingAsync(int movieId);
    Task<bool> IsUserWatchingAsync(int movieId);
    Task<double> GetPlaybackSpeedAsync(int movieId);
    Task<bool> SetPlaybackSpeedAsync(int movieId, double speed);
    Task<bool> SeekForwardAsync(int movieId, TimeSpan seekTime);
    Task<bool> SeekBackwardAsync(int movieId, TimeSpan seekTime);
    Task<TimeSpan> GetCurrentTimeAsync(int movieId);
}

