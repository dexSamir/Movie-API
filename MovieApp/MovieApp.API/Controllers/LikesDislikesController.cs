using Microsoft.AspNetCore.Mvc;
using MovieApp.BL.Services.Interfaces;
using MovieApp.BL.Utilities.Enums;

namespace MovieApp.API.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class LikesDislikesController : ControllerBase
{
    readonly ILikeDislikeService _service;
    public LikesDislikesController(ILikeDislikeService service)
    {
        _service = service; 
    }

    [HttpGet("count")]
    public async Task<IActionResult> GetLikeDislikeCountAsync([FromQuery] EReactionEntityType entityType, [FromQuery] int entityId)
    {
        return Ok(await _service.GetLikeDislikeCountAsync(entityType, entityId));
    }

    [HttpPost("like")]
    public async Task<IActionResult> LikeAsync([FromQuery] EReactionEntityType entityType, [FromQuery] int entityId)
    {
        return Ok(await _service.LikeAsync(entityType, entityId));
    }
    [HttpPost("dislike")]
    public async Task<IActionResult> DislikeAsync([FromQuery] EReactionEntityType entityType, [FromQuery] int entityId)
    {
        return Ok(await _service.DislikeAsync(entityType, entityId));
    }
    [HttpPost("undo-like")]
    public async Task<IActionResult> UndoLikeAsync([FromQuery] EReactionEntityType entityType, [FromQuery] int entityId)
    {
        return Ok(await _service.UndoLikeAsync(entityType, entityId));
    }

    [HttpPost("undo-dislike")]
    public async Task<IActionResult> UndoDislikeAsync([FromQuery] EReactionEntityType entityType, [FromQuery] int entityId)
    {

        return Ok(await _service.UndoDislikeAsync(entityType, entityId));
    }
}
