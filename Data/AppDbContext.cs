using Microsoft.EntityFrameworkCore;

namespace todo_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Models.List>().HasData(
                new Models.List { Id = 1, Name = "Default List", Description = "This is a default list." },
                new Models.List { Id = 2, Name = "Work Tasks", Description = "Tasks related to work." },
                new Models.List { Id = 3, Name = "Personal Tasks", Description = "Tasks related to personal life." }
            );

            modelBuilder.Entity<Models.Task>().HasData(
                new Models.Task { Id = 1, Title = "Sample Task", Description = "This is a sample task.", DueDate = new DateTime(2025, 08, 27), IsCompleted = false, ListId = 1 },
                new Models.Task { Id = 2, Title = "Work Task", Description = "Complete the project report.", DueDate = new DateTime(2025, 08, 23), IsCompleted = false, ListId = 2 },
                new Models.Task { Id = 3, Title = "Personal Task", Description = "Buy groceries.", DueDate = new DateTime(2025, 08, 21), IsCompleted = false, ListId = 3 }
            );
        }


        public DbSet<Models.List> Lists { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
        // Define DbSets for your entities
        // public DbSet<YourEntity> YourEntities { get; set; }
        // Example:
        // public DbSet<TodoItem> TodoItems { get; set; }
    }
}
