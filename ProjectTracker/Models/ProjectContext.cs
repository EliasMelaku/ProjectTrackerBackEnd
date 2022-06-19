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
            modelBuilder.Entity<ProjectModel>().Property<string>("Deliverable").HasField("Deliverable");
            modelBuilder.Entity<ProjectModel>().Property<string>("User").HasField("User");

        }

        public DbSet<ProjectModel> Projects { get; set; }

        public DbSet<UserModel> Users { get; set;}

        public DbSet<ProjectTaskModel> Tasks { get; set; }
    }
}
