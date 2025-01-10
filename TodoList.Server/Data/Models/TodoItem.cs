namespace TodoList.Server.Data.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public required string Label { get; set; }
        public bool IsCompleted { get; set; }
    }
}
