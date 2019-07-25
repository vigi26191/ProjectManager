using ProjectManager.Entities.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ProjectManager.DAL.EntityConfigurations
{
    public class TaskConfig : EntityTypeConfiguration<Task>
    {
        public TaskConfig()
        {
            ToTable("tblTasks").HasKey(a => a.TaskId);

            Property(p => p.TaskId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.TaskName).IsRequired();

            Property(p => p.TaskPriority).IsOptional();

            Property(p => p.IsParentTask).IsOptional();

            Property(p => p.TaskStartDate).IsOptional();

            Property(p => p.TaskEndDate).IsOptional();

            Property(p => p.IsTaskComplete).IsOptional();

            HasRequired(h => h.Project).WithMany(u => u.Tasks)
                .HasForeignKey(p => p.ProjectId).WillCascadeOnDelete(false);

            HasRequired(h => h.User).WithMany(u => u.Tasks)
                .HasForeignKey(p => p.UserId).WillCascadeOnDelete(false);

            HasOptional(h => h.ParentTask).WithMany()
                .HasForeignKey(p => p.ParentTaskId).WillCascadeOnDelete(false);
        }
    }
}

