using System;
using Microsoft.Extensions.DependencyInjection;
using MovieApp.BL.Services.Implements;
using MovieApp.BL.Services.Interfaces;

namespace MovieApp.BL;
public static class ServiceRegistration
{
	public static IServiceCollection AddServices(this IServiceCollection services)
	{
		services.AddScoped<IDirectorService, DirectorService>();
		services.AddScoped<IGenreService, GenreService>();


        return services; 
	}
    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ServiceRegistration));
        return services;
    }
}

