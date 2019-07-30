using AutoMapper;
using ProjectManager.DAL;
using ProjectManager.DAL.Concretes;
using ProjectManager.Entities.Domain;
using ProjectManager.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManager.BAL
{
    public class ProjectBAL
    {
        public ProjectDTO GetProject(int projectId)
        {
            ProjectDTO project = null;

            using (var unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                project = Mapper.Map<ProjectDTO>(unitOfWork.Projects.Get(projectId));
            }

            return project;
        }

        public IEnumerable<ProjectDTO> GetProjects()
        {
            List<ProjectDTO> projects = null;

            using (var unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                projects = Mapper.Map<List<ProjectDTO>>(unitOfWork.Projects.GetAll().ToList());
            }

            return projects;
        }

        public ProjectLookupDTO GetProjectLookupData()
        {
            ProjectLookupDTO projectLookupDTO = null;

            using (var unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                projectLookupDTO = new ProjectLookupDTO
                {
                    Users = unitOfWork.Users.GetAll().Where(w => w.IsActive == true).ToList()
                    .Select(s => new KeyValuePair<int, string>(s.UserId, s.FirstName)).ToList(),

                    Projects = Mapper.Map<List<ProjectDTO>>(unitOfWork.Projects.GetAll().ToList())
                };
            }

            return projectLookupDTO;
        }

        public bool SaveProject(ProjectDTO projectDTO)
        {
            using (var unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                var projectInDB = unitOfWork.Projects.Get(projectDTO.ProjectId);

                if (projectInDB == null)
                {
                    projectInDB = Mapper.Map<Project>(projectDTO);
                    unitOfWork.Projects.Add(projectInDB);
                }
                else
                {
                    Mapper.Map(projectDTO, projectInDB);
                }

                try
                {
                    return unitOfWork.Complete() > 0;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public bool SuspendProject(int projectId)
        {
            using (var unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                var projectInDB = unitOfWork.Projects.Get(projectId);

                if (projectInDB == null)
                {
                    return false;
                }
                else
                {
                    projectInDB.IsProjectSuspended = true;

                    var tasks = unitOfWork.Tasks.GetAll().Where(w => w.ProjectId == projectId).ToList();
                    foreach (var task in tasks)
                    {
                        task.IsTaskComplete = true;
                    }
                }

                try
                {
                    return unitOfWork.Complete() > 0;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
    }
}
