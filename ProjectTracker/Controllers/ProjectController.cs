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

        [HttpGet("{id}")]
        public async Task<ProjectModel> GetProject(int id)
        {
            return await _projectRepository.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<ProjectModel>> PostProject([FromBody] ProjectDTO model)
        {
            var newProject = await _projectRepository.Create(model);
            return CreatedAtAction(nameof(GetProjects), new { id = newProject.Id }, newProject);
        }

        [HttpPut("{id}/{completion}")]
        public async Task<IActionResult> UpdateCompletion(int id, int completion)
        {
            await _projectRepository.Update(id, completion);
            return Ok();
        }
    }
}
