using ProjectManager.BAL;
using ProjectManager.Entities;
using ProjectManager.Entities.Constants;
using ProjectManager.Entities.DTO;
using System.Web.Http;

namespace ProjectManager.API.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private readonly UserBAL _userBAL;

        public UserController()
        {
            _userBAL = new UserBAL();
        }

        [HttpGet]
        [Route("getUser/{userId:int}")]
        public IHttpActionResult GetUser(int userId)
        {
            var user = _userBAL.GetUser(userId);

            if (user != null)
            {
                return Ok(user);
            }

            return NotFound();
        }

        [HttpGet]
        [Route("getUsers")]
        public IHttpActionResult GetUsers()
        {
            var users = _userBAL.GetUsers();

            if (users != null)
            {
                return Ok(users);
            }

            return NotFound();
        }

        [HttpPost]
        [Route("saveUser")]
        public IHttpActionResult SaveUser(UserDTO user)
        {
            if (ModelState.IsValid)
            {
                var result = _userBAL.SaveUser(user);

                if (result)
                {
                    return Ok($"{Constants.USER} information for '{user.FirstName}' {Messages.SAVE_SUCCESS}");
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost]
        [Route("removeUser/{userId:int}")]
        public IHttpActionResult DeleteUser(int userId)
        {
            bool isUserRecordInUse = false;
            var result = _userBAL.DeleteUser(userId, out isUserRecordInUse);

            if (result)
            {
                return Ok(Messages.DELETE_SUCCESS);
            }
            else if (isUserRecordInUse)
            {
                return BadRequest(Messages.USER_DELTE_FAILURE);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
