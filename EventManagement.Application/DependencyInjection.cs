using EventManagement.Application.Interfaces;
using EventManagement.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EventManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IEventService, EventService>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
