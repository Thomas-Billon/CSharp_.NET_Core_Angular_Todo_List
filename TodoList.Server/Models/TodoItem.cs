namespace TodoList.Server.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Label { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;
    }
}
