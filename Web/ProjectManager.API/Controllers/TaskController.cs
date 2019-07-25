using ProjectManager.BAL;
using ProjectManager.Entities;
using ProjectManager.Entities.Constants;
using ProjectManager.Entities.DTO;
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
        [Route("lookupTask")]
        public IHttpActionResult GetTaskLookupData()
        {
            var lookupData = _taskBAL.GetTaskLookupData();

            if (lookupData != null)
            {
                return Ok(lookupData);
            }

            return NotFound();
        }

        [HttpPost]
        [Route("saveTask")]
        public IHttpActionResult SaveTask(TaskDTO task)
        {
            if (ModelState.IsValid)
            {
                var result = _taskBAL.SaveTask(task);

                if (result)
                {
                    return Ok($"{Constants.TASK} information {Messages.SAVE_SUCCESS}");
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
        [Route("endTask/{taskId:int}")]
        public IHttpActionResult EndTask(int taskId)
        {
            var result = _taskBAL.EndTask(taskId);

            if (result)
            {
                return Ok($"{Messages.TASK_END_SUCCESS}");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
