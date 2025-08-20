using System.Text.Json.Serialization;

namespace todo_API.Models
{
    public class Task
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime DueDate { get; set; }

        public bool IsCompleted { get; set; }

        public int ListId { get; set; }

        [JsonIgnore]  // 🚀 Prevents circular reference

        public List List { get; set; } // Navigation property to the List entity
    }
}
