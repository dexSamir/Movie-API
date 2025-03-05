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

    [HttpPut]
    public async Task<IActionResult> UpdateReview([FromQuery] int reviewId, [FromBody] ReviewUpdateDto dto)
    {
        return Ok(await _service.UpdateReviewAsync( reviewId, dto));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteReview([FromQuery] int reviewId)
    {
        return Ok(await _service.DeleteReviewAsync(reviewId));
    }

    [HttpGet]
    public async Task<IActionResult> GetReviewById([FromQuery] int reviewId)
    {
        return Ok(await _service.GetReviewByIdAsync(reviewId));
    }

    [HttpGet]
    public async Task<IActionResult> GetReviewsByMovie([FromQuery] int movieId)
    {
        return Ok(await _service.GetReviewsByMovieAsync(movieId));
    }

    [HttpGet]
    public async Task<IActionResult> GetReviewsBySerie([FromQuery] int serieId)
    {
        return Ok(await _service.GetReviewsBySerieAsync(serieId));
    }

    [HttpGet]
    public async Task<IActionResult> GetReviewsByEpisode([FromQuery] int episodeId)
    {
        return Ok(await _service.GetReviewsByEpisodeAsync(episodeId));
    }

    [HttpGet]
    public async Task<IActionResult> GetReviewsByUser()
    {
        return Ok(await _service.GetReviewsByUserAsync());
    }

    [HttpGet]
    public async Task<IActionResult> GetRepliesByReview([FromQuery] int parentReviewId)
    {
        return Ok(await _service.GetRepliesByReviewAsync(parentReviewId));
    }

    [HttpPost]
    public async Task<IActionResult> LikeReview([FromQuery] int reviewId)
    {
        return Ok(await _service.LikeReviewAsync(reviewId));
    }

    [HttpPost]
    public async Task<IActionResult> DislikeReview([FromQuery] int reviewId)
    {
        return Ok(await _service.DislikeReviewAsync(reviewId));
    }

    [HttpPost]
    public async Task<IActionResult> UndoLikeReview([FromQuery] int reviewId)
    {
        return Ok(await _service.UndoLikeReviewAsync(reviewId));
    }

    [HttpPost]
    public async Task<IActionResult> UndoDislikeReview([FromQuery] int reviewId)
    {
        return Ok(await _service.UndoDislikeReviewAsync(reviewId));
    }

    [HttpGet]
    public async Task<IActionResult> GetReviewReactionCount([FromQuery] int reviewId)
    {
        return Ok(await _service.GetReviewReactionCountAsync(reviewId));
    }
}
