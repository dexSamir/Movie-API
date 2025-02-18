using Microsoft.AspNetCore.Mvc;
using MovieApp.BL.DTOs.MovieDtos;
using MovieApp.BL.DTOs.RecommendationDtos;
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

    [HttpGet("{id}")]
    public async Task<ActionResult<MovieGetDto>> GetMovieById(int id)
    {
        return Ok(await _service.GetByIdAsync(id));
    }

    [HttpGet("genre/{genre}")]
    public async Task<ActionResult<IEnumerable<MovieGetDto>>> GetMoviesByGenre(string genre)
    {
        return Ok(await _service.GetByGenre(genre));
    }

    [HttpGet("director/{directorId}")]
    public async Task<ActionResult<IEnumerable<MovieGetDto>>> GetMoviesByDirector(int directorId)
    {
        return Ok(await _service.GetByDirectorAsync(directorId));
    }

    [HttpGet("actor/{actorId}")]
    public async Task<ActionResult<IEnumerable<MovieGetDto>>> GetMoviesByActor(int actorId)
    {
        return Ok(await _service.GetByActorAsync(actorId));
    }

    [HttpGet("rating/{rating}")]
    public async Task<ActionResult<IEnumerable<MovieGetDto>>> GetMoviesByRating(double rating)
    {
        return Ok(await _service.GetByRating(rating));
    }

    [HttpGet("release-date/{releaseDate}")]
    public async Task<ActionResult<IEnumerable<MovieGetDto>>> GetMoviesByReleaseDate(DateOnly releaseDate)
    {
        return Ok(await _service.GetByReleaseDateAsync(releaseDate));
    }

    [HttpGet("duration-range/{minDuration}/{maxDuration}")]
    public async Task<ActionResult<IEnumerable<MovieGetDto>>> GetMoviesByDurationRange(int minDuration, int maxDuration)
    {
        return Ok(await _service.GetByDurationRangeAsync(minDuration, maxDuration));
    }

    [HttpGet("title/{title}")]
    public async Task<ActionResult<IEnumerable<MovieGetDto>>> GetMoviesByTitle(string title)
    {
        return Ok(await _service.GetByTitleAsync(title));
    }

    [HttpGet("sort/title/{ascending}")]
    public async Task<ActionResult<IEnumerable<MovieGetDto>>> SortMoviesByTitle(bool ascending = true)
    {
        return Ok(await _service.SortByTitleAsync(ascending));
    }

    [HttpGet("sort/release-date/{ascending}")]
    public async Task<ActionResult<IEnumerable<MovieGetDto>>> SortMoviesByReleaseDate(bool ascending = true)
    {
        return Ok(await _service.SortByReleaseDateAsync(ascending));
    }

    [HttpGet("sort/rating/{ascending}")]
    public async Task<ActionResult<IEnumerable<MovieGetDto>>> SortMoviesByRating(bool ascending = true)
    {
        return Ok(await _service.SortByRatingAsync(ascending));
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateMovie([FromBody] MovieCreateDto dto)
    {
        return Ok(await _service.CreateAsync(dto));
    }

    [HttpPost("range")]
    public async Task<ActionResult<IEnumerable<int>>> CreateMovies([FromBody] IEnumerable<MovieCreateDto> dtos)
    {
        return Ok(await _service.CreateRangeAsync(dtos));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<bool>> UpdateMovie(int id, [FromBody] MovieUpdateDto dto)
    {
        return Ok(await _service.UpdateAsync(dto,id));
    }

    [HttpPut("{id}/media/{type}")]
    public async Task<ActionResult<bool>> UpdateMovieMedia(int id, IFormFile file, EMediaType type)
    {
        return Ok(await _service.UpdateMediaUrlAsync(id, file, type));
    }

    [HttpDelete("{ids}")]
    public async Task<ActionResult<bool>> DeleteMovies(string ids, EDeleteType deleteType)
    {
        return Ok(await _service.DeleteAsync(ids, deleteType));
    }

    [HttpPost("{movieId}/rate")]
    public async Task<ActionResult<bool>> RateMovie(int movieId, [FromBody] int score)
    {
        return Ok(await _service.RateMovieAsync(movieId, score));
    }

    [HttpPut("rating/{ratingId}")]
    public async Task<ActionResult<bool>> UpdateRating(int ratingId, [FromBody] int newScore)
    {
        return Ok(await _service.UpdateRatingAsync(ratingId, newScore));
    }

    [HttpDelete("rating/{ratingId}")]
    public async Task<ActionResult<bool>> DeleteRating(int ratingId)
    {
        return Ok(await _service.DeleteRatingAsync(ratingId));
    }

    [HttpGet("{movieId}/average-rating")]
    public async Task<ActionResult<double>> GetAverageRating(int movieId)
    {
        return Ok(await _service.GetAverageRatingAsync(movieId));
    }

    [HttpPost("{movieId}/like")]
    public async Task<ActionResult<bool>> LikeMovie(int movieId)
    {
        return Ok(await _service.LikeMovieAsync(movieId));
    }

    [HttpPost("{movieId}/dislike")]
    public async Task<ActionResult<bool>> DislikeMovie(int movieId)
    {
        return Ok(await _service.DislikeMovieAsync(movieId));
    }

    [HttpPost("{movieId}/undo-like")]
    public async Task<ActionResult<bool>> UndoLikeMovie(int movieId)
    {
        return Ok(await _service.UndoLikeMovieAsync(movieId));
    }

    [HttpPost("{movieId}/undo-dislike")]
    public async Task<ActionResult<bool>> UndoDislikeMovie(int movieId)
    {
        return Ok(await _service.UndoDislikeMovieAsync(movieId));
    }

    [HttpGet("{movieId}/reactions")]
    public async Task<ActionResult<(int LikeCount, int DislikeCount)>> GetMovieReactions(int movieId)
    {
        return Ok(await _service.GetMovieReactionCountAsync(movieId));
    }

    [HttpGet("total-count")]
    public async Task<ActionResult<int>> GetTotalMovieCount()
    {
        return Ok(await _service.GetTotalMovieCountAsync());
    }

    [HttpGet("{movieId}/total-watch-count")]
    public async Task<ActionResult<int>> GetTotalWatchCount(int movieId)
    {
        return Ok(await _service.GetTotalWatchCountAsync(movieId));
    }

    [HttpGet("top-rated/{count}")]
    public async Task<ActionResult<IEnumerable<MovieGetDto>>> GetTopRatedMovies(int count)
    {
        return Ok(await _service.GetTopRatedMoviesAsync(count));
    }

    [HttpGet("most-watched/{count}")]
    public async Task<ActionResult<IEnumerable<MovieGetDto>>> GetMostWatchedMovies(int count)
    {
        return Ok(await _service.GetMostWatchedMoviesAsync(count));
    }

    [HttpGet("recommendations")]
    public async Task<ActionResult<IEnumerable<RecommendationGetDto>>> GetRecommendations()
    {
        return Ok(await _service.GetRecommendationsAsync());
    }

    [HttpGet("popular")]
    public async Task<ActionResult<IEnumerable<MovieGetDto>>> GetPopularMovies()
    {
        return Ok(await _service.GetPopularMoviesAsync());
    }

    [HttpGet("recently-added")]
    public async Task<ActionResult<IEnumerable<MovieGetDto>>> GetRecentlyAddedMovies()
    {
        return Ok(await _service.GetRecentlyAddedMoviesAsync());
    }

    [HttpPost("{movieId}/rent")]
    public async Task<ActionResult<bool>> RentMovie(int movieId)
    {
        return Ok(await _service.RentMovieAsync(movieId));
    }

    [HttpGet("rented")]
    public async Task<ActionResult<IEnumerable<MovieGetDto>>> GetUserRentedMovies()
    {
        return Ok(await _service.GetUserRentedMoviesAsync());
    }
}
