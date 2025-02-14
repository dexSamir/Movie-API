using Microsoft.Extensions.DependencyInjection;
using MovieApp.BL.ExternalServices.Implements;
using MovieApp.BL.ExternalServices.Interfaces;
using MovieApp.BL.Services.Implements;
using MovieApp.BL.Services.Interfaces;

namespace MovieApp.BL;
public static class ServiceRegistration
{
	public static IServiceCollection AddServices(this IServiceCollection services)
	{
		services.AddScoped<IDirectorService, DirectorService>();
		services.AddScoped<IGenreService, GenreService>();
        services.AddScoped<IActorService, ActorService>();
        services.AddScoped<ILanguageService, LanguageService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IMovieService, MovieService>();
        services.AddScoped<IRatingService, RatingService>();
        services.AddScoped<IReviewService, ReviewService>(); 

        services.AddScoped<ICacheService, CacheService>();
        services.AddScoped<ICurrentUser, CurrentUser>();
        services.AddScoped<IJwtTokenHandler, JwtTokenHandler>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IFileService, FileService>();

        services.AddDistributedMemoryCache(); 
        return services; 
	}


    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ServiceRegistration));
        return services;
    }
}

