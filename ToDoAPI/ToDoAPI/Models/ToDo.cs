namespace ToDoAPI.Models
{
    public class ToDo
    {
        public Guid Id { get; set; }
        public string Task { get; set; }
        public string Priority { get; set; }
        public string Category { get; set; }
    }
}
