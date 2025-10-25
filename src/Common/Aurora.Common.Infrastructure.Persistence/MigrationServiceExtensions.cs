using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Aurora.Common.Infrastructure.Persistence;

public static class MigrationServiceExtensions
{
    public static void MigrateDatabase<TContext>(
        this WebApplication application) where TContext : DbContext
    {
        ArgumentNullException.ThrowIfNull(application);
        ArgumentNullException.ThrowIfNull(application.Services);

        IServiceProvider serviceProvider = application.Services;

        using IServiceScope scope = serviceProvider.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<TContext>();

        if (context is null)
        {
            throw new ArgumentNullException(nameof(application));
        }

        context.Database.Migrate();
    }
}