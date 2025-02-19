using Microsoft.Extensions.DependencyInjection;
using MovieApp.BL.ExternalServices.Implements;
using MovieApp.BL.ExternalServices.Interfaces;
using MovieApp.BL.OtherServices.Implements;
using MovieApp.BL.OtherServices.Interfaces;
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
        services.AddScoped<IRentalService, RentalService>();
        services.AddScoped<IReviewService, ReviewService>();
        services.AddScoped<IHistoryService, HistoryService>(); 

        services.AddScoped<ICacheService, CacheService>();
        services.AddScoped<ICurrentUser, CurrentUser>();
        services.AddScoped<IJwtTokenHandler, JwtTokenHandler>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IFileService, FileService>();


        services.AddScoped<IReactionStrategy, MovieReactionStrategy>();
        services.AddScoped<IReactionStrategy, ReviewReactionStrategy>();
        services.AddScoped<ILikeDislikeService, LikeDislikeService>();

        services.AddScoped<IWatchProgressStrategy, MovieWatchProgressStrategy>();
        services.AddScoped<IWatchProgressService, WatchProgressService>();


        services.AddHttpContextAccessor(); 
        services.AddDistributedMemoryCache(); 
        return services; 
	}


    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ServiceRegistration));
        return services;
    }
}

