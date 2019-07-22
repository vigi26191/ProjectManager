using ProjectManager.DAL.Concretes;
using ProjectManager.Entities.Domain;
using ProjectManager.IRepositories;

namespace ProjectManager.DAL.Repository
{
    public class TaskRepository : Repository<Task>, ITaskRepository
    {
        public ApplicationDbContext DataContext { get { return Context as ApplicationDbContext; } }

        public TaskRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
