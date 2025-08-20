using Microsoft.EntityFrameworkCore;

namespace todo_API.Data
{
    public static class DbInit
    {
        public static void Migrate(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // Apply any pending migrations
            db.Database.Migrate();
        }
    }
}
