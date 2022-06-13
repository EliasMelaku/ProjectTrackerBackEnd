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
                Description = task.Description,
                IsCompleted = task.IsCompleted,
                ProjectId = task.ProjectId
            };
            _context.Tasks.Add(myTask);
            await _context.SaveChangesAsync();

            return myTask;
        }

        public async Task Delete(int id)
        {
            var taskToDelete = await _context.Tasks.FindAsync(id);
            if (taskToDelete != null)
            {
                _context.Remove(taskToDelete);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProjectTaskModel>> Get()
        {
            return await _context.Tasks.ToListAsync();
        }

        public Task<ProjectTaskModel> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task Complete(int id)
        {
            var updatedTask = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
            updatedTask.IsCompleted = true;
            _context.Entry(updatedTask).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Update(TaskDTO task)
        {
            var updatedTask = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == task.Id);
            updatedTask.Title = task.Title;
            updatedTask.Description = task.Description;
            updatedTask.IsCompleted = task.IsCompleted;
            _context.Entry(updatedTask).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
