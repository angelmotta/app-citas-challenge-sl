using Microsoft.EntityFrameworkCore;

namespace Citas_API.Data;

public static class DataExtensions
{
    public static void MigrateDb(this WebApplication app) {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppStoreContext>();
        dbContext.Database.Migrate();
    }
}
