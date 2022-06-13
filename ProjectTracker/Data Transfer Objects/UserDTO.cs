using ProjectTracker.Models;

namespace ProjectTracker.Data_Transfer_Objects
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Profile { get; set; }

        public List<ProjectModel>? Projects { get; set; }
    }
}
