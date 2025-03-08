using Microsoft.AspNetCore.Mvc;
using MovieApp.BL.DTOs.MovieDtos;
using MovieApp.BL.Services.Interfaces;
using MovieApp.BL.Utilities.Enums;

namespace MovieApp.API.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class MoviesController : ControllerBase
{
    readonly IMovieService _service;
    public MoviesController(IMovieService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllMovies()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMovieById(int id)
    {
        return Ok(await _service.GetByIdAsync(id));
    }

    [HttpGet("genre/{genre}")]
    public async Task<IActionResult> GetMoviesByGenre(string genre)
    {
        return Ok(await _service.GetByGenre(genre));
    }

    [HttpGet("director/{directorId}")]
    public async Task<IActionResult> GetMoviesByDirector(int directorId)
    {
        return Ok(await _service.GetByDirectorAsync(directorId));
    }

    [HttpGet("actor/{actorId}")]
    public async Task<IActionResult> GetMoviesByActor(int actorId)
    {
        return Ok(await _service.GetByActorAsync(actorId));
    }

    [HttpGet("rating/{rating}")]
    public async Task<IActionResult> GetMoviesByRating(double rating)
    {
        return Ok(await _service.GetByRating(rating));
    }

    [HttpGet("release-date/{releaseDate}")]
    public async Task<IActionResult> GetMoviesByReleaseDate(DateOnly releaseDate)
    {
        return Ok(await _service.GetByReleaseDateAsync(releaseDate));
    }

    [HttpGet("duration-range/{minDuration}/{maxDuration}")]
    public async Task<IActionResult> GetMoviesByDurationRange(int minDuration, int maxDuration)
    {
        return Ok(await _service.GetByDurationRangeAsync(minDuration, maxDuration));
    }

    [HttpGet("title/{title}")]
    public async Task<IActionResult> GetMoviesByTitle(string title)
    {
        return Ok(await _service.GetByTitleAsync(title));
    }

    [HttpGet("sort/title/{ascending}")]
    public async Task<IActionResult> SortMoviesByTitle(bool ascending = true)
    {
        return Ok(await _service.SortByTitleAsync(ascending));
    }

    [HttpGet("sort/release-date/{ascending}")]
    public async Task<IActionResult> SortMoviesByReleaseDate(bool ascending = true)
    {
        return Ok(await _service.SortByReleaseDateAsync(ascending));
    }

    [HttpGet("sort/rating/{ascending}")]
    public async Task<IActionResult> SortMoviesByRating(bool ascending = true)
    {
        return Ok(await _service.SortByRatingAsync(ascending));
    }

    [HttpPost]
    public async Task<IActionResult> CreateMovie([FromBody] MovieCreateDto dto)
    {
        return Ok(await _service.CreateAsync(dto));
    }

    [HttpPost("range")]
    public async Task<IActionResult> CreateMovies([FromBody] IEnumerable<MovieCreateDto> dtos)
    {
        return Ok(await _service.CreateRangeAsync(dtos));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMovie(int id, [FromBody] MovieUpdateDto dto)
    {
        return Ok(await _service.UpdateAsync(dto,id));
    }

    [HttpPut("{id}/media/{type}")]
    public async Task<IActionResult> UpdateMovieMedia(int id, IFormFile file, EMediaType type)
    {
        return Ok(await _service.UpdateMediaUrlAsync(id, file, type));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        return Ok(await _service.DeleteAsync(id, EDeleteType.Hard));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> SoftDelete(string id)
    {
        return Ok(await _service.DeleteAsync(id, EDeleteType.Soft));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> ReverseSoftDelete(string id)
    {
        return Ok(await _service.DeleteAsync(id, EDeleteType.Soft));
    }
    [HttpDelete("{ids}")]
    public async Task<IActionResult> DeleteRange([FromRoute] string ids)
    {
        return Ok(await _service.DeleteAsync(ids, EDeleteType.Soft));
    }
    [HttpDelete("{ids}")]
    public async Task<IActionResult> SoftDeleteRange([FromRoute] string ids)
    {
        return Ok(await _service.DeleteAsync(ids, EDeleteType.Soft));
    }
    [HttpDelete("{ids}")]
    public async Task<IActionResult> ReverseSoftDeleteRange([FromRoute] string ids)
    {
        return Ok(await _service.DeleteAsync(ids, EDeleteType.Soft));
    }

    [HttpPost("{movieId}/rate")]
    public async Task<IActionResult> RateMovie(int movieId, [FromForm] int score)
    {
        return Ok(await _service.RateMovieAsync(movieId, score));
    }

    [HttpPut("rating/{movieId}")]
    public async Task<IActionResult> UpdateRating(int movieId, [FromBody] int newScore)
    {
        return Ok(await _service.UpdateRatingAsync(movieId, newScore));
    }

    [HttpDelete("rating/{movieId}")]
    public async Task<IActionResult> DeleteRating(int movieId)
    {
        return Ok(await _service.DeleteRatingAsync(movieId));
    }

    [HttpGet("{movieId}/average-rating")]
    public async Task<IActionResult> GetAverageRating(int movieId)
    {
        return Ok(await _service.GetAverageRatingAsync(movieId));
    }

    [HttpPost("{movieId}/like")]
    public async Task<IActionResult> LikeMovie(int movieId)
    {
        return Ok(await _service.LikeMovieAsync(movieId));
    }

    [HttpPost("{movieId}/dislike")]
    public async Task<IActionResult> DislikeMovie(int movieId)
    {
        return Ok(await _service.DislikeMovieAsync(movieId));
    }

    [HttpPost("{movieId}/undo-like")]
    public async Task<IActionResult> UndoLikeMovie(int movieId)
    {
        return Ok(await _service.UndoLikeMovieAsync(movieId));
    }

    [HttpPost("{movieId}/undo-dislike")]
    public async Task<IActionResult> UndoDislikeMovie(int movieId)
    {
        return Ok(await _service.UndoDislikeMovieAsync(movieId));
    }

    [HttpGet("{movieId}/reactions")]
    public async Task<IActionResult> GetMovieReactions(int movieId)
    {
        return Ok(await _service.GetMovieReactionCountAsync(movieId));
    }

    [HttpGet("total-count")]
    public async Task<IActionResult> GetTotalMovieCount()
    {
        return Ok(await _service.GetTotalMovieCountAsync());
    }

    [HttpGet("{movieId}/total-watch-count")]
    public async Task<IActionResult> GetTotalWatchCount(int movieId)
    {
        return Ok(await _service.GetTotalWatchCountAsync(movieId));
    }

    [HttpGet("top-rated/{count}")]
    public async Task<IActionResult> GetTopRatedMovies(int count)
    {
        return Ok(await _service.GetTopRatedMoviesAsync(count));
    }

    [HttpGet("most-watched/{count}")]
    public async Task<IActionResult> GetMostWatchedMovies(int count)
    {
        return Ok(await _service.GetMostWatchedMoviesAsync(count));
    }

    [HttpGet("recommendations")]
    public async Task<IActionResult> GetRecommendations()
    {
        return Ok(await _service.GetRecommendationsAsync());
    }

    [HttpGet("popular")]
    public async Task<IActionResult> GetPopularMovies()
    {
        return Ok(await _service.GetPopularMoviesAsync());
    }

    [HttpGet("recently-added")]
    public async Task<IActionResult> GetRecentlyAddedMovies()
    {
        return Ok(await _service.GetRecentlyAddedMoviesAsync());
    }

    [HttpPost("{movieId}/rent")]
    public async Task<IActionResult> RentMovie(int movieId)
    {
        return Ok(await _service.RentMovieAsync(movieId));
    }

    [HttpGet("rented")]
    public async Task<IActionResult> GetUserRentedMovies()
    {
        return Ok(await _service.GetUserRentedMoviesAsync());
    }
}
