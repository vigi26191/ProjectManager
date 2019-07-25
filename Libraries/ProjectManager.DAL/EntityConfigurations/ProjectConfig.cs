using ProjectManager.Entities.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ProjectManager.DAL.EntityConfigurations
{
    public class ProjectConfig : EntityTypeConfiguration<Project>
    {
        public ProjectConfig()
        {
            ToTable("tblProjects").HasKey(a => a.ProjectId);

            Property(p => p.ProjectId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.ProjectName).IsRequired();

            Property(p => p.ProjectStartDate).IsOptional();

            Property(p => p.ProjectEndDate).IsOptional();

            Property(p => p.ProjectPriority).IsRequired();

            Property(p => p.IsProjectSuspended).IsOptional();

            HasRequired(h => h.User).WithMany(u => u.Projects)
                .HasForeignKey(p => p.UserId).WillCascadeOnDelete(false);
        }
    }
}
