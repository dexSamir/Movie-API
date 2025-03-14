using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MovieApp.BL.DTOs.UserDtos;
using MovieApp.BL.Exceptions.Common;
using MovieApp.BL.ExternalServices.Interfaces;
using MovieApp.BL.Services.Interfaces;
using MovieApp.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace MovieApp.BL.Services.Implements;

public class AuthService : IAuthService
{
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;
    private readonly IJwtTokenHandler _tokenHandler;
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    readonly IFileService _fileService;


    public AuthService(
        IFileService fileService,
        IMapper mapper,
        IJwtTokenHandler tokenHandler,
        IEmailService emailService,
        SignInManager<User> signInManager,
        UserManager<User> userManager)
    {
        _fileService = fileService; 
        _emailService = emailService;
        _tokenHandler = tokenHandler;
        _mapper = mapper;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<string> LoginAsync(LoginDto dto)
    {
        var user = await _userManager.FindByNameAsync(dto.UsernameOrEmail) ??
                   await _userManager.FindByEmailAsync(dto.UsernameOrEmail);

        if (user == null)
            throw new NotFoundException<User>();

        var result = await _signInManager.PasswordSignInAsync(user, dto.Password, dto.RememberMe, true);

        if (!result.Succeeded)
            throw new NotFoundException<User>(); 
        return _tokenHandler.CreateToken(user, 12);
    }

    public async Task RegisterAsync(RegisterDto dto)
    {
        var existingUserByEmail = await _userManager.FindByEmailAsync(dto.Email);
        var existingUserByUsername = await _userManager.FindByNameAsync(dto.UserName);

        if (existingUserByEmail != null)
            throw new ExistException($"{dto.Email} already exists.");

        if (existingUserByUsername != null)
            throw new ExistException($"{dto.UserName} already exists.");

        var newUser = _mapper.Map<User>(dto);
        newUser.ProfileUrl = await _fileService.ProcessImageAsync(dto.ProfileUrl, "users/","image/", 15); 
        var createResult = await _userManager.CreateAsync(newUser, dto.Password);

        if (!createResult.Succeeded)
            foreach (var error in createResult.Errors)
                throw new ValidationException(error.Description);

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

        newUser.IsVerified = false;
        newUser.ConfirmationToken = token;

        await _emailService.SendEmailVereficationAsync(newUser.Email, token);
    }

    public async Task<string> SendVerificationEmailAsync(string email, string token)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            throw new NotFoundException<User>();

        await _emailService.SendEmailVereficationAsync(email, token);

        return "Verification email sent.";
    }

    public async Task<bool> VerifyAccountAsync(string email, string token)
        => await _emailService.VerifyEmailAsync(email, token);

}