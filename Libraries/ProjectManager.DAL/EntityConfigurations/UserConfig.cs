using ProjectManager.Entities.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ProjectManager.DAL.EntityConfigurations
{
    public class UserConfig : EntityTypeConfiguration<User>
    {
        public UserConfig()
        {
            ToTable("tblUsers").HasKey(a => a.UserId);

            Property(p => p.UserId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.FirstName).IsRequired();

            Property(p => p.LastName).IsRequired();

            Property(p => p.EmployeeId).IsRequired();

            HasIndex(h => h.EmployeeId).IsUnique();
        }
    }
}
