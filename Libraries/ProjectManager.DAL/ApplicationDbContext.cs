using ProjectManager.DAL.EntityConfigurations;
using ProjectManager.Entities.Domain;
using System.Data.Entity;

namespace ProjectManager.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("name=ProjectManagerContext")
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new ProjectConfig());
            modelBuilder.Configurations.Add(new UserConfig());
            modelBuilder.Configurations.Add(new TaskConfig());
        }
    }
}
