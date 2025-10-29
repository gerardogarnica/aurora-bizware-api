using Aurora.Bizware.Api.Settings;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Aurora.Bizware.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddSwaggerGenWithAuth(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new()
            {
                Title = "Aurora Bizware API",
                Version = "v1"
            });
        });

        builder.Services.Configure<JsonOptions>(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        builder.Services.ConfigureOptions<SwaggerGenOptionsSetup>();

        return builder.Services;
    }

    public static IApplicationBuilder UseSwaggerWithUI(this WebApplication app)
    {
        app.UseSwagger();

        app.UseSwaggerUI(options =>
        {
            options.DocumentTitle = "Aurora Bizware API";
        });

        return app;
    }
}