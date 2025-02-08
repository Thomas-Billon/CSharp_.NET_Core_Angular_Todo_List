using TodoList.Server.Entities;

namespace TodoList.Server.Dtos
{
    public record TodoItemDTO(
        int Id,
        string Title,
        bool IsCompleted
    );

    public static class TodoItemExtension
    {
        public static TodoItemDTO ToDTO(this TodoItem x) => new TodoItemDTO
        (
            x.Id,
            x.Title,
            x.IsCompleted
        );

        public static TodoItem ToEntity(this TodoItemDTO x) => new TodoItem
        {
            Id = x.Id,
            Title = x.Title,
            IsCompleted = x.IsCompleted
        };
    }
}
