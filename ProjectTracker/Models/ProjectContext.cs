using Microsoft.EntityFrameworkCore;

namespace ProjectTracker.Models
{
    public class ProjectContext : DbContext
    {

        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ProjectTaskModel>().Property<string>("SubTask").HasField("SubTask");
            
        }

        public DbSet<ProjectModel> Projects { get; set; }

        public DbSet<UserModel> Users { get; set;}

        public DbSet<ProjectTaskModel> Tasks { get; set; }
    }
}
