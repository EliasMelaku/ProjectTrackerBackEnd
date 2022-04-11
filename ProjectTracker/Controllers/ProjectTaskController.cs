using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectTracker.Data_Transfer_Objects;
using ProjectTracker.Models;
using ProjectTracker.Repositories;

namespace ProjectTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public ProjectTaskController(ITaskRepository taskRepository)
        {
            this._taskRepository = taskRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<ProjectTaskModel>> GetProjects()
        {
            return await _taskRepository.Get();
        }

        [HttpPost]
        public async Task<ActionResult<ProjectModel>> PostProject([FromBody] TaskDTO model)
        {
            var newProject = await _taskRepository.Create(model);
            return CreatedAtAction(nameof(GetProjects), new { id = newProject.Id }, newProject);
        }
    }
}
