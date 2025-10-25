using Aurora.Common.Application.Time;
using Aurora.Common.Infrastructure.DomainEvents;
using Aurora.Common.Infrastructure.Time;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Quartz;

namespace Aurora.Common.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddDateTimeServices(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeService, DateTimeService>();

        return services;
    }

    public static IServiceCollection AddDomainEventsDispatcher(this IServiceCollection services)
    {
        services.AddTransient<IDomainEventsDispatcher, DomainEventsDispatcher>();

        return services;
    }

    public static IServiceCollection AddQuartzConfiguration(this IServiceCollection services)
    {
        services.AddQuartz();
        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

        return services;
    }
}