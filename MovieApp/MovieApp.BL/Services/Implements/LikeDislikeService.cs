using MovieApp.BL.Exceptions.AuthException;
using MovieApp.BL.Exceptions.Common;
using MovieApp.BL.ExternalServices.Interfaces;
using MovieApp.BL.OtherServices.Interfaces;
using MovieApp.BL.Utilities.Enums;
using MovieApp.Core.Entities;

namespace MovieApp.BL.Services.Interfaces;
public class LikeDislikeService : ILikeDislikeService
{
    private readonly ICurrentUser _user;
    private readonly IDictionary<EReactionEntityType, IReactionStrategy> _strategies;

    public LikeDislikeService(ICurrentUser user, IEnumerable<IReactionStrategy> strategies)
    {
        _user = user;
        _strategies = strategies.ToDictionary(s => s.EntityType);
    }

    private async Task<string> GetUserIdAsync()
    {
        var userId = _user.GetId();
        if (userId == null)
            throw new AuthorisationException<User>();
        return await Task.FromResult(userId);
    }

    public async Task<(int LikeCount, int DislikeCount)> GetLikeDislikeCountAsync(EReactionEntityType entityType, int entityId)
    {
        if (!_strategies.TryGetValue(entityType, out var strategy))
            throw new InvalidEntityTypeException();

        return await strategy.GetReactionCountAsync(entityId);
    }

    public async Task<bool> LikeAsync(EReactionEntityType entityType, int entityId)
    {
        var userId = await GetUserIdAsync();
        if (!_strategies.TryGetValue(entityType, out var strategy))
            throw new InvalidEntityTypeException();

        return await strategy.LikeAsync(entityId, userId);
    }

    public async Task<bool> DislikeAsync(EReactionEntityType entityType, int entityId)
    {
        var userId = await GetUserIdAsync();
        if (!_strategies.TryGetValue(entityType, out var strategy))
            throw new InvalidEntityTypeException();

        return await strategy.DislikeAsync(entityId, userId);
    }

    public async Task<bool> UndoLikeAsync(EReactionEntityType entityType, int entityId)
    {
        var userId = await GetUserIdAsync();
        if (!_strategies.TryGetValue(entityType, out var strategy))
            throw new InvalidEntityTypeException();

        return await strategy.UndoLikeAsync(entityId, userId);
    }

    public async Task<bool> UndoDislikeAsync(EReactionEntityType entityType, int entityId)
    {
        var userId = await GetUserIdAsync();
        if (!_strategies.TryGetValue(entityType, out var strategy))
            throw new InvalidEntityTypeException();

        return await strategy.UndoDislikeAsync(entityId, userId);
    }
}

