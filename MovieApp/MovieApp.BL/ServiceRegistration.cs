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


        services.AddScoped<IFileService, FileService>();
        return services; 
	}
    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ServiceRegistration));
        return services;
    }
}

