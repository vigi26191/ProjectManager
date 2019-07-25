using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using ProjectManager.Entities.DTO;
using ProjectManager.DAL.Concretes;
using ProjectManager.DAL;
using ProjectManager.Entities.Domain;

namespace ProjectManager.BAL
{
    public class TaskBAL
    {
        public TaskDTO GetTask(int taskId)
        {
            TaskDTO task = null;

            using (var unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                task = Mapper.Map<TaskDTO>(unitOfWork.Tasks.Get(taskId));
            }

            return task;
        }

        public IEnumerable<TaskDTO> GetTasks()
        {
            List<TaskDTO> tasks = null;

            using (var unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                tasks = Mapper.Map<List<TaskDTO>>(unitOfWork.Tasks.GetAll().ToList());
            }

            return tasks;
        }

        public TaskLookupDTO GetTaskLookupData()
        {
            TaskLookupDTO taskLookupDTO = null;

            using (var unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                taskLookupDTO = new TaskLookupDTO
                {
                    Projects = unitOfWork.Projects.GetAll()
                    .Where(w => w.IsProjectSuspended == null || w.IsProjectSuspended == false)
                    .Select(s => new KeyValuePair<int, string>(s.ProjectId, s.ProjectName)).ToList(),

                    ParentTasks = unitOfWork.Tasks.GetAll()
                    .Where(w => w.IsParentTask == null || w.IsParentTask == true)
                    .Select(s => new KeyValuePair<int, string>(s.TaskId, s.TaskName)).ToList(),

                    Users = unitOfWork.Users.GetAll()
                    .Select(s => new KeyValuePair<int, string>(s.UserId, s.FirstName)).ToList()
                };
            }

            return taskLookupDTO;
        }

        public bool SaveTask(TaskDTO taskDTO)
        {
            using (var unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                var taskInDB = unitOfWork.Tasks.Get(taskDTO.TaskId);

                if (taskInDB == null)
                {
                    taskInDB = Mapper.Map<Task>(taskDTO);
                    unitOfWork.Tasks.Add(taskInDB);
                }
                else
                {
                    Mapper.Map(taskDTO, taskInDB);
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

        public bool EndTask(int taskId)
        {
            using (var unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                var taskInDB = unitOfWork.Tasks.Get(taskId);

                if (taskInDB == null)
                {
                    return false;
                }
                else
                {
                    taskInDB.IsTaskComplete = true;
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
