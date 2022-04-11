using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectTracker.Data_Transfer_Objects;

namespace ProjectTracker.Models
{
    public class ProjectModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }

        public int ProjectManagerId { get; set; }

        public string? Department { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? DueDate { get; set; }

        public int Stage { get; set; }

        public string? Report { get; set; }

        [System.Runtime.Serialization.IgnoreDataMember]
        [ForeignKey("ProjectManagerId")]
        public UserModel ProjectManager { get; set; }

        public List<ProjectTaskModel> Tasks { get; set; }
    }
}
