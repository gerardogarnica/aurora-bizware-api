using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Aurora.Bizware.Api.Settings;

public sealed class SwaggerGenOptionsSetup : IConfigureNamedOptions<SwaggerGenOptions>
{
    private const string apiName = "Aurora Bizware";
    private const string apiDescription = "Aurora Bizware";
    private const string apiVersion = "v1.0";

    public void Configure(string? name, SwaggerGenOptions options)
    {
        Configure(options);
    }

    public void Configure(SwaggerGenOptions options)
    {
        options.SwaggerDoc(apiVersion, CreateOpenApiInfo());

        options.CustomSchemaIds(type => type.FullName?.Replace("+", "."));
        options.DescribeAllParametersInCamelCase();

        options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, CreateOpenApiSecurityScheme());

        options.AddSecurityRequirement(CreateOpenApiSecurityRequirement());
    }

    private static OpenApiInfo CreateOpenApiInfo()
    {
        return new OpenApiInfo()
        {
            Title = $"{apiName} API",
            Description = apiDescription,
            Version = apiVersion,
            Contact = new OpenApiContact
            {
                Name = "Aurora Support",
                Email = "support@aurosoft.ec"
            }
        };
    }

    private static OpenApiSecurityScheme CreateOpenApiSecurityScheme()
    {
        return new OpenApiSecurityScheme()
        {
            BearerFormat = "JWT",
            Description = "JWT authorization header using the Bearer scheme.\r\n\r\n" +
                "Enter the word 'Bearer' [space] and the security token.\r\n\r\n" +
                "Example: \"Bearer 12345abcdef\"",
            Name = HeaderNames.Authorization,
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = JwtBearerDefaults.AuthenticationScheme
        };
    }

    private static OpenApiSecurityRequirement CreateOpenApiSecurityRequirement()
    {
        return new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference()
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = JwtBearerDefaults.AuthenticationScheme
                    },
                    Scheme = "oauth2",
                    Name = JwtBearerDefaults.AuthenticationScheme,
                    In = ParameterLocation.Header
                },
                Array.Empty<string>()
            }
        };
    }
}