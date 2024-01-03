using Task = TodoList.Server.Models.Task;

namespace TodoList.Server.Repositories
{
	public interface ITaskRepository : IRepository<Task>
	{
	}
}
