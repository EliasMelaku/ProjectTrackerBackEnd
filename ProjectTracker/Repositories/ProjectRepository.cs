using Microsoft.EntityFrameworkCore;
using ProjectTracker.Data_Transfer_Objects;
using ProjectTracker.Models;
using ProjectTrackingTool.Repositories;

namespace ProjectTracker.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ProjectContext _context;

        public ProjectRepository(ProjectContext context)
        {
            this._context = context;
        }


        public async Task<ProjectModel> Create(NewProjectDTO project)
        {
            var newProject = new ProjectModel
            {
                Department = project.Department,
                ProjectManagerId = project.ProjectManagerId,
                Title = project.Title,
                Description = project.Description,
                CreatedDate = project.CreatedDate,
                DueDate = project.DueDate,
                Urgency = project.Urgency,
                Completion = project.Completion,
                Deliverables = project.Deliverables,
                Users = project.Users,
                Report = project.Report,
                IsCompleted = false
            };

            _context.Projects.Add(newProject);
            await _context.SaveChangesAsync();

            return newProject;
        }

        public async Task<string> Delete(int id)
        {
            string reportPath = "noReport";
            var projectToDelete = await _context.Projects.FindAsync(id);
            if (projectToDelete != null)
            {
                reportPath = projectToDelete.Report;
                _context.Remove(projectToDelete);
            }

            await _context.SaveChangesAsync();
            return reportPath;
        }

        public async Task<IEnumerable<ProjectModel>> Get()
        {
            var projects = await _context.Projects.ToListAsync();
            // var projectDtos = new List<ProjectDTO>();
            //
            // foreach (ProjectModel project in projects)
            // {
            //     projectDtos.Add(new ProjectDTO
            //     {
            //         ProjectManagerId = project.ProjectManagerId,
            //         Department = project.Department,
            //         Title = project.Title,
            //         Description = project.Description,
            //         CreatedDate = project.CreatedDate,
            //         DueDate = project.DueDate,
            //         Urgency = project.Urgency,
            //         Completion = project.Completion,
            //         Deliverables = project.Deliverables,
            //         Report = project.Report
            //
            //     });
            //     
            // }
            

            return projects;
        }

        public async Task<IEnumerable<ProjectDTO>> GetOtherProjects(int id)
        {
            List<ProjectDTO> otherProjects = new List<ProjectDTO>();
            SimpleUser simplePm = new SimpleUser();
            var projects = await _context.Projects.ToListAsync();
            foreach (var project in projects)
            {
                if (project.Users.Contains(id))
                {
                    var pm = await _context.Users.FirstOrDefaultAsync(u => u.Id == project.ProjectManagerId);
                    simplePm.Id = pm.Id;
                    simplePm.Username = pm.Username;
                    simplePm.Profile = pm.Profile;
                    var projectDto = new ProjectDTO
                    {
                        ProjectManagerId = project.ProjectManagerId,
                        ProjectManager = simplePm,
                        Department = project.Department,
                        Title = project.Title,
                        Description = project.Description,
                        CreatedDate = project.CreatedDate,
                        DueDate = project.DueDate,
                        Urgency = project.Urgency,
                        Completion = project.Completion,
                        Report = project.Report,
                        IsCompleted = project.IsCompleted
                        
                    };
                    otherProjects.Add(projectDto);
                }
            }
            return otherProjects;
        }

        public async Task<ProjectDTO> Get(int id)
        {
            Console.WriteLine("Here");
            List<SimpleUser> simpleUser = new List<SimpleUser>();
            SimpleUser simplePm = new SimpleUser();
            ProjectDTO project = new ProjectDTO();
            var baseProject = await _context.Projects.Include(p => p.Tasks).FirstOrDefaultAsync(p => p.Id == id);
            if (baseProject == null)
            {
                return null;
            }

            var users = await _context.Users.Where(u => baseProject.Users.Contains(u.Id)).ToListAsync();
            foreach (UserModel user in users)
            {
                simpleUser.Add(new SimpleUser
                {
                    Id = user.Id, 
                    Username = user.Username, 
                    Profile = user.Profile,
                });
            }

            var projectManager = await _context.Users.FirstOrDefaultAsync(u => u.Id == baseProject.ProjectManagerId);
            simplePm.Id = projectManager.Id;
            simplePm.Username = projectManager.Username;
            simplePm.Profile = projectManager.Profile;

            project.ProjectManagerId = baseProject.ProjectManagerId;
            project.ProjectManager = simplePm;
            project.Department = baseProject.Department;
            project.Title = baseProject.Title;
            project.Description = baseProject.Description;
            project.CreatedDate = baseProject.CreatedDate;
            project.DueDate = baseProject.DueDate;
            project.Urgency = baseProject.Urgency;
            project.Completion = baseProject.Completion;
            project.Deliverables = baseProject.Deliverables;
            project.Report = baseProject.Report;
            project.Tasks = baseProject.Tasks;
            project.Users = simpleUser;
            project.IsCompleted = baseProject.IsCompleted;
            // Console.WriteLine(baseProject.Users[0]);

            return project;



        }

        public async Task<string> GetReport(int id)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);
            return project.Report;
        }

        public async Task Update(int id, NewProjectDTO project)
        {
            var updatedProject = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);
            updatedProject.Title = project.Title;
            updatedProject.Description = project.Description;
            updatedProject.Department = project.Department;
            updatedProject.DueDate = project.DueDate;
            updatedProject.Deliverables = project.Deliverables;
            updatedProject.Users = project.Users;
            updatedProject.Urgency = project.Urgency;
            _context.Entry(updatedProject).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, int completion)
        {
            var updatedProject = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);
            updatedProject.Completion = completion;
            _context.Entry(updatedProject).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, string path)
        {
            var updatedProject = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);
            updatedProject.Report = path;
            updatedProject.IsCompleted = true;
            _context.Entry(updatedProject).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Complete(int id)
        {
            var updatedProject = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);
            updatedProject.IsCompleted = true;
            await _context.SaveChangesAsync();
        }
    }
}
