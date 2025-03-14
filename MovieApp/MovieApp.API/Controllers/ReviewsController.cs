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

    [HttpPost]
    public async Task<IActionResult> AddReview([FromBody] ReviewCreateDto dto)
    {
        return Ok(await _service.AddReviewAsync(dto));
    }

    [HttpPut("{reviewId}")]
    public async Task<IActionResult> UpdateReview(int reviewId, [FromBody] ReviewUpdateDto dto)
    {
        return Ok(await _service.UpdateReviewAsync( reviewId, dto));
    }

    [HttpDelete("{reviewId}")]
    public async Task<IActionResult> DeleteReview(int reviewId)
    {
        return Ok(await _service.DeleteReviewAsync(reviewId));
    }

    [HttpGet("{reviewId}")]
    public async Task<IActionResult> GetReviewById(int reviewId)
    {
        return Ok(await _service.GetReviewByIdAsync(reviewId));
    }

    [HttpGet("{movieId}")]
    public async Task<IActionResult> GetReviewsByMovie(int movieId)
    {
        return Ok(await _service.GetReviewsByMovieAsync(movieId));
    }

    [HttpGet("{serieId}")]
    public async Task<IActionResult> GetReviewsBySerie(int serieId)
    {
        return Ok(await _service.GetReviewsBySerieAsync(serieId));
    }

    [HttpGet("{episodeId}")]
    public async Task<IActionResult> GetReviewsByEpisode(int episodeId)
    {
        return Ok(await _service.GetReviewsByEpisodeAsync(episodeId));
    }

    [HttpGet]
    public async Task<IActionResult> GetReviewsByUser()
    {
        return Ok(await _service.GetReviewsByUserAsync());
    }

    [HttpGet("{parentReviewId}")]
    public async Task<IActionResult> GetRepliesByReview(int parentReviewId)
    {
        return Ok(await _service.GetRepliesByReviewAsync(parentReviewId));
    }

    [HttpPost("{reviewId}")]
    public async Task<IActionResult> LikeReview(int reviewId)
    {
        return Ok(await _service.LikeReviewAsync(reviewId));
    }

    [HttpPost("{reviewId}")]
    public async Task<IActionResult> DislikeReview(int reviewId)
    {
        return Ok(await _service.DislikeReviewAsync(reviewId));
    }

    [HttpPost("{reviewId}")]
    public async Task<IActionResult> UndoLikeReview(int reviewId)
    {
        return Ok(await _service.UndoLikeReviewAsync(reviewId));
    }

    [HttpPost("{reviewId}")]
    public async Task<IActionResult> UndoDislikeReview(int reviewId)
    {
        return Ok(await _service.UndoDislikeReviewAsync(reviewId));
    }

    [HttpGet("{reviewId}")]
    public async Task<IActionResult> GetReviewReactionCount(int reviewId)
    {
        return Ok(await _service.GetReviewReactionCountAsync(reviewId));
    }
}
