using Humanizer;
using Microsoft.AspNetCore.Mvc;
using MovieApp.BL.DTOs.UserDtos;
using MovieApp.BL.Exceptions.Common;
using MovieApp.BL.Services.Interfaces;
using MovieApp.Core.Entities;
using NuGet.Common;

namespace MovieApp.API.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class AuthsController : ControllerBase
{
    readonly IAuthService _service;
    public AuthsController(IAuthService service)
    {
        _service = service; 
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginDto dto)
    {
        return Ok(await _service.LoginAsync(dto));
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto dto)
    {
        await _service.RegisterAsync(dto);
        return Ok();
    }

    [HttpPost("send-verification-email")]
    public async Task<IActionResult> SendVerificationEmailAsync([FromQuery] string email, [FromQuery] string token)
    {
        return Ok(await _service.SendVerificationEmailAsync(email, token));
    }

    [HttpPost("verify-account")]
    public async Task<IActionResult> VerifyAccountAsync([FromQuery] string email, [FromQuery] int code)
    {
        return Ok(await _service.VerifyAccountAsync(email, code));
    }
}
