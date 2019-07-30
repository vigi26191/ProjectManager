using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectManager.API.Controllers;
using ProjectManager.Entities;
using ProjectManager.Entities.Constants;
using ProjectManager.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Web.Http.Results;
using System.Linq;

namespace ProjectManager.API.Tests
{
    /// <summary>
    /// UnitTest(s) - Project controller
    /// </summary>
    [TestClass]
    public class TestProjectController
    {
        [TestInitialize]
        public void TestInitialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
        }

        private readonly ProjectController _projectController;

        public TestProjectController()
        {
            _projectController = new ProjectController();
        }

        /// <summary>
        /// GetProject function should return single project if projectId is found
        ///</summary>
        [TestMethod]
        public void TestGetProject()
        {
            int projectId = 1;

            var result = _projectController.GetProject(projectId) as OkNegotiatedContentResult<ProjectDTO>;

            Assert.IsNotNull(result.Content);
        }

        /// <summary>
        /// GetProject function should return not found when invalid projectid is passed
        ///</summary>
        [TestMethod]
        public void TestGetProject_ShouldNotFindProject()
        {
            int projectId = 999999999;

            var result = _projectController.GetProject(projectId);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        /// <summary>
        /// GetProjects should return IEnumerable<ProjectDTO>
        ///</summary>
        [TestMethod]
        public void TestGetProjects()
        {
            var result = _projectController.GetProjects() as OkNegotiatedContentResult<IEnumerable<ProjectDTO>>;

            Assert.IsNotNull(result.Content);
        }

        /// <summary>
        /// GetProjectLookupData should return IEnumerable<ProjectLookupDTO>
        ///</summary>
        [TestMethod]
        public void TestGetProjectLookupData()
        {
            var result = _projectController.GetProjectLookupData() as OkNegotiatedContentResult<ProjectLookupDTO>;

            Assert.IsNotNull(result.Content);
        }

        /// <summary>
        /// SaveProject should Add a new record to table when a valid data is passed
        ///</summary>
        [TestMethod]
        public void TestSaveProject()
        {
            var project = new ProjectDTO()
            {
                ProjectName = "ProjectTest",
                ProjectPriority = 30,
                ProjectStartDate = DateTime.Now,
                ProjectEndDate = DateTime.Now,
                IsProjectSuspended = null,
                UserId = 1
            };

            var result = _projectController.SaveProject(project) as OkNegotiatedContentResult<string>;

            Assert.IsNotNull(result.Content);
            Assert.AreEqual($"{Constants.PROJECT} information {Messages.SAVE_SUCCESS}", result.Content);
        }

        /// <summary>
        /// SaveProject should update existing record when valid projectid is passed
        ///</summary>
        [TestMethod]
        public void TestSaveProject_ShouldUpdateRecord()
        {
            var project = new ProjectDTO()
            {
                ProjectId = 1,
                ProjectName = "ProjectTest 1009",
                ProjectPriority = 30,
                ProjectStartDate = DateTime.Now,
                ProjectEndDate = DateTime.Now,
                IsProjectSuspended = null,
                UserId = 1
            };

            var result = _projectController.SaveProject(project) as OkNegotiatedContentResult<string>;

            Assert.IsNotNull(result.Content);
            Assert.AreEqual($"{Constants.PROJECT} information {Messages.SAVE_SUCCESS}", result.Content);

            var projects = _projectController.GetProjects() as OkNegotiatedContentResult<IEnumerable<ProjectDTO>>;
            var filteredProjects = projects.Content.Where(w => w.ProjectId == project.ProjectId).ToList();

            Assert.IsNotNull(projects.Content);
            Assert.AreEqual(1, filteredProjects.Count);

            Assert.AreEqual(project.ProjectId, filteredProjects.SingleOrDefault().ProjectId);
            Assert.AreEqual(project.ProjectName, filteredProjects.SingleOrDefault().ProjectName);
            Assert.AreEqual(project.ProjectPriority, filteredProjects.SingleOrDefault().ProjectPriority);
            Assert.AreEqual(project.UserId, filteredProjects.SingleOrDefault().UserId);
        }

        /// <summary>
        /// SaveProject should return BadRequest when any errors occur while saving date to database
        ///</summary>
        [TestMethod]
        public void TestSaveProject_ShouldReturnBadRequest()
        {
            var project = new ProjectDTO()
            {
                ProjectPriority = 31,
                ProjectStartDate = DateTime.Now,
                ProjectEndDate = DateTime.Now,
                IsProjectSuspended = null,
            };

            var result = _projectController.SaveProject(project);

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        /// <summary>
        /// SuspendProject function should update IsProjectSuspended to TRUE when valid projectid is passed
        ///</summary>
        [TestMethod]
        public void TestSuspendProject_ShouldSuspendProject()
        {
            int projectId = 1;

            var result = _projectController.SuspendProject(projectId) as OkNegotiatedContentResult<string>;

            Assert.IsNotNull(result.Content);
            Assert.AreEqual(Messages.PROJECT_SUSPENDED_SUCCESS, result.Content);

            var project = _projectController.GetProject(projectId) as OkNegotiatedContentResult<ProjectDTO>;

            Assert.IsNotNull(project.Content);
            Assert.AreEqual(true, project.Content.IsProjectSuspended);
        }

        /// <summary>
        /// SuspendProject function should return not found when invalid projectid is passed
        ///</summary>
        [TestMethod]
        public void TestSuspendProject_ShouldNotSuspendProject()
        {
            int projectId = 999999999;

            var result = _projectController.SuspendProject(projectId);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

    }
}
