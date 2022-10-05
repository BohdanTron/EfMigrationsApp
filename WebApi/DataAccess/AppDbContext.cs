using Microsoft.EntityFrameworkCore;

namespace WebApi.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed initial data
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "James" }, 
                new User { Id = 2, Name = "Lars" },
                new User { Id = 3, Name = "Kirk" }, 
                new User { Id = 4, Name = "Cliff" });
        }
    }
}
