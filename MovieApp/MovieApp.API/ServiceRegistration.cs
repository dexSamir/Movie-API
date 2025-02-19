using Microsoft.IdentityModel.Tokens;
using System.Text;
using MovieApp.BL.DTOs.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace MovieApp.API;
public static class ServiceRegistration
{
    public static IServiceCollection AddJwtOptions(this IServiceCollection services, IConfiguration Configuration)
    {
        services.Configure<JwtOptions>(Configuration.GetSection(JwtOptions.Jwt));
        return services;
    }
    public static IServiceCollection AddEmailOptions(this IServiceCollection services, IConfiguration Configuration)
    {
        services.Configure<SmtpOptions>(Configuration.GetSection(SmtpOptions.Name));
        return services;
    }

    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {

        JwtOptions Jwtopt = new JwtOptions();
        Jwtopt.Issuer = configuration.GetSection("JwtSettings")["Issuer"]!;
        Jwtopt.Audience = configuration.GetSection("JwtSettings")["Audience"]!;
        Jwtopt.SecretKey = configuration.GetSection("JwtSettings")["SecretKey"]!;

        var SignInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Jwtopt.SecretKey));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    IssuerSigningKey = SignInKey,
                    ValidAudience = Jwtopt.Audience,
                    ValidIssuer = Jwtopt.Issuer,
                    ClockSkew = TimeSpan.Zero
                };
            }
        );
        return services;
    }
}
