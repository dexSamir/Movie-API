using Humanizer;
using Microsoft.AspNetCore.Mvc;
using MovieApp.BL.DTOs.RentalDtos;
using MovieApp.BL.Exceptions.AuthException;
using MovieApp.BL.Exceptions.Common;
using MovieApp.BL.Services.Interfaces;
using MovieApp.Core.Entities;

namespace MovieApp.API.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class RentalsController : ControllerBase
{
    readonly IRentalService _service;
    public RentalsController(IRentalService service)
    {
        _service = service; 
    }

    [HttpGet("user-rentals")]
    public async Task<IActionResult> GetUserRentalsAsync()
    {
        return Ok(await _service.GetUserRentalsAsync());
    }

    [HttpGet("rental-by-id")]
    public async Task<IActionResult> GetRentalByIdAsync([FromQuery] int rentalId)
    {
        return Ok(await _service.GetRentalByIdAsync(rentalId));
    }

    [HttpPost("rent-movie")]
    public async Task<IActionResult> RentMovieAsync([FromBody] RentalCreateDto dto)
    {
        return Ok(await _service.RentMovieAsync(dto));
    }

    [HttpPut("update-rental")]
    public async Task<IActionResult> UpdateRentalAsync([FromBody] RentalUpdateDto dto, int rentalId)
    {
        return Ok(await _service.UpdateRentalAsync(dto, rentalId));
    }

    [HttpPost("return-movie")]
    public async Task<IActionResult> ReturnMovieAsync([FromQuery] int rentalId)
    {
        return Ok(await _service.ReturnMovieAsync(rentalId));
    }
}
