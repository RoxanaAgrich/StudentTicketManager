using Application.Abtractions;
using Infrastrucrure.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastrucrure.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureServices(this IServiceCollection services)
        => services.AddTransient<IJwtTokenService, JwtTokenService>();
}
