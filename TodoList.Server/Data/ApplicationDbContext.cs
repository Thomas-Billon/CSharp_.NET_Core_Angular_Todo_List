using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TodoList.Server.Models;

namespace TodoList.Server.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{
		}

		[Key]
		public DbSet<Models.Task> Todos { get; set; }
	}
}
