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
		return services; 
	}
}

