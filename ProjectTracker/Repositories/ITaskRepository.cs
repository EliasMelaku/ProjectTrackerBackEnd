using ProjectTracker.Data_Transfer_Objects;
using ProjectTracker.Models;

namespace ProjectTracker.Repositories
{
    public interface ITaskRepository
    {
       
            Task<IEnumerable<ProjectTaskModel>> Get();

            Task<ProjectTaskModel> Get(int id);

            Task<ProjectTaskModel> Create(TaskDTO task);

            Task Update(TaskDTO task);

            Task Complete(int id);

            Task Delete(int id);
    }
}
