using Microsoft.EntityFrameworkCore;
using TodoList.Server.Entities;

namespace TodoList.Server.Data.Configurations
{
    public class TodoItemConfiguration
    {
        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<TodoItem>();

            entity.ToTable(nameof(ApplicationDbContext.TodoItems));
        }
    }
}
