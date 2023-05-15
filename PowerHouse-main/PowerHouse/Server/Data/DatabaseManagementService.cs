using Microsoft.EntityFrameworkCore;

namespace PowerHouse.Server.Data
{
    public static class DatabaseManagementService
    {
        public static IApplicationBuilder MigrateDatabase(this IApplicationBuilder app)
        {
            using (var scoped = app.ApplicationServices.CreateScope())
            {
                using (var context = scoped.ServiceProvider.GetService<PowerHouseDbContext>())
                {
                    try
                    {
                        context.Database.EnsureDeleted();
                        context.Database.Migrate();
                        return app;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }
    }
}
