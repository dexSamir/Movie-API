//using Microsoft.AspNetCore.Mvc;
//using MovieApp.BL.DTOs.HistoryDtos;
//using MovieApp.BL.Services.Interfaces;

//namespace MovieApp.API.Controllers;
//[Route("api/[controller]/[action]")]
//[ApiController]
//public class HistoriesController : ControllerBase
//{
//    readonly IHistoryService _service;
//    public HistoriesController(IHistoryService service)
//    {
//        _service = service;
//    }

//    [HttpGet("user-watch-history")]
//    public async Task<IActionResult> GetUserWatchHistoryAsync()
//    {
//        return Ok(await _service.GetUserWatchHistoryAsync());
//    }

//    [HttpGet("last-watched")]
//    public async Task<IActionResult> GetLastWatchedAsync()
//    {
//        return Ok(await _service.GetLastWatchedAsync());
//    }

//    [HttpPost("add-history")]
//    public async Task<IActionResult> AddHistoryAsync([FromBody] HistoryCreateDto dto)
//    {
//        return Ok(await _service.AddHistoryAsync(dto));
//    }

//    [HttpPut("update-history")]
//    public async Task<IActionResult> UpdateHistoryAsync([FromBody] HistoryUpdateDto dto, int historyId)
//    {
//        return Ok(await _service.UpdateHistoryAsync(dto, historyId));
//    }

//    [HttpDelete("remove-from-history")]
//    public async Task<IActionResult> RemoveFromHistoryAsync([FromQuery] int historyId)
//    {
//        return Ok(await _service.RemoveFromHistoryAsync( historyId));
//    }

//    [HttpDelete("clear-history")]
//    public async Task<IActionResult> ClearHistoryAsync()
//    {
//        return Ok(await _service.ClearHistoryAsync());
//    }
//}
