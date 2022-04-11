using ProjectTracker.Data_Transfer_Objects;
using ProjectTracker.Models;

namespace ProjectTracker.Repositories
{
    public interface ITaskRepository
    {
       
            Task<IEnumerable<ProjectTaskModel>> Get();

            Task<ProjectTaskModel> Get(int id);

            Task<ProjectTaskModel> Create(TaskDTO task);

            Task Update(ProjectTaskModel task);

            Task Delete(int id);
    }
}
