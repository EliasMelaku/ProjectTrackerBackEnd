using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ProjectTracker.Data_Transfer_Objects;

namespace ProjectTracker.Models
{
    [Index(nameof(ProjectManagerId), nameof(Title), IsUnique = true)]
    public class ProjectModel
    {

        private string Deliverable;
        private string User;
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

        [NotMapped]
        public List<int> Users
        {
            get
            {
                if (User.Length > 0)
                {
                    // Console.WriteLine(User.Split(delimiter).ToList()[0]);
                    var userString = User.Split(delimiter).ToList();
                    // Console.WriteLine(userString.Count + "\n\n\n\n\n\n");
                    // foreach (var user in userString)
                    // {
                    //     Console.WriteLine(user);
                    // }
                    if (userString.Count > 0)
                    {
                         return userString.Select(s => int.Parse(s)).ToList();
                        // return new List<int>();
                    }
                }


                return new List<int>();

            }
            set
            {
                User = string.Join($"{delimiter}", value);
            }
        }

        public int Completion { get; set; }

        public string? Urgency { get; set; }

        public string? Report { get; set; }

        public bool IsCompleted { get; set; }

        [System.Runtime.Serialization.IgnoreDataMember]
        [ForeignKey("ProjectManagerId")]
        public UserModel ProjectManager { get; set; }

        public List<ProjectTaskModel> Tasks { get; set; }
    }
}
