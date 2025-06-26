using System.Linq.Expressions;
using TodoList.Server.Entities;

namespace TodoList.Server.Queries
{
    public class TodoItemQuery
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public required bool IsCompleted { get; set; }

        public static Expression<Func<TodoItem, TodoItemQuery>> Select => x => new TodoItemQuery
        {
            Id = x.Id,
            Title = x.Title,
            IsCompleted = x.IsCompleted
        };
    }

    public static class TodoItemQueryExtension
    {
        public static IQueryable<TodoItem> Where_IsCompleted(this IQueryable<TodoItem> source)
        {
            return source
                .Where(x => x.IsCompleted);
        }
    }
}
