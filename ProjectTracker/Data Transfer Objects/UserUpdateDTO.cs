namespace ProjectTracker.Data_Transfer_Objects
{
    public class UserUpdateDTO
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime DOB { get; set; }
    }
}
