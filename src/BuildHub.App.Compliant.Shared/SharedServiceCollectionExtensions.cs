using BuildHub.App.Compliant.Application;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;

namespace BuildHub.App.Compliant.Shared;

public static class SharedServiceCollectionExtensions
{
    public static IServiceCollection AddSharedServices(this IServiceCollection services)
    {
        services.AddMudServices();
        services.AddApplicationServices();
        return services;
    }
}