using Microsoft.Extensions.DependencyInjection;
using RealStatePortal.Application.Interfaces;
using RealStatePortal.Application.Services;

namespace RealStatePortal.Application;

public static class ExtensionServices
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IPropertyService, PropertyService>();
        return services;
    }
}