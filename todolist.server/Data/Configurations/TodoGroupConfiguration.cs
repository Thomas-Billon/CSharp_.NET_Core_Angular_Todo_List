using Microsoft.EntityFrameworkCore;
using TodoList.Server.Entities;

namespace TodoList.Server.Data
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
