﻿using MovieApp.BL.DTOs.UserDtos;

namespace MovieApp.BL.Services.Interfaces;
public interface IAuthService
{
    Task<string> LoginAsync(LoginDto dto);
    Task RegisterAsync(RegisterDto dto);
    Task<bool> VerifyAccountAsync(string email, string token);
    Task<string> SendVerificationEmailAsync(string email, string token);
}

