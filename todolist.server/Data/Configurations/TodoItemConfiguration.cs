using Microsoft.EntityFrameworkCore;
using TodoList.Server.Data;
using TodoList.Server.Entities;

namespace todolist.server.Data.Configurations
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
