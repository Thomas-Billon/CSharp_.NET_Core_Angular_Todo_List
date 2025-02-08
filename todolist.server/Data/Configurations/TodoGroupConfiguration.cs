using Microsoft.EntityFrameworkCore;
using TodoList.Server.Data;
using TodoList.Server.Entities;

namespace todolist.server.Data.Configurations
{
    public class TodoGroupConfiguration
    {
        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<TodoGroup>();

            entity.ToTable(nameof(ApplicationDbContext.TodoGroups));

            entity
                .HasMany(x => x.TodoItems)
                .WithOne(x => x.TodoGroup)
                .HasForeignKey(nameof(TodoItem.TodoGroupId))
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
