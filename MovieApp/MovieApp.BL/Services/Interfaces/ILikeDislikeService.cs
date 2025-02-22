using MovieApp.BL.DTOs.ReactionDtos;
using MovieApp.BL.Utilities.Enums;

namespace MovieApp.BL.Services.Interfaces;
public interface ILikeDislikeService
{
    Task<ReactionCountDto> GetLikeDislikeCountAsync(EReactionEntityType entityType, int entityId);
    Task<bool> LikeAsync(EReactionEntityType entityType, int entityId);
    Task<bool> DislikeAsync(EReactionEntityType entityType, int entityId);
    Task<bool> UndoLikeAsync(EReactionEntityType entityType, int entityId);
    Task<bool> UndoDislikeAsync(EReactionEntityType entityType, int entityId); 
}

