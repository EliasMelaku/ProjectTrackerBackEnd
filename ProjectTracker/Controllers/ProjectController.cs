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

        [HttpGet("others/{id}")]
        public async Task<IEnumerable<ProjectDTO>> GetOtherProjects(int id)
        {
            return  await _projectRepository.GetOtherProjects(id);
        }

        [HttpGet("{id}")]
        public async Task<ProjectDTO> GetProject(int id)
        {
            return await _projectRepository.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<ProjectModel>> PostProject([FromBody] NewProjectDTO model)
        {
            var newProject = await _projectRepository.Create(model);
            return CreatedAtAction(nameof(GetProjects), new { id = newProject.Id }, newProject);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditProject(int id,[FromBody] NewProjectDTO model)
        {
            await _projectRepository.Update(id, model);
            return Ok();
        }

        [HttpPut("{id}/{completion}")]
        public async Task<IActionResult> UpdateCompletion(int id, int completion)
        {
            await _projectRepository.Update(id, completion);
            return Ok();
        }

        [HttpPut("complete/{id}")]
        public async Task<IActionResult> CompleteProject(int id)
        {
            await _projectRepository.Complete(id);
            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var projectPath = await _projectRepository.Delete(id);
            if (projectPath == "noReport")
            {
                return Ok();
            }
            try
            {
                FileInfo file = new FileInfo(projectPath);
                if (file.Exists)
                {
                    file.Delete();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return Ok();
        }
    }
}
