using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Aurora.Common.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceInterceptors(this IServiceCollection services)
    {
        services.AddSingleton<AuditableEntitiesInterceptor>();
        services.AddSingleton<SoftDeletableEntitiesInterceptor>();

        return services;
    }
}