using Task = TodoList.Server.Models.Task;

namespace TodoList.Server.Services
{
	public interface ITaskService
	{
		public IEnumerable<Task> GetAllTasks();
		public Task GetTaskById(int id);
		public int CreateTask(Task task);
		public bool UpdateTask(Task task);
		public bool DeleteTask(int id);
	}
}
