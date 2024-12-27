using Microsoft.Extensions.DependencyInjection;

namespace BuildHub.App.Compliant.External;

public static class ExternalBuildHubServiceCollectionExtensions
{
    public static IServiceCollection AddExternalBuildHubServices(this IServiceCollection services)
    {
        services.AddHttpClient();
        services.AddScoped<IBuildHubClient, BuildHubClient>();
        return services;
    }
}