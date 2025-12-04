using Aurora.Bizware.Api.Settings;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Aurora.Bizware.Api.Extensions;

internal static class ApplicationBuilderExtensions
{
    internal static IServiceCollection AddSwaggerGenWithAuth(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new()
            {
                Title = "Aurora Bizware API",
                Version = "v1"
            });
        });

        services.Configure<JsonOptions>(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        services.ConfigureOptions<SwaggerGenOptionsSetup>();

        return services;
    }

    internal static IApplicationBuilder UseSwaggerWithUI(this WebApplication app)
    {
        app.UseSwagger();

        app.UseSwaggerUI(options =>
        {
            options.DocumentTitle = "Aurora Bizware API";
        });

        return app;
    }
}