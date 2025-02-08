using System.ComponentModel.DataAnnotations;

namespace TodoList.Server.Entities
{
    public class TodoGroup
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
    }

    public record TodoGroupDTO(int Id, string Title);

    public static class TodoGroupExtension
    {
        public static TodoGroupDTO ToDTO(this TodoGroup x) => new TodoGroupDTO
        (
            x.Id,
            x.Title
        );

        public static TodoGroup ToEntity(this TodoGroupDTO x) => new TodoGroup
        {
            Id = x.Id,
            Title = x.Title
        };
    }
}
