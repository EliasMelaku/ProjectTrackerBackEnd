using Microsoft.EntityFrameworkCore;
using ProjectTracker.Data_Transfer_Objects;
using ProjectTracker.Models;

namespace ProjectTracker.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ProjectContext _context;

        public TaskRepository(ProjectContext context)
        {
            this._context = context;
        }


        public async Task<ProjectTaskModel> Create(TaskDTO task)
        {
            var myTask = new ProjectTaskModel
            {
                Title = task.Title,
                SubTasks = task.Subtasks,
                ProjectId = task.ProjectId,
            };
            _context.Tasks.Add(myTask);
            await _context.SaveChangesAsync();

            return myTask;
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProjectTaskModel>> Get()
        {
            return await _context.Tasks.ToListAsync();
        }

        public Task<ProjectTaskModel> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(ProjectTaskModel task)
        {
            throw new NotImplementedException();
        }
    }
}
