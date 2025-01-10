using Microsoft.EntityFrameworkCore;
using TodoList.Server.Models;

namespace TodoList.Server.Data
{
    public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<TodoItem> TodoItems { get; set; }
	}
}
