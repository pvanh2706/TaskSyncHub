using TaskSystemHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace TaskSystemHub.Persistence
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) { }

        public DbSet<TaskItem> Tasks { get; set; }
        // dotnet ef migrations add AddScheduledIssueTable
        // dotnet ef database update
        public DbSet<ScheduledIssue> ScheduledIssues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
