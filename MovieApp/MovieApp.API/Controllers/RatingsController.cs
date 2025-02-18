using Microsoft.AspNetCore.Mvc;
using MovieApp.BL.Services.Interfaces;

namespace MovieApp.API.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class RatingsController : ControllerBase
{
    readonly IRatingService _service;
    public RatingsController(IRatingService service)
    {
        _service = service; 
    }

    [HttpPost("rate-movie")]
    public async Task<IActionResult> RateMovieAsync([FromQuery] int movieId, [FromQuery] int score)
    {
        return Ok(await _service.RateMovieAsync(movieId, score));
    }

    [HttpPost("rate-episode")]
    public async Task<IActionResult> RateEpisodeAsync([FromQuery] int episodeId, [FromQuery] int score)
    {
        return Ok(await _service.RateEpisodeAsync(episodeId, score));
    }

    [HttpPost("rate-serie")]
    public async Task<IActionResult> RateSerieAsync([FromQuery] int serieId, [FromQuery] int score)
    {

        return Ok(await _service.RateSerieAsync(serieId, score));
    }

    [HttpPut("update-rating")]
    public async Task<IActionResult> UpdateRatingAsync([FromQuery] int ratingId, [FromQuery] int newScore)
    {
        return Ok(await _service.UpdateRatingAsync(ratingId, newScore));
    }

    [HttpDelete("delete-rating")]
    public async Task<IActionResult> DeleteRatingAsync([FromQuery] int ratingId)
    {
        return Ok(await _service.DeleteRatingAsync(ratingId));
    }

    [HttpGet("user-ratings")]
    public async Task<IActionResult> GetUserRatingsAsync()
    {
        return Ok(await _service.GetUserRatingsAsync());
    }

    [HttpGet("episode-rating")]
    public async Task<IActionResult> GetAverageRatingForEpisodeAsync([FromQuery] int episodeId)
    {
        return Ok(await _service.GetAverageRatingForEpisodeAsync(episodeId));
    }

    [HttpGet("movie-rating")]
    public async Task<IActionResult> GetAverageRatingForMovieAsync([FromQuery] int movieId)
    {
        return Ok( await _service.GetAverageRatingForMovieAsync(movieId));
    }

    [HttpGet("serie-rating")]
    public async Task<IActionResult> GetAverageRatingForSerieAsync([FromQuery] int serieId)
    {
        return Ok(await _service.GetAverageRatingForSerieAsync(serieId));
    }

    [HttpGet("serie-hybrid-rating")]
    public async Task<IActionResult> GetHybridRatingForSerieAsync([FromQuery] int serieId)
    {
        return Ok(await _service.GetHybridRatingForSerieAsync(serieId));
    }
}
