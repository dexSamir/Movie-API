using MovieApp.BL.Exceptions.Common;
using MovieApp.BL.OtherServices.Interfaces;
using MovieApp.BL.Utilities.Enums;
using MovieApp.Core.Entities;
using MovieApp.Core.Repositories;

namespace MovieApp.BL.OtherServices.Implements;
public class MovieWatchProgressStrategy : IWatchProgressStrategy
{
    public EWatchProgress EntityType => EWatchProgress.Movie;

    readonly IMovieRepository _movieRepo;
    readonly IWatchProgressRepository _watchRepo;
    readonly string[] _includeProperties =
    {
        "Movie", "Episode", "User"
    };
    public MovieWatchProgressStrategy(IMovieRepository movieRepo, IWatchProgressRepository watchRepo)
    {
        _movieRepo = movieRepo;
        _watchRepo = watchRepo;
    }

    public async Task<bool> StartWatchingAsync(int movieId, string userId)
    {
        var existingProgress = await GetWatchProgressAsync(movieId, userId);
        if (existingProgress != null) return false;

        var watchProgress = new WatchProgress
        {
            MovieId = movieId,
            UserId = userId,
            StartTime = DateTime.UtcNow,
            IsWatching = true,
            PlaybackSpeed = 1.0,
            CurrentTime = TimeSpan.Zero
        };

        await _watchRepo.AddAsync(watchProgress);
        return await _watchRepo.SaveAsync() > 0;
    }

    public async Task<bool> UpdateWatchProgressAsync(int movieId, string userId, TimeSpan currentTime)
    {
        var progress = await GetWatchProgressAsync(movieId, userId);
        if (progress == null) throw new NotFoundException<WatchProgress>();

        progress.CurrentTime = currentTime;
        progress.LastUpdated = DateTime.UtcNow;

        return await _watchRepo.SaveAsync() > 0;
    }

    public async Task<bool> FinishWatchingAsync(int movieId, string userId)
    {
        var progress = await GetWatchProgressAsync(movieId, userId);
        if (progress == null) throw new NotFoundException<WatchProgress>();

        progress.IsWatching = false;
        progress.EndTime = DateTime.UtcNow;

        return await _watchRepo.SaveAsync() > 0;
    }

    public async Task<bool> PauseMovieAsync(int movieId, string userId, TimeSpan pausedAt)
    {
        var progress = await GetWatchProgressAsync(movieId, userId);
        if (progress == null) throw new NotFoundException<WatchProgress>();

        progress.IsWatching = false;
        progress.PausedAt = pausedAt;
        progress.LastUpdated = DateTime.UtcNow;

        return await _watchRepo.SaveAsync() > 0;
    }

    public async Task<bool> ResumeWatchingAsync(int movieId, string userId)
    {
        var progress = await GetWatchProgressAsync(movieId, userId);
        if (progress == null) throw new NotFoundException<WatchProgress>();

        progress.IsWatching = true;
        progress.LastUpdated = DateTime.UtcNow;

        return await _watchRepo.SaveAsync() > 0;
    }

    public async Task<double> GetPlaybackSpeedAsync(int movieId, string userId)
    {
        var progress = await GetWatchProgressAsync(movieId, userId);
        return progress?.PlaybackSpeed ?? 1.0;
    }

    public async Task<bool> SetPlaybackSpeedAsync(int movieId, string userId, double speed)
    {
        var progress = await GetWatchProgressAsync(movieId, userId);
        if (progress == null) throw new NotFoundException<WatchProgress>();

        progress.PlaybackSpeed = speed;
        progress.LastUpdated = DateTime.UtcNow;

        return await _watchRepo.SaveAsync() > 0;
    }

    public async Task<bool> SeekForwardAsync(int movieId, string userId, TimeSpan seekTime)
    {
        var progress = await GetWatchProgressAsync(movieId, userId);
        if (progress == null) throw new NotFoundException<WatchProgress>();

        progress.CurrentTime += seekTime;
        progress.LastUpdated = DateTime.UtcNow;

        return await _watchRepo.SaveAsync() > 0;
    }

    public async Task<bool> SeekBackwardAsync(int movieId, string userId, TimeSpan seekTime)
    {
        var progress = await GetWatchProgressAsync(movieId, userId);
        if (progress == null) throw new NotFoundException<WatchProgress>();

        progress.CurrentTime -= seekTime;
        if (progress.CurrentTime < TimeSpan.Zero)
            progress.CurrentTime = TimeSpan.Zero;

        progress.LastUpdated = DateTime.UtcNow;

        return await _watchRepo.SaveAsync() > 0;
    }

    public async Task<TimeSpan> GetCurrentTimeAsync(int movieId, string userId)
    {
        var progress = await GetWatchProgressAsync(movieId, userId);
        return progress?.CurrentTime ?? TimeSpan.Zero;
    }

    public async Task<bool> IsUserWatchingAsync(int movieId, string userId)
    {
        var progress = await GetWatchProgressAsync(movieId, userId);
        return progress?.IsWatching ?? false;
    }

    private async Task<WatchProgress?> GetWatchProgressAsync(int movieId, string userId)
        => await _watchRepo.GetFirstAsync(wp => wp.MovieId == movieId && wp.UserId == userId, false, _includeProperties);
}
