using ProjectManager.DAL.Concretes;
using ProjectManager.Entities.Domain;
using ProjectManager.IRepositories;

namespace ProjectManager.DAL.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public ApplicationDbContext DataContext { get { return Context as ApplicationDbContext; } }

        public UserRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
