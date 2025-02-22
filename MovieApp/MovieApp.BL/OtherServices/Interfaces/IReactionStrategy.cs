using MovieApp.BL.DTOs.ReactionDtos;
using MovieApp.BL.Utilities.Enums;

namespace MovieApp.BL.OtherServices.Interfaces;
public interface IReactionStrategy
{
    EReactionEntityType EntityType { get; }

    Task<ReactionCountDto> GetReactionCountAsync(int entityId);
    Task<bool> LikeAsync(int entityId, string userId);
    Task<bool> DislikeAsync(int entityId, string userId);
    Task<bool> UndoLikeAsync(int entityId, string userId);
    Task<bool> UndoDislikeAsync(int entityId, string userId);
}

