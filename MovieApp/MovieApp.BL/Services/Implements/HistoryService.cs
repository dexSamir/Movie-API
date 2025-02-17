using AutoMapper;
using Microsoft.EntityFrameworkCore.Migrations;
using MovieApp.BL.DTOs.HistoryDtos;
using MovieApp.BL.Exceptions.AuthException;
using MovieApp.BL.Exceptions.Common;
using MovieApp.BL.ExternalServices.Interfaces;
using MovieApp.BL.Services.Interfaces;
using MovieApp.Core.Entities;
using MovieApp.Core.Repositories;

namespace MovieApp.BL.Services.Implements;
public class HistoryService : IHistoryService
{
    readonly IHistoryRepository _repo;
    readonly ICacheService _cacheService;
    readonly ICurrentUser _currentUser;
    readonly IMapper _mapper;

    public HistoryService(IHistoryRepository repo, ICacheService cacheService, ICurrentUser currentUser, IMapper mapper)
    {
        _repo = repo;
        _cacheService = cacheService;
        _currentUser = currentUser;
        _mapper = mapper;
    }

    private readonly string[] _includeProperties =
    {
        "Movie", "Serie", "Episode", "User"
    };

    private async Task<string> GetUserIdAsync()
    {
        var userId = _currentUser.GetId();
        if (userId == null)
            throw new AuthorisationException<User>();
        return await Task.FromResult(userId);
    }

    public async Task<IEnumerable<HistoryGetDto>> GetUserWatchHistoryAsync()
    {
        var userId = await GetUserIdAsync();

        var cacheKey = $"UserHistory_{userId}";
        return await _cacheService.GetOrSetAsync(cacheKey, async () =>
        {
            var histories = await _repo.GetWhereAsync(x=> x.UserId == userId, false, _includeProperties);
            return _mapper.Map<IEnumerable<HistoryGetDto>>(histories);
        }, TimeSpan.FromMinutes(10));
    }

    public async Task<HistoryGetDto> GetLastWatchedAsync()
    {
        var userId = await GetUserIdAsync();

        var cacheKey = $"LastWatched_{userId}";
        return await _cacheService.GetOrSetAsync(cacheKey, async () =>
        {
            var lastWatched = await _repo.GetWhereAsync(x => x.UserId == userId, false, _includeProperties);
            var lastItem = lastWatched.OrderByDescending(x => x.WatchedAt).FirstOrDefault();
            return _mapper.Map<HistoryGetDto>(lastItem);
        }, TimeSpan.FromMinutes(10));

    }

    public async Task<int> AddHistoryAsync(HistoryCreateDto dto)
    {
        var userId = await GetUserIdAsync();

        var history = _mapper.Map<History>(dto);
        history.UserId = userId;
        history.WatchedAt = DateTime.UtcNow;
        await _repo.AddAsync(history);
        await _repo.SaveAsync();

        var cacheKey = $"UserHistory_{userId}";
        await _cacheService.RemoveAsync(cacheKey);
        await _repo.SaveAsync();
        return history.Id;
    }

    public async Task<bool> UpdateHistoryAsync(HistoryUpdateDto dto)
    {
        var userId = await GetUserIdAsync();

        var history = await _repo.GetByIdAsync(dto.Id);
        if (history == null || history.UserId != userId)
            throw new NotFoundException<History>();

        _mapper.Map(dto, history);
        _repo.UpdateAsync(history);

        var cacheKey = $"UserHistory_{userId}";
        await _cacheService.RemoveAsync(cacheKey);

        return await _repo.SaveAsync() > 0;
    }

    public async Task<bool> RemoveFromHistoryAsync(int historyId)
    {
        var userId = await GetUserIdAsync();

        var history = await _repo.GetByIdAsync(historyId);
        if (history == null || history.UserId != userId)
            throw new NotFoundException<History>();

        _repo.Delete(history);

        var cacheKey = $"UserHistory_{userId}";
        await _cacheService.RemoveAsync(cacheKey);

        return await _repo.SaveAsync() > 0;
    }

    public async Task<bool> ClearHistoryAsync()
    {
        var userId = await GetUserIdAsync();

        var userHistory = await _repo.GetWhereAsync(x => x.UserId == userId);
        if (!userHistory.Any())
            return false;

        _repo.DeleteRange(userHistory);

        var cacheKey = $"UserHistory_{userId}";
        await _cacheService.RemoveAsync(cacheKey);

        return await _repo.SaveAsync() > 0;
    }
}

