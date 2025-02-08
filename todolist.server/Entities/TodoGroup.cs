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
}
