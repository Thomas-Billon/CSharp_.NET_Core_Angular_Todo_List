using Microsoft.EntityFrameworkCore;
using System.Linq;
using TodoList.Server.Data;
using Task = TodoList.Server.Models.Task;

namespace TodoList.Server.Repositories
{
	public class TaskRepository : ITaskRepository
	{
		private readonly ApplicationDbContext _context;

		public TaskRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public IEnumerable<Task> GetAll()
		{
			return _context.Todos.Select(t => t);
		}

		public Task Get(int id)
		{
			Task? task = _context.Todos.FirstOrDefault(t => t.Id == id);
			if (task == null)
			{
				throw new ArgumentException($"Error: Id {id} not found");
			}

			return task;
		}

		public int Create(Task item)
		{
			_context.Todos.Add(item);
			_context.SaveChanges();

			return item.Id;
		}

		public bool Update(Task item)
		{
			Task? task = _context.Todos.FirstOrDefault(t => t.Id == item.Id);
			if (task == null)
			{
				throw new ArgumentException($"Error: Id {item.Id} not found");
			}

			task.Id = item.Id;
			task.Label = item.Label;
			task.IsCompleted = item.IsCompleted;
			_context.SaveChanges();

			return true;
		}

		public bool Delete(int id)
		{
			Task? task = _context.Todos.FirstOrDefault(t => t.Id == id);
			if (task == null)
			{
				throw new ArgumentException($"Error: Id {id} not found");
			}

			_context.Todos.Remove(task);
			_context.SaveChanges();

			return true;
		}

		public bool Save()
		{
			_context.SaveChanges();

			return true;
		}
	}
}
