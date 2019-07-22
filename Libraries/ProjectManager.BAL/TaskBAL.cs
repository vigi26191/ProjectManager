using ProjectManager.DAL;
using ProjectManager.DAL.Concretes;
using ProjectManager.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManager.BAL
{
    public class TaskBAL
    {
        public Task GetTask(int taskId)
        {
            Task task = null;

            using (var unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                task = unitOfWork.Tasks.Get(taskId);
            }

            return task;
        }

        public IEnumerable<Task> GetTasks()
        {
            List<Task> tasks = null;

            using (var unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                tasks = unitOfWork.Tasks.GetAll().ToList();
            }

            return tasks;
        }

        public bool SaveTask(Task Task)
        {
            using (var unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                unitOfWork.Tasks.Add(Task);

                try
                {
                    return unitOfWork.Complete() > 0;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool EndTask(int taskId, out bool taskNotFound)
        {
            using (var unitOfWork = new UnitOfWork(new ApplicationDbContext()))
            {
                Task task = unitOfWork.Tasks.Get(taskId);

                if (task != null)
                {
                    try
                    {
                        taskNotFound = false;

                        task.IsTaskComplete = true;
                        unitOfWork.Tasks.Add(task);
                        return unitOfWork.Complete() > 0;
                    }
                    catch (Exception)
                    {
                        taskNotFound = true;
                        return false;
                    }
                }
                else
                {
                    taskNotFound = true;
                }
            }

            return false;
        }
    }
}
