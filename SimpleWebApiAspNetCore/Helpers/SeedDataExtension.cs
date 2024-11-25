using SimpleWebApiAspNetCore.Repositories;
using SimpleWebApiAspNetCore.Services;

namespace SimpleWebApiAspNetCore.Helpers;

public static class SeedDataExtension
{
    public static void SeedData(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var seedDataService = scope.ServiceProvider.GetRequiredService<ISeedDataService>();
            seedDataService.Initialize(dbContext);
        }
    }
}
