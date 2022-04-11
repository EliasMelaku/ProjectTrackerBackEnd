using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectTracker.Data_Transfer_Objects;
using ProjectTracker.Models;
using ProjectTrackingTool.Repositories;

namespace ProjectTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectController(IProjectRepository projectRepository)
        {
            this._projectRepository = projectRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<ProjectModel>> GetProjects()
        {
            return await _projectRepository.Get();
        }

        [HttpPost]
        public async Task<ActionResult<ProjectModel>> PostProject([FromBody] ProjectDTO model)
        {
            var newProject = await _projectRepository.Create(model);
            return CreatedAtAction(nameof(GetProjects), new { id = newProject.Id }, newProject);
        }
    }
}
