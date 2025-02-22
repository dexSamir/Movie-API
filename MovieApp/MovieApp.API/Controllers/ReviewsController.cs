using Microsoft.AspNetCore.Mvc;
using MovieApp.BL.DTOs.ReviewDtos;
using MovieApp.BL.Services.Interfaces;

namespace MovieApp.API.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class ReviewsController : ControllerBase
{
    readonly IReviewService _service;
    public ReviewsController(IReviewService service)
    {
        _service = service;
    }

    [HttpPost("add-review")]
    public async Task<IActionResult> AddReviewAsync([FromBody] ReviewCreateDto dto)
    {
        return Ok(await _service.AddReviewAsync(dto));
    }

    [HttpPut("update-review")]
    public async Task<IActionResult> UpdateReviewAsync([FromQuery] int reviewId, [FromBody] ReviewUpdateDto dto)
    {
        return Ok(await _service.UpdateReviewAsync( reviewId, dto));
    }

    [HttpDelete("delete-review")]
    public async Task<IActionResult> DeleteReviewAsync([FromQuery] int reviewId)
    {
        return Ok(await _service.DeleteReviewAsync(reviewId));
    }

    [HttpGet("get-review")]
    public async Task<IActionResult> GetReviewByIdAsync([FromQuery] int reviewId)
    {
        return Ok(await _service.GetReviewByIdAsync(reviewId));
    }

    [HttpGet("reviews-by-movie")]
    public async Task<IActionResult> GetReviewsByMovieAsync([FromQuery] int movieId)
    {
        return Ok(await _service.GetReviewsByMovieAsync(movieId));
    }

    [HttpGet("reviews-by-serie")]
    public async Task<IActionResult> GetReviewsBySerieAsync([FromQuery] int serieId)
    {
        return Ok(await _service.GetReviewsBySerieAsync(serieId));
    }

    [HttpGet("reviews-by-episode")]
    public async Task<IActionResult> GetReviewsByEpisodeAsync([FromQuery] int episodeId)
    {
        return Ok(await _service.GetReviewsByEpisodeAsync(episodeId));
    }

    [HttpGet("reviews-by-user")]
    public async Task<IActionResult> GetReviewsByUserAsync()
    {
        return Ok(await _service.GetReviewsByUserAsync());
    }

    [HttpPost("add-reply")]
    public async Task<IActionResult> AddReplyAsync([FromBody] ReviewCreateDto dto)
    {
        return Ok(await _service.AddReplyAsync(dto));
    }

    [HttpPut("update-reply")]
    public async Task<IActionResult> UpdateReplyAsync([FromQuery] int reviewId, [FromBody] ReviewUpdateDto dto)
    {
        return Ok(await _service.UpdateReplyAsync(dto, reviewId));
    }

    [HttpGet("replies-by-review")]
    public async Task<IActionResult> GetRepliesByReviewAsync([FromQuery] int parentReviewId)
    {
        return Ok(await _service.GetRepliesByReviewAsync(parentReviewId));
    }

    [HttpPost("like-review")]
    public async Task<IActionResult> LikeReviewAsync([FromQuery] int reviewId)
    {
        return Ok(await _service.LikeReviewAsync(reviewId));
    }

    [HttpPost("dislike-review")]
    public async Task<IActionResult> DislikeReviewAsync([FromQuery] int reviewId)
    {
        return Ok(await _service.DeleteReviewAsync(reviewId));
    }

    [HttpPost("undo-like-review")]
    public async Task<IActionResult> UndoLikeReviewAsync([FromQuery] int reviewId)
    {
        return Ok(await _service.UndoLikeReviewAsync(reviewId));
    }

    [HttpPost("undo-dislike-review")]
    public async Task<IActionResult> UndoDislikeReviewAsync([FromQuery] int reviewId)
    {
        return Ok(await _service.UndoDislikeReviewAsync(reviewId));
    }

    [HttpGet("review-reaction-count")]
    public async Task<IActionResult> GetReviewReactionCountAsync([FromQuery] int reviewId)
    {
        return Ok(await _service.GetReviewReactionCountAsync(reviewId));
    }
}
