using ProjectManager.DAL.Concretes;
using ProjectManager.Entities.Domain;
using ProjectManager.IRepositories;

namespace ProjectManager.DAL.Repository
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ApplicationDbContext DataContext { get { return Context as ApplicationDbContext; } }

        public ProjectRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
