using ProjectTracker.Models;

namespace ProjectTracker.Data_Transfer_Objects
{
    public class ProjectDTO
    {
        public int ProjectManagerId { get; set; }

        public SimpleUser ProjectManager { get; set; }

        public string? Department { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? DueDate { get; set; }

        public string? Urgency { get; set; }

        public int Completion { get; set; }

        public List<string> Deliverables { get; set; }

        public List<SimpleUser> Users { get; set; }

        public List<ProjectTaskModel> Tasks { get; set; }

        public string? Report { get; set; }

        public bool IsCompleted { get; set; }
    }
}
