using ProjectManager.BAL;
using ProjectManager.Entities;
using ProjectManager.Entities.Constants;
using ProjectManager.Entities.DTO;
using System.Web.Http;

namespace ProjectManager.API.Controllers
{
    [RoutePrefix("api/project")]
    public class ProjectController : ApiController
    {
        private readonly ProjectBAL _projectBAL;

        public ProjectController()
        {
            _projectBAL = new ProjectBAL();
        }

        [HttpGet]
        [Route("getProject/{projectId:int}")]
        public IHttpActionResult GetProject(int projectId)
        {
            var project = _projectBAL.GetProject(projectId);

            if (project != null)
            {
                return Ok(project);
            }

            return NotFound();
        }

        [HttpGet]
        [Route("getProjects")]
        public IHttpActionResult GetProjects()
        {
            var projects = _projectBAL.GetProjects();

            if (projects != null)
            {
                return Ok(projects);
            }

            return NotFound();
        }

        [HttpGet]
        [Route("lookupProject")]
        public IHttpActionResult GetProjectLookupData()
        {
            var lookupData = _projectBAL.GetProjectLookupData();

            if (lookupData != null)
            {
                return Ok(lookupData);
            }

            return NotFound();
        }

        [HttpPost]
        [Route("saveProject")]
        public IHttpActionResult SaveProject(ProjectDTO project)
        {
            if (ModelState.IsValid)
            {
                var result = _projectBAL.SaveProject(project);

                if (result)
                {
                    return Ok($"{Constants.PROJECT} information {Messages.SAVE_SUCCESS}");
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
        [Route("suspendProject/{projectId:int}")]
        public IHttpActionResult SuspendProject(int projectId)
        {
            var result = _projectBAL.SuspendProject(projectId);

            if (result)
            {
                return Ok(Messages.PROJECT_SUSPENDED_SUCCESS);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
