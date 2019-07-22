﻿using ProjectManager.Entities.Domain;
using ProjectManager.IRepository.Contracts;

namespace ProjectManager.IRepositories
{
    public interface IProjectRepository : IRepository<Project>
    {
    }
}
