using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectTracker.Data_Transfer_Objects;

namespace ProjectTracker.Models
{
    public class ProjectModel
    {

        private string Deliverable;
        private string delimiter = "~";

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }

        public int ProjectManagerId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string? Department { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? DueDate { get; set; }

        [NotMapped]
        public List<string> Deliverables
        {
            get { return Deliverable.Split(delimiter).ToList(); }
            set { Deliverable = string.Join($"{delimiter}", value);  }
        }

        public int Completion { get; set; }

        public string? Urgency { get; set; }

        public string? Report { get; set; }

        [System.Runtime.Serialization.IgnoreDataMember]
        [ForeignKey("ProjectManagerId")]
        public UserModel ProjectManager { get; set; }

        public List<ProjectTaskModel> Tasks { get; set; }
    }
}
