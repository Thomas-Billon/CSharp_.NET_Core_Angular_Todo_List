using System.Linq.Expressions;
using TodoList.Server.Entities;

namespace TodoList.Server.Queries
{
    public class TodoGroupQuery
    {
        public required int Id { get; set; }
        public required string Title { get; set; }

        public static Expression<Func<TodoGroup, TodoGroupQuery>> Select => x => new TodoGroupQuery
        {
            Id = x.Id,
            Title = x.Title
        };
    }
}
