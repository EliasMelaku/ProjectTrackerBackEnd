using ProjectTracker.Models;
using ProjectTracker.Data_Transfer_Objects;
using ProjectTracker.Models;

namespace ProjectTrackingTool.Repositories
{
    public interface IProjectRepository
    {
        Task<IEnumerable<ProjectModel>> Get();

        Task<IEnumerable<ProjectDTO>> GetOtherProjects(int id);

        Task<ProjectDTO> Get(int id);

        Task<string> GetReport(int id);

        Task<ProjectModel> Create(NewProjectDTO project);

        Task Update(int id, NewProjectDTO project);

        Task Update(int id, int completion);

        Task Update(int id, string path);

        Task Complete(int id);

        Task<string> Delete(int id);
    }
}