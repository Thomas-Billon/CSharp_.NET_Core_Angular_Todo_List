using System.Linq.Expressions;
using TodoList.Server.Models;

namespace TodoList.Server.Queries
{
    public class TodoItemQuery
    {
        public int Id { get; set; }
        public required string Label { get; set; }
        public bool IsCompleted { get; set; }

        public static Expression<Func<TodoItem, TodoItemQuery>> Select => x => x.ToTodoItemQuery();
    }

    public static class TodoItemQueryExtensions
    {
        public static TodoItemQuery ToTodoItemQuery(this TodoItem x)
        {
            return new TodoItemQuery
            {
                Id = x.Id,
                Label = x.Label,
                IsCompleted = x.IsCompleted
            };
        }

        public static TodoItem ToTodoItem(this TodoItemQuery x)
        {
            return new TodoItem
            {
                Id = x.Id,
                Label = x.Label,
                IsCompleted = x.IsCompleted
            };
        }

        public static IQueryable<TodoItem> Where_IsCompleted(this IQueryable<TodoItem> source)
        {
            return source
                .Where(x => x.IsCompleted);
        }
    }
}
