﻿using Microsoft.EntityFrameworkCore;
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


        public async Task<ProjectModel> Create(ProjectDTO project)
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
                Report = project.Report,
            };

            _context.Projects.Add(newProject);
            await _context.SaveChangesAsync();

            return newProject;
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProjectModel>> Get()
        {
            var projects = await _context.Projects.Include(p => p.Tasks).ToListAsync();
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

        public async Task<ProjectModel> Get(int id)
        {
            return await _context.Projects.Include(p => p.Tasks).FirstOrDefaultAsync(p => p.Id == id);
            
        }

        public Task Update(ProjectDTO project)
        {
            throw new NotImplementedException();
        }

        public async Task Update(int id, int completion)
        {
            var updatedProject = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);
            updatedProject.Completion = completion;
            _context.Entry(updatedProject).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
