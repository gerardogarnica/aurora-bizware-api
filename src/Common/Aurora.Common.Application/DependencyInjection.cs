using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Aurora.Common.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddCommonApplicationServices(
        this IServiceCollection services,
        Assembly[] assemblies) => services
            .AddMessagingHandlers(assemblies)
            .AddBehaviors()
            .AddDomainHandlers(assemblies)
            .AddValidators(assemblies);

    private static IServiceCollection AddMessagingHandlers(this IServiceCollection services, Assembly[] assemblies)
    {
        services
            .Scan(scan => scan.FromAssemblies(assemblies)
            .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)), publicOnly: false)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services
            .Scan(scan => scan.FromAssemblies(assemblies)
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)), publicOnly: false)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services
            .Scan(scan => scan.FromAssemblies(assemblies)
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)), publicOnly: false)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }

    private static IServiceCollection AddBehaviors(this IServiceCollection services)
    {
        services.Decorate(typeof(ICommandHandler<,>), typeof(ValidationBehavior.CommandHandler<,>));
        services.Decorate(typeof(ICommandHandler<>), typeof(ValidationBehavior.CommandBaseHandler<>));

        services.Decorate(typeof(IQueryHandler<,>), typeof(PerformanceBehavior.QueryHandler<,>));
        services.Decorate(typeof(ICommandHandler<,>), typeof(PerformanceBehavior.CommandHandler<,>));
        services.Decorate(typeof(ICommandHandler<>), typeof(PerformanceBehavior.CommandBaseHandler<>));

        services.Decorate(typeof(IQueryHandler<,>), typeof(LoggingBehavior.QueryHandler<,>));
        services.Decorate(typeof(ICommandHandler<,>), typeof(LoggingBehavior.CommandHandler<,>));
        services.Decorate(typeof(ICommandHandler<>), typeof(LoggingBehavior.CommandBaseHandler<>));

        return services;
    }

    private static IServiceCollection AddDomainHandlers(this IServiceCollection services, Assembly[] assemblies)
    {
        services
            .Scan(scan => scan.FromAssemblies(assemblies)
            .AddClasses(classes => classes.AssignableTo(typeof(IDomainEventHandler<>)), publicOnly: false)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }

    private static IServiceCollection AddValidators(this IServiceCollection services, Assembly[] assemblies)
    {
        services.AddValidatorsFromAssemblies(assemblies, includeInternalTypes: true);

        return services;
    }
}