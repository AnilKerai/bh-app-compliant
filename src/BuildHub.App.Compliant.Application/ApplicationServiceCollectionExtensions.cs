using BuildHub.App.Compliant.Application.Services;
using BuildHub.App.Compliant.External;
using Microsoft.Extensions.DependencyInjection;

namespace BuildHub.App.Compliant.Application;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IProjectService, ProjectService>();
        services.AddExternalBuildHubServices();
        return services;
    }
}