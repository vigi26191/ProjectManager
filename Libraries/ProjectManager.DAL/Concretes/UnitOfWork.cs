using ProjectManager.DAL.Repository;
using ProjectManager.IRepositories;
using ProjectManager.IRepository.Contracts;

namespace ProjectManager.DAL.Concretes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Initialize();
        }

        public IProjectRepository Projects { get; set; }

        public IUserRepository Users { get; set; }

        public ITaskRepository Tasks { get; set; }

        public void Initialize()
        {
            Projects = new ProjectRepository(_context);
            Users = new UserRepository(_context);
            Tasks = new TaskRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
