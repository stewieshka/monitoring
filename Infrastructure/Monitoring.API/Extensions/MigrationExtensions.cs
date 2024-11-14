using Microsoft.EntityFrameworkCore;
using Monitoring.Data.Database;

namespace Monitoring.API.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var dbContext = scope.ServiceProvider
            .GetRequiredService<MonitoringDbContext>();
        
        dbContext.Database.Migrate();
    }
}