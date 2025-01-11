namespace TodoList.Server.Entities
{
    public class TodoItem
    {
        public int Id { get; set; }
        public required string Label { get; set; }
        public bool IsCompleted { get; set; }
    }

    public record TodoItemDTO(int Id, string Label, bool IsCompleted);

    public static class TodoItemExtension
    {
        public static TodoItemDTO ToDTO(this TodoItem x) => new TodoItemDTO
        (
            x.Id,
            x.Label,
            x.IsCompleted
        );

        public static TodoItem ToEntity(this TodoItemDTO x) => new TodoItem
        {
            Id = x.Id,
            Label = x.Label,
            IsCompleted = x.IsCompleted
        };
    }
}
