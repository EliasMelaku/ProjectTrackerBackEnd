namespace ProjectTracker.Data_Transfer_Objects
{
    public class ProjectDTO
    {
        public int ProjectManagerId { get; set; }

        public string? Department { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? DueDate { get; set; }

        public int Stage { get; set; }

        public string? Report { get; set; }
    }
}
