﻿using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using MovieApp.BL.ExternalServices.Interfaces;

namespace MovieApp.BL.ExternalServices.Implements;
public class CacheService : ICacheService
{
    readonly IDistributedCache _cache;

    public CacheService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<T> GetOrSetAsync<T>(string cacheKey, Func<Task<T>> getData, TimeSpan expiration)
    {
        var cachedData = await _cache.GetStringAsync(cacheKey);
        if (!string.IsNullOrEmpty(cachedData))
            return JsonSerializer.Deserialize<T>(cachedData);

        var data = await getData();
        await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(data), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiration
        });
        return data;
    }

    public async Task RemoveAsync(string cacheKey)
    {
        await _cache.RemoveAsync(cacheKey);
    }
}

