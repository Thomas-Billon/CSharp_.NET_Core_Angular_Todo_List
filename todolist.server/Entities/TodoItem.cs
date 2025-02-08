using System.ComponentModel.DataAnnotations;

namespace TodoList.Server.Entities
{
    public class TodoItem
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }

        public TodoGroup? TodoGroup { get; set; }
        public int? TodoGroupId { get; set; }
    }

    public record TodoItemDTO(int Id, string Title, bool IsCompleted);

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
