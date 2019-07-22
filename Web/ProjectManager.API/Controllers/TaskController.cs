using ProjectManager.BAL;
using ProjectManager.Entities;
using ProjectManager.Entities.Constants;
using ProjectManager.Entities.Domain;
using System.Web.Http;

namespace ProjectManager.API.Controllers
{
    [RoutePrefix("api/task")]
    public class TaskController : ApiController
    {
        private readonly TaskBAL _taskBAL;

        public TaskController()
        {
            _taskBAL = new TaskBAL();
        }

        [HttpGet]
        [Route("getTask/{taskId:int}")]
        public IHttpActionResult GetTask(int taskId)
        {
            var task = _taskBAL.GetTask(taskId);

            if (task != null)
            {
                return Ok(task);
            }

            return NotFound();
        }

        [HttpGet]
        [Route("getTasks")]
        public IHttpActionResult GetTasks()
        {
            var tasks = _taskBAL.GetTasks();

            if (tasks != null)
            {
                return Ok(tasks);
            }

            return NotFound();
        }

        [HttpGet]
        [Route("endTask/{taskId:int}")]
        public IHttpActionResult endTask(int taskId)
        {
            bool taskNotFound = false;

            var result = _taskBAL.EndTask(taskId, out taskNotFound);

            if (result)
            {
                return Ok($"{Messages.TASK_END_SUCCESS}");
            }
            else
            {
                if (taskNotFound)
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest();
                }
            }
        }
    }
}
