using SurvStation.Api.Config;

namespace SurvStation.Api.Extensions;

internal static class ConfigExtensions
{
    internal static void ConfigureOptions(this IServiceCollection collection, ConfigurationManager configManager)
    {
        collection.Configure<DatabaseConnectionOptions>(configManager.GetSection("ConnectionStrings"));
    }
}
