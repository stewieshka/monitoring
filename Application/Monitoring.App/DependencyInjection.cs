using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Monitoring.App.Services;

namespace Monitoring.App;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<AuthService>();

        services.AddScoped<BuildingService>();
        services.AddScoped<SensorService>();
        services.AddScoped<SensorRecordService>();
        services.AddScoped<ImageService>();
        
        services.AddAutoMapper(typeof(MappingProfile));

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        
        services.AddFluentValidationAutoValidation();
        
        return services;
    }
}