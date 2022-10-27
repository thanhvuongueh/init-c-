using InitalWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InitalWebAPI.Contexts
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {
        }

        public DbSet<Dog> Dogs { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<ExampleObject1>()
            //                .HasMany(obj1 => obj1.obj2)
            //                .WithOne(obj2 => obj2.obj1)
            //                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
