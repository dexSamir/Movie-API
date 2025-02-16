using MovieApp.BL.Exceptions.AuthException;
using MovieApp.BL.Exceptions.Common;
using MovieApp.BL.ExternalServices.Interfaces;
using MovieApp.BL.OtherServices.Interfaces;
using MovieApp.BL.Services.Interfaces;
using MovieApp.BL.Utilities.Enums;
using MovieApp.Core.Entities;

namespace MovieApp.BL.Services.Implements;

public class WatchProgressService : IWatchProgressService
{
    private readonly ICacheService _cache;
    readonly ICurrentUser _user;
    private readonly IDictionary<EWatchProgress, IWatchProgressStrategy> _strategies;

    public WatchProgressService(ICacheService cache, IEnumerable<IWatchProgressStrategy> strategies, ICurrentUser user)
    {
        _cache = cache;
        _user = user; 
        _strategies = strategies.ToDictionary(s => s.EntityType);
    }

    private string GetCacheKey(int movieId, string userId) => $"watchProgress_{movieId}_{userId}";

    private async Task RemoveCacheAsync(int movieId, string userId)
    {
        await _cache.RemoveAsync(GetCacheKey(movieId, userId));
    }

    private async Task<string> GetUserIdAsync()
    {
        var userId = _user.GetId();
        if (userId == null)
            throw new AuthorisationException<User>();
        return await Task.FromResult(userId);
    }

    private IWatchProgressStrategy GetStrategy(EWatchProgress type)
    {
        if (!_strategies.TryGetValue(type, out var strategy))
            throw new NotFoundException<IWatchProgressStrategy>();
        return strategy;
    }

    public async Task<bool> StartWatchingAsync(int movieId)
    {
        var userId = await GetUserIdAsync(); 
        var strategy = GetStrategy(EWatchProgress.Movie);
        var result = await strategy.StartWatchingAsync(movieId, userId);
        if (result) await RemoveCacheAsync(movieId, userId);
        return result;
    }

    public async Task<bool> UpdateWatchProgressAsync(int movieId, TimeSpan currentTime)
    {
        var userId = await GetUserIdAsync();
        var strategy = GetStrategy(EWatchProgress.Movie);
        var result = await strategy.UpdateWatchProgressAsync(movieId, userId, currentTime);
        if (result) await RemoveCacheAsync(movieId, userId);
        return result;
    }

    public async Task<bool> FinishWatchingAsync(int movieId)
    {
        var userId = await GetUserIdAsync();

        var strategy = GetStrategy(EWatchProgress.Movie);
        var result = await strategy.FinishWatchingAsync(movieId, userId);
        if (result) await RemoveCacheAsync(movieId, userId);
        return result;
    }

    public async Task<bool> PauseMovieAsync(int movieId, TimeSpan pausedAt)
    {
        var userId = await GetUserIdAsync();
        var strategy = GetStrategy(EWatchProgress.Movie);
        var result = await strategy.PauseMovieAsync(movieId, userId, pausedAt);
        if (result) await RemoveCacheAsync(movieId, userId);
        return result;
    }

    public async Task<bool> ResumeWatchingAsync(int movieId)
    {
        var userId = await GetUserIdAsync();
        var strategy = GetStrategy(EWatchProgress.Movie);
        var result = await strategy.ResumeWatchingAsync(movieId, userId);
        if (result) await RemoveCacheAsync(movieId, userId);
        return result;
    }

    public async Task<bool> IsUserWatchingAsync(int movieId)
    {
        var userId = await GetUserIdAsync();
        var cacheKey = GetCacheKey(movieId, userId);
        return await _cache.GetOrSetAsync(cacheKey, async () =>
        {
            var strategy = GetStrategy(EWatchProgress.Movie);
            return await strategy.IsUserWatchingAsync(movieId, userId);
        }, TimeSpan.FromMinutes(5));
    }

    public async Task<TimeSpan> GetCurrentTimeAsync(int movieId)
    {
        var userId = await GetUserIdAsync();
        var cacheKey = GetCacheKey(movieId, userId);
        return await _cache.GetOrSetAsync(cacheKey, async () =>
        {
            var strategy = GetStrategy(EWatchProgress.Movie);
            return await strategy.GetCurrentTimeAsync(movieId, userId);
        }, TimeSpan.FromMinutes(5));
    }

    public async Task<double> GetPlaybackSpeedAsync(int movieId)
    {
        var userId = await GetUserIdAsync();
        var cacheKey = GetCacheKey(movieId, userId);
        return await _cache.GetOrSetAsync(cacheKey, async () =>
        {
            var strategy = GetStrategy(EWatchProgress.Movie);
            return await strategy.GetPlaybackSpeedAsync(movieId, userId);
        }, TimeSpan.FromMinutes(5));
    }

    public async Task<bool> SetPlaybackSpeedAsync(int movieId, double speed)
    {
        var userId = await GetUserIdAsync();
        var strategy = GetStrategy(EWatchProgress.Movie);
        var result = await strategy.SetPlaybackSpeedAsync(movieId, userId, speed);
        if (result) await RemoveCacheAsync(movieId, userId);
        return result;
    }

    public async Task<bool> SeekForwardAsync(int movieId, TimeSpan seekTime)
    {
        var userId = await GetUserIdAsync();
        var strategy = GetStrategy(EWatchProgress.Movie);
        var result = await strategy.SeekForwardAsync(movieId, userId, seekTime);
        if (result) await RemoveCacheAsync(movieId, userId);
        return result;
    }

    public async Task<bool> SeekBackwardAsync(int movieId, TimeSpan seekTime)
    {
        var userId = await GetUserIdAsync();
        var strategy = GetStrategy(EWatchProgress.Movie);
        var result = await strategy.SeekBackwardAsync(movieId, userId, seekTime);
        if (result) await RemoveCacheAsync(movieId, userId);
        return result;
    }
}
