using Microsoft.EntityFrameworkCore;
using TaskTracker.Abstractions.Entities;

namespace TaskTracker.Db
{
    public class TaskTrackerContext : DbContext
    {
        public DbSet<TaskEntity> Tasks { get; set; }

        public TaskTrackerContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskEntity>()
                .HasIndex(t => t.Name)
                .IsUnique();
        }
    }
}
