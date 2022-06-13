using ProjectTracker.Models;
using ProjectTracker.Data_Transfer_Objects;
using ProjectTracker.Models;

namespace ProjectTrackingTool.Repositories
{
    public interface IProjectRepository
    {
        Task<IEnumerable<ProjectModel>> Get();

        Task<ProjectModel> Get(int id);

        Task<ProjectModel> Create(ProjectDTO project);

        Task Update(ProjectDTO project);

        Task Update(int id, int completion);

        Task Delete(int id);
    }
}