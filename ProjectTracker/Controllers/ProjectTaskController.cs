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
        public async Task<ActionResult<ProjectTaskModel>> PostProject([FromBody] TaskDTO model)
        {
            var newTask = await _taskRepository.Create(model);
            return CreatedAtAction(nameof(GetProjects), new { id = newTask.Id }, newTask);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _taskRepository.Delete(id);
            return Ok("Task Deleted");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTask(TaskDTO model)
        {
            await _taskRepository.Update(model);
            return Ok("Task Updated");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> CompleteTask(int id)
        {
            await _taskRepository.Complete(id);
            return Ok("Completed");
        }
    }
}
