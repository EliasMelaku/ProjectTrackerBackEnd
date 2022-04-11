using ProjectTracker.Models;
using ProjectTracker.Data_Transfer_Objects;
using ProjectTracker.Models;

namespace ProjectTrackingTool.Repositories
{
    public interface IProjectRepository
    {
        Task<IEnumerable<ProjectModel>> Get();

        Task<ProjectDTO> Get(int id);

        Task<ProjectModel> Create(ProjectDTO project);

        Task Update(ProjectDTO project);

        Task Delete(int id);
    }
}