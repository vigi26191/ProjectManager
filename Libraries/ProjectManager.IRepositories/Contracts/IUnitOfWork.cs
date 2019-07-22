using ProjectManager.IRepositories;
using System;

namespace ProjectManager.IRepository.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IProjectRepository Projects { get; }

        IUserRepository Users { get; }

        ITaskRepository Tasks { get; }

        int Complete();
    }
}
