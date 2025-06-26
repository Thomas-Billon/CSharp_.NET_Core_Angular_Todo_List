using Microsoft.EntityFrameworkCore;
using TodoList.Server.Data.Configurations;
using TodoList.Server.Entities;

namespace TodoList.Server.Data
{
    public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{
		}

        public virtual DbSet<TodoGroup> TodoGroups { get; set; }
        public virtual DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            TodoGroupConfiguration.OnModelCreating(modelBuilder);
            TodoItemConfiguration.OnModelCreating(modelBuilder);
        }
    }
}
