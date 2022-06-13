using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjectTracker.Models
{
    [Index(nameof(Username), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    public class UserModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Profile { get; set; }

        [System.Runtime.Serialization.IgnoreDataMember]
        public byte[] PasswordHash { get; set; }

        [System.Runtime.Serialization.IgnoreDataMember]
        public byte[] PasswordSalt { get; set; }

        public List<ProjectModel>? Projects { get; set; }
    }
}
