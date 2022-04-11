namespace ProjectTracker.Data_Transfer_Objects
{
    public class TaskDTO
    {
        public string Title { get; set; }

        public List<string> Subtasks { get; set; }

        public int ProjectId { get; set; }
    }
}
