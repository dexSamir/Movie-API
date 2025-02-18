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
    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration Configuration)
    {
        JwtOptions jwtOpt = new JwtOptions();
        jwtOpt.Issuer = Configuration.GetRequiredSection("JwtSettings")["Issuer"]!;
        jwtOpt.Audience = Configuration.GetRequiredSection("JwtSettings")["Audience"]!;
        jwtOpt.SecretKey = Configuration.GetRequiredSection("JwtSettings")["SecretKey"]!;
        var signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOpt.SecretKey));
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    IssuerSigningKey = signInKey,
                    ValidAudience = jwtOpt.Audience,
                    ValidIssuer = jwtOpt.Issuer,
                    ClockSkew = TimeSpan.Zero

                };
            });

        return services;
    }
}
