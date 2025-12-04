namespace Aurora.Bizware.Api.Extensions;

internal static class ConfigurationExtensions
{
    internal static void AddModuleConfigurations(this IConfigurationBuilder builder, string[] modules)
    {
        foreach (string moduleName in modules)
        {
            string moduleConfigFile = $"modules.{moduleName.ToLowerInvariant()}.json";
            builder.AddJsonFile(moduleConfigFile, optional: false, reloadOnChange: true);

            string moduleDevConfigFile = $"modules.{moduleName.ToLowerInvariant()}.Development.json";
            builder.AddJsonFile(moduleDevConfigFile, optional: true, reloadOnChange: true);
        }
    }
}