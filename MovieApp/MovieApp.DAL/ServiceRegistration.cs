using Microsoft.Extensions.DependencyInjection;
using MovieApp.Core.Repositories;
using MovieApp.DAL.Repositories;

namespace MovieApp.DAL;
public static class ServiceRegistration
{
	public static IServiceCollection AddRepositories(this IServiceCollection services)
	{
		services.AddScoped<IDirectorRepository, DirectorRepository>();
		services.AddScoped<IGenreRepository, GenreRepository>();
		services.AddScoped<IActorRepository, ActorRepository>();
		services.AddScoped<ILanguageRepository, LanguageRepository>();
		services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<IRatingRepository, RatingRepository>();
		services.AddScoped<IEpisodeRepository, EpisodeRepository>();
		services.AddScoped<IReviewRepository, ReviewRepository>();

        return services; 
	}
}

