namespace MovieApp.BL.Services.Interfaces;
public interface IWatchProgressService
{
    Task<bool> StartWatchingAsync(int entityId);
    Task<bool> UpdateWatchProgressAsync(int entityId, TimeSpan currentTime);
    Task<bool> FinishWatchingAsync(int entityId);
    Task<bool> PauseMovieAsync(int entityId, TimeSpan pausedAt);
    Task<bool> ResumeWatchingAsync(int entityId);
    Task<bool> IsUserWatchingAsync(int entityId);
    Task<double> GetPlaybackSpeedAsync(int entityId);
    Task<bool> SetPlaybackSpeedAsync(int entityId, double speed);
    Task<bool> SeekForwardAsync(int entityId, TimeSpan seekTime);
    Task<bool> SeekBackwardAsync(int entityId, TimeSpan seekTime);
    Task<TimeSpan> GetCurrentTimeAsync(int entityId);
}

