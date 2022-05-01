using Microsoft.Data.SqlClient;
using Polly;

namespace CatalogService.Api.Infrastructure.Context;

public class CatalogContextSeed
{
    public async Task SeedAsync(CatalogContext context, IWebHostEnvironment env, ILogger<CatalogContextSeed> logger)
    {
        var policy = Policy.Handle<SqlException>().
            WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                onRetry: (exception, timeSpan, retry, ctx) =>
                {
                    logger.LogWarning(exception, "[{prefix}] Exception {ExceptionType} with message {Message} detected on attempt {retry} of");
                }
            );

        var setupDirPath = Path.Combine(env.ContentRootPath, "Infrastructure", "Setup", "SeedFiles");
        var picturePath = "Pics";

        await policy.ExecuteAsync(() => ProcessSeeding(context, setupDirPath, picturePath, logger));
    }

    private async Task ProcessSeeding(CatalogContext context, string setupDirPath, string picturePath, ILogger logger)
    {
        if (!context.CatalogBrands.Any())
        {
            await context.CatalogBrands.AddRangeAsync(GetCatalogBrandsFromFile(setupDirPath));

            await context.SaveChangesAsync();
        }
    }
}
