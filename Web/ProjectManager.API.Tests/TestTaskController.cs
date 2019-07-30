using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectManager.API.Controllers;
using ProjectManager.Entities;
using ProjectManager.Entities.Constants;
using ProjectManager.Entities.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;

namespace ProjectManager.API.Tests
{
    /// <summary>
    /// UnitTest(s) - Task controller
    /// </summary>
    [TestClass]
    public class TestTaskController
    {
        [TestInitialize]
        public void TestInitialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
        }

        private readonly TaskController _taskController;

        public TestTaskController()
        {
            _taskController = new TaskController();
        }

        /// <summary>
        /// GetTask function should return single task if taskId is found
        ///</summary>
        [TestMethod]
        public void TestGetTask()
        {
            int taskId = 1;

            var result = _taskController.GetTask(taskId) as OkNegotiatedContentResult<TaskDTO>;

            Assert.IsNotNull(result.Content);
        }

        /// <summary>
        /// GetTask function should return not found when invalid taskid is passed
        ///</summary>
        [TestMethod]
        public void TestGetTask_ShouldNotFindTask()
        {
            int taskId = 999999999;

            var result = _taskController.GetTask(taskId);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        /// <summary>
        /// GetTasks should return IEnumerable<TaskDTO>
        ///</summary>
        [TestMethod]
        public void TestGetTasks()
        {
            var result = _taskController.GetTasks() as OkNegotiatedContentResult<IEnumerable<TaskDTO>>;

            Assert.IsNotNull(result.Content);
        }

        /// <summary>
        /// GetTaskLookupData should return IEnumerable<TaskLookupDTO>
        ///</summary>
        [TestMethod]
        public void TestGetTaskLookupData()
        {
            var result = _taskController.GetTaskLookupData() as OkNegotiatedContentResult<TaskLookupDTO>;

            Assert.IsNotNull(result.Content);
        }

        /// <summary>
        /// SaveTask should Add a new record to table when a valid data is passed
        ///</summary>
        [TestMethod]
        public void TestSaveTask()
        {
            var task = new TaskDTO()
            {
                TaskName = "Test Task",
                TaskPriority = 1,
                IsParentTask = null,
                TaskStartDate = null,
                TaskEndDate = null,
                IsTaskComplete = null,
                ProjectId = 2,
                UserId = 1,
                ParentTaskId = null
            };

            var result = _taskController.SaveTask(task) as OkNegotiatedContentResult<string>;

            Assert.IsNotNull(result.Content);
            Assert.AreEqual($"{Constants.TASK} information {Messages.SAVE_SUCCESS}", result.Content);
        }

        /// <summary>
        /// SaveTask should update existing record when valid taskid is passed
        ///</summary>
        [TestMethod]
        public void TestSaveTask_ShouldUpdateRecord()
        {
            var task = new TaskDTO()
            {
                TaskId = 1,
                TaskName = "Test Task Updated",
                TaskPriority = 1,
                IsParentTask = null,
                TaskStartDate = null,
                TaskEndDate = null,
                IsTaskComplete = null,
                ProjectId = 2,
                UserId = 1,
                ParentTaskId = null
            };

            var result = _taskController.SaveTask(task) as OkNegotiatedContentResult<string>;

            Assert.IsNotNull(result.Content);
            Assert.AreEqual($"{Constants.TASK} information {Messages.SAVE_SUCCESS}", result.Content);

            var tasks = _taskController.GetTasks() as OkNegotiatedContentResult<IEnumerable<TaskDTO>>;
            var filteredTasks = tasks.Content.Where(w => w.TaskId == task.TaskId).ToList();

            Assert.IsNotNull(tasks.Content);
            Assert.AreEqual(1, filteredTasks.Count);

            Assert.AreEqual(task.TaskId, filteredTasks.SingleOrDefault().TaskId);
            Assert.AreEqual(task.TaskName, filteredTasks.SingleOrDefault().TaskName);
        }

        /// <summary>
        /// SaveTask should return BadRequest when any errors occur while saving date to database
        ///</summary>
        [TestMethod]
        public void TestSaveTask_ShouldReturnBadRequest()
        {
            var task = new TaskDTO()
            {
                TaskName = ""
            };

            var result = _taskController.SaveTask(task);

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        /// <summary>
        /// EndTask function should update IsTaskComplete to TRUE when valid taskid is passed
        ///</summary>
        [TestMethod]
        public void TestEndTask_ShouldEndTask()
        {
            int taskId = 1;

            var result = _taskController.EndTask(taskId) as OkNegotiatedContentResult<string>;

            Assert.IsNotNull(result.Content);
            Assert.AreEqual(Messages.TASK_END_SUCCESS, result.Content);

            var task = _taskController.GetTask(taskId) as OkNegotiatedContentResult<TaskDTO>;

            Assert.IsNotNull(task.Content);
            Assert.AreEqual(true, task.Content.IsTaskComplete);
        }

        /// <summary>
        /// EndTask function should return not found when invalid taskid is passed
        ///</summary>
        [TestMethod]
        public void TestEndTask_ShouldNotEndTask()
        {
            int taskId = 999999999;

            var result = _taskController.EndTask(taskId);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
