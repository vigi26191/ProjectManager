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
    /// UnitTest(s) - User controller
    /// </summary>
    [TestClass]
    public class TestUserController
    {
        [TestInitialize]
        public void TestInitialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
        }

        private readonly UserController _userController;

        public TestUserController()
        {
            _userController = new UserController();
        }

        /// <summary>
        /// GetUser function should return single user if userId is found
        ///</summary>
        [TestMethod]
        public void TestGetUser()
        {
            int userId = 1;

            var result = _userController.GetUser(userId) as OkNegotiatedContentResult<UserDTO>;

            Assert.IsNotNull(result.Content);
        }

        /// <summary>
        /// GetUser function should return not found when invalid userid is passed
        ///</summary>
        [TestMethod]
        public void TestGetUser_ShouldNotFindUser()
        {
            int userId = 999999999;

            var result = _userController.GetUser(userId);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        /// <summary>
        /// GetUsers should return IEnumerable<UserDTO>
        ///</summary>
        [TestMethod]
        public void TestGetUsers()
        {
            var result = _userController.GetUsers() as OkNegotiatedContentResult<IEnumerable<UserDTO>>;

            Assert.IsNotNull(result.Content);
        }

        /// <summary>
        /// SaveUser should Add a new record to table when a valid data is passed
        ///</summary>
        [TestMethod]
        public void TestSaveUser()
        {
            var user = new UserDTO()
            {
                FirstName = "Test FirstName",
                LastName = "Test LastName",
                EmployeeId = 12345
            };

            var result = _userController.SaveUser(user) as OkNegotiatedContentResult<string>;

            Assert.IsNotNull(result.Content);
            Assert.AreEqual($"{Constants.USER} information for '{user.FirstName}' {Messages.SAVE_SUCCESS}", 
                            result.Content);
        }

        /// <summary>
        /// SaveUser should return BadRequest when any errors occur while saving date to database
        ///</summary>
        [TestMethod]
        public void TestSaveUser_ShouldReturnBadRequest()
        {
            var user = new UserDTO()
            {
                FirstName = "",
                LastName = "",
                EmployeeId = 123
            };

            var result = _userController.SaveUser(user);

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        /// <summary>
        /// DeleteUser function should update delete user from database
        ///</summary>
        [TestMethod]
        public void TestDeleteUser_ShouldRemoveUser()
        {
            int userId = 2008;

            var result = _userController.DeleteUser(userId) as OkNegotiatedContentResult<string>;

            Assert.IsNotNull(result.Content);
            Assert.AreEqual(Messages.DELETE_SUCCESS, result.Content);
        }

        /// <summary>
        /// DeleteUser function should return not found when invalid userid is passed
        ///</summary>
        [TestMethod]
        public void TestDeleteUser_ShouldNotAbleToRemoveUser()
        {
            int userId = 999999999;

            var result = _userController.DeleteUser(userId);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        /// <summary>
        /// SuspendUser function should return not found when invalid userid is passed
        ///</summary>
        [TestMethod]
        public void TestDeleteUser_ShouldNotAbleToRemoveUser_When_Referenced_In_Other_Tables()
        {
            int userId = 1;

            var result = _userController.DeleteUser(userId) as BadRequestErrorMessageResult;

            Assert.IsNotNull(result.Message);
            Assert.IsNotNull(Messages.USER_DELTE_FAILURE, result.Message);
        }
    }
}
