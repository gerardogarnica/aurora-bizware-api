using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace Aurora.Common.Infrastructure.Persistence;

public static class SeedDataServiceExtensions
{
    public static void SeedData<TContext>(
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

        var types = typeof(TContext).Assembly.GetTypes()
            .Where(x => x.IsClass && x.GetInterfaces().Contains(typeof(ISeedDataService<TContext>)))
            .ToList();

        types.ForEach(x =>
        {
            var instance = Activator.CreateInstance(x);
            var seedMethod = x?.GetMethod("Seed");
            seedMethod?.Invoke(instance, [context]);
        });

        context.SaveChanges();
    }

    public static TEntity? GetFromFile<TEntity, TContext>(this TContext context, string path)
        where TEntity : class
        where TContext : DbContext
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(path);

        string jsonContent = File.ReadAllText(path);
        return JsonSerializer.Deserialize<TEntity>(jsonContent);
    }
}