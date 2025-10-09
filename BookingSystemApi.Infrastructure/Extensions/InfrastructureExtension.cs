using BookingSystemApi.Infrastructure.Auth;
using BookingSystemApi.Infrastructure.Interfaces.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace BookingSystemApi.Infrastructure.Extensions;

public static class InfrastructureExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IJwtProvider, JwtProvider>();

        return services;
    }
}