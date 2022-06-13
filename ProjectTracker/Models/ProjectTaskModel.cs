using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectTracker.Models
{
    public class ProjectTaskModel
    {

        // private string SubTask;
        // private string delimiter = ",";

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }

        // [NotMapped] // This makes it a single column in the table / it won't create a different table
        // public List<string> SubTasks
        // {
        //     get  { return SubTask.Split(delimiter).ToList()  ; }
        //     set { SubTask = string.Join($"{delimiter}", value); }
        // }

        public string Description { get; set; }

        public bool IsCompleted { get; set; }

        public int ProjectId { get; set; }

        [System.Runtime.Serialization.IgnoreDataMember]
        [ForeignKey("ProjectId")]
        public ProjectModel Project { get; set; }

    }
}
