namespace todo_API.Models
{
    public class List
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Task> Tasks { get; set; } = new List<Task>();

    }
}
