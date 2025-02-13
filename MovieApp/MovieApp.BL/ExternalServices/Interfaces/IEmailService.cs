namespace MovieApp.BL.ExternalServices.Interfaces;
public interface IEmailService
{
    Task<string> EmailVereficationToken(string email);
    Task<string> SendEmailVereficationAsync(string email, string token);
    Task<bool> VerifyEmailAsync(string email, int code);
}

