using System.ComponentModel.DataAnnotations;

namespace TodoList.Server.Entities
{
    public class TodoItem
    {
        [Key]
        public int Id { get; set; }
        public string Label { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;

        public TodoGroup? TodoGroup { get; set; }
        public int? TodoGroupId { get; set; }
    }
}
