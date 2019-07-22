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

        public bool SaveProject(ProjectDTO project)
        {
            using (var unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                var projectInDB = unitOfWork.Projects.Get(project.ProjectId);

                if (projectInDB == null)
                {
                    var newProject = Mapper.Map<Project>(project);
                    unitOfWork.Projects.Add(newProject);
                }
                else
                {
                    Mapper.Map(project, projectInDB);
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
                    unitOfWork.Projects.Add(projectInDB);
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
