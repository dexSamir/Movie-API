using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MovieApp.BL.Exceptions.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using MovieApp.BL.ExternalServices.Interfaces;
using MovieApp.Core.Entities;
using MovieApp.BL.DTOs.Options;
using MovieApp.DAL.Context;
using Microsoft.EntityFrameworkCore;
using BlogApp.BL.Exceptions.AuthException;

namespace MovieApp.BL.ExternalServices.Implements;
public class EmailService : IEmailService
{
    readonly IOptions<SmtpOptions> _options;
    readonly SmtpOptions _smtp;
    readonly IMemoryCache _cache;
    readonly AppDbContext _context;
    readonly IConfiguration _configuration;
    public EmailService(
        IOptions<SmtpOptions> options,
        AppDbContext context,
        IConfiguration configuration,
        IMemoryCache cache)
    {
        _cache = cache;
        _options = options;
        _configuration = configuration;
        _context = context;
        _smtp = _options.Value;
    }

    public async Task<string> EmailVereficationToken(string email)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, email)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: credentials);

        var handler = new JwtSecurityTokenHandler();
        return handler.WriteToken(token);
    }

    public async Task<string> SendEmailVerificationAsync(string email, string token)
    {
        if (_cache.TryGetValue(email, out var _))
            throw new ExistException("Email artiq gonderilib!");

        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        if (user is null)
            throw new NotFoundException<User>();

        SmtpClient smtp = new SmtpClient
        {
            Host = _smtp.Host,
            Port = _smtp.Port,
            EnableSsl = true,
            Credentials = new NetworkCredential(_smtp.Sender, _smtp.Password)
        };

        MailMessage msg = new MailMessage
        {
            From = new MailAddress(_smtp.Sender, "Samir Habibov"),
            Subject = "Email Confirmation",
            Body = $"<p>Dear User,</p>" +
               $"<p>Please use the following link to confirm your account:</p>" +
               $"<p><a href='https://yourapp.com/verify-email?email={email}&token={token}'>Confirm Email</a></p>" +
               $"<p>If you did not request this, please ignore this message.</p>" +
               $"<p>Thank you,<br/>WatchMovie</p>",
            IsBodyHtml = true
        };

        msg.To.Add(email);
        smtp.Send(msg);

        return "Email gönderildi!";
    }

    public async Task<bool> VerifyEmailAsync(string email, int code)
    {
        if (!_cache.TryGetValue(email, out int result))
            throw new NotFoundException("Bu email-e kod gonderilmeyib!");

        if (result != code)
            throw new CodeIsInvalidException();

        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        if (user == null)
            throw new NotFoundException<User>();

        user!.IsVerified = true;
        await _context.SaveChangesAsync();
        return true;
    }
}

