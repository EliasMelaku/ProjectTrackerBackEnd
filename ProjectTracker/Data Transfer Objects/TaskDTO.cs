namespace ProjectTracker.Data_Transfer_Objects
{
    public class TaskDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; }

        public int ProjectId { get; set; }
    }
}
