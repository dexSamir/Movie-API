using MovieApp.BL.Exceptions.AuthException;
using MovieApp.BL.Exceptions.Common;
using MovieApp.BL.ExternalServices.Interfaces;
using MovieApp.BL.Services.Interfaces;
using MovieApp.Core.Entities;
using MovieApp.Core.Repositories;

namespace MovieApp.BL.Services.Implements;

public class WatchProgressService : IWatchProgressService
{
    readonly IWatchProgressRepository _repo;
    readonly ICurrentUser _user;
    readonly ICacheService _cache;
    private readonly string[] _includeProperties = { "Movie", "Serie", "Episode", "User" };

    public WatchProgressService(IWatchProgressRepository repo, ICurrentUser user, ICacheService cache)
    {
        _repo = repo;
        _user = user;
        _cache = cache;
    }

    private async Task<string> GetUserIdAsync()
    {
        var userId = _user.GetId();
        if (userId == null)
            throw new AuthorisationException<User>();
        return await Task.FromResult(userId);
    }

    private async Task<WatchProgress> GetWatchProgressAsync(int movieId, string userId)
        => await _repo.GetFirstAsync(wp => wp.MovieId == movieId && wp.UserId == userId, false, _includeProperties);

    private string GetCacheKey(int movieId, string userId)
    {
        return $"watchProgress_{movieId}_{userId}";
    }

    private async Task RemoveCacheAsync(int movieId, string userId)
    {
        await _cache.RemoveAsync(GetCacheKey(movieId, userId));
    }

    public async Task<bool> StartWatchingAsync(int movieId)
    {
        var userId = await GetUserIdAsync();

        var existingProgress = await GetWatchProgressAsync(movieId, userId);
        if (existingProgress != null)
            return false;

        var watchProgress = new WatchProgress
        {
            MovieId = movieId,
            UserId = userId,
            StartTime = DateTime.UtcNow,
            IsWatching = true,
            PlaybackSpeed = 1.0,
            CurrentTime = TimeSpan.Zero
        };

        await _repo.AddAsync(watchProgress);
        var result = await _repo.SaveAsync() > 0;
        if (result)
            await RemoveCacheAsync(movieId, userId);

        return result;
    }

    public async Task<bool> UpdateWatchProgressAsync(int movieId, TimeSpan currentTime)
    {
        var userId = await GetUserIdAsync();

        var progress = await GetWatchProgressAsync(movieId, userId);
        if (progress == null)
            throw new NotFoundException<WatchProgress>();

        progress.CurrentTime = currentTime;
        progress.LastUpdated = DateTime.UtcNow;

        var result = await _repo.SaveAsync() > 0;
        if (result)
            await RemoveCacheAsync(movieId, userId);

        return result;
    }

    public async Task<bool> FinishWatchingAsync(int movieId)
    {
        var userId = await GetUserIdAsync();

        var progress = await GetWatchProgressAsync(movieId, userId);
        if (progress == null)
            throw new NotFoundException<WatchProgress>();

        progress.IsWatching = false;
        progress.EndTime = DateTime.UtcNow;

        var result = await _repo.SaveAsync() > 0;
        if (result)
            await RemoveCacheAsync(movieId, userId);

        return result;
    }

    public async Task<bool> PauseMovieAsync(int movieId, TimeSpan pausedAt)
    {
        var userId = await GetUserIdAsync();

        var progress = await GetWatchProgressAsync(movieId, userId);
        if (progress == null)
            throw new NotFoundException<WatchProgress>();

        progress.IsWatching = false;
        progress.PausedAt = pausedAt;
        progress.LastUpdated = DateTime.UtcNow;

        var result = await _repo.SaveAsync() > 0;
        if (result)
            await RemoveCacheAsync(movieId, userId);

        return result;
    }

    public async Task<bool> ResumeWatchingAsync(int movieId)
    {
        var userId = await GetUserIdAsync();

        var progress = await GetWatchProgressAsync(movieId, userId);
        if (progress == null)
            throw new NotFoundException<WatchProgress>();

        progress.IsWatching = true;
        progress.LastUpdated = DateTime.UtcNow;

        var result = await _repo.SaveAsync() > 0;
        if (result)
            await RemoveCacheAsync(movieId, userId);

        return result;
    }

    public async Task<bool> IsUserWatchingAsync(int movieId)
    {
        var userId = await GetUserIdAsync();
        var cacheKey = GetCacheKey(movieId, userId);

        return await _cache.GetOrSetAsync(cacheKey, async () =>
        {
            var progress = await GetWatchProgressAsync(movieId, userId);
            return progress?.IsWatching ?? false;
        }, TimeSpan.FromMinutes(5));
    }

    public async Task<TimeSpan> GetCurrentTimeAsync(int movieId)
    {
        var userId = await GetUserIdAsync();
        var cacheKey = GetCacheKey(movieId, userId);

        return await _cache.GetOrSetAsync(cacheKey, async () =>
        {
            var progress = await GetWatchProgressAsync(movieId, userId);
            return progress?.CurrentTime ?? TimeSpan.Zero;
        }, TimeSpan.FromMinutes(5));
    }

    public async Task<double> GetPlaybackSpeedAsync(int movieId)
    {
        var userId = await GetUserIdAsync();
        var cacheKey = GetCacheKey(movieId, userId);

        return await _cache.GetOrSetAsync(cacheKey, async () =>
        {
            var progress = await GetWatchProgressAsync(movieId, userId);
            return progress?.PlaybackSpeed ?? 1.0;
        }, TimeSpan.FromMinutes(5));
    }

    public async Task<bool> SetPlaybackSpeedAsync(int movieId, double speed)
    {
        var userId = await GetUserIdAsync();

        var progress = await GetWatchProgressAsync(movieId, userId);
        if (progress == null)
            throw new NotFoundException<WatchProgress>();

        progress.PlaybackSpeed = speed;
        progress.LastUpdated = DateTime.UtcNow;

        var result = await _repo.SaveAsync() > 0;
        if (result)
            await RemoveCacheAsync(movieId, userId);

        return result;
    }

    public async Task<bool> SeekForwardAsync(int movieId, TimeSpan seekTime)
    {
        var userId = await GetUserIdAsync();

        var progress = await GetWatchProgressAsync(movieId, userId);
        if (progress == null)
            throw new NotFoundException<WatchProgress>();

        progress.CurrentTime += seekTime;
        progress.LastUpdated = DateTime.UtcNow;

        var result = await _repo.SaveAsync() > 0;
        if (result)
            await RemoveCacheAsync(movieId, userId);

        return result;
    }

    public async Task<bool> SeekBackwardAsync(int movieId, TimeSpan seekTime)
    {
        var userId = await GetUserIdAsync();

        var progress = await GetWatchProgressAsync(movieId, userId);
        if (progress == null)
            throw new NotFoundException<WatchProgress>();

        progress.CurrentTime -= seekTime;
        if (progress.CurrentTime < TimeSpan.Zero)
            progress.CurrentTime = TimeSpan.Zero;

        progress.LastUpdated = DateTime.UtcNow;

        var result = await _repo.SaveAsync() > 0;
        if (result)
            await RemoveCacheAsync(movieId, userId);

        return result;
    }
}
