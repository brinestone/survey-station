using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SurvStation.Infra.Contexts;
using SurvStation.Api.Config;

namespace SurvStation.Api.Extensions;

internal static class DataExtensions
{
    internal static IServiceCollection AddDbContexts(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddDbContext<FormsContext<Guid>>((services, builder) =>
        {
            var options = services.GetRequiredService<IOptions<DatabaseConnectionOptions>>();
            builder.UseNpgsql(options.Value.Forms)
            .UseSnakeCaseNamingConvention();
        });
    }
}
