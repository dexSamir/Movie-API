//using Microsoft.AspNetCore.Mvc;
//using MovieApp.BL.Services.Interfaces;

//namespace MovieApp.API.Controllers;
//[Route("api/[controller]/[action]")]
//[ApiController]
//public class WatchProgressesController : ControllerBase
//{
//    readonly IWatchProgressService _service;
//    public WatchProgressesController(IWatchProgressService service)
//    {
//        _service = service;
//    }

//    [HttpPost("start-watching")]
//    public async Task<IActionResult> StartWatchingAsync([FromQuery] int movieId)
//    {
//        return Ok(await _service.StartWatchingAsync(movieId));
//    }

//    [HttpPut("update-watch-progress")]
//    public async Task<IActionResult> UpdateWatchProgressAsync([FromQuery] int movieId, [FromQuery] TimeSpan currentTime)
//    {
//        return Ok(await _service.UpdateWatchProgressAsync(movieId, currentTime));
//    }

//    [HttpPost("finish-watching")]
//    public async Task<IActionResult> FinishWatchingAsync([FromQuery] int movieId)
//    {
//        return Ok(await _service.FinishWatchingAsync(movieId));
//    }

//    [HttpPost("pause-movie")]
//    public async Task<IActionResult> PauseMovieAsync([FromQuery] int movieId, [FromQuery] TimeSpan pausedAt)
//    {
//        return Ok(await _service.PauseMovieAsync(movieId, pausedAt));
//    }

//    [HttpPost("resume-watching")]
//    public async Task<IActionResult> ResumeWatchingAsync([FromQuery] int movieId)
//    {
//        return Ok(await _service.ResumeWatchingAsync(movieId));
//    }

//    [HttpGet("is-user-watching")]
//    public async Task<IActionResult> IsUserWatchingAsync([FromQuery] int movieId)
//    {
//        return Ok(await _service.IsUserWatchingAsync(movieId));
//    }

//    [HttpGet("current-time")]
//    public async Task<IActionResult> GetCurrentTimeAsync([FromQuery] int movieId)
//    {
//        return Ok(await _service.GetCurrentTimeAsync(movieId));
//    }

//    [HttpGet("playback-speed")]
//    public async Task<IActionResult> GetPlaybackSpeedAsync([FromQuery] int movieId)
//    {
//        return Ok(await _service.GetPlaybackSpeedAsync(movieId));
//    }

//    [HttpPut("set-playback-speed")]
//    public async Task<IActionResult> SetPlaybackSpeedAsync([FromQuery] int movieId, [FromQuery] double speed)
//    {
//        return Ok(await _service.SetPlaybackSpeedAsync(movieId, speed));
//    }

//    [HttpPost("seek-forward")]
//    public async Task<IActionResult> SeekForwardAsync([FromQuery] int movieId, [FromQuery] TimeSpan seekTime)
//    {
//        return Ok(await _service.SeekForwardAsync(movieId, seekTime));
//    }

//    [HttpPost("seek-backward")]
//    public async Task<IActionResult> SeekBackwardAsync([FromQuery] int movieId, [FromQuery] TimeSpan seekTime)
//    {
//        return Ok(await _service.SeekBackwardAsync(movieId, seekTime));
//    }
//}
