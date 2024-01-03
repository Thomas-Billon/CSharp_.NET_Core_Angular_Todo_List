using TodoList.Server.Repositories;
using Task = TodoList.Server.Models.Task;

namespace TodoList.Server.Services
{
	public class TaskService : ITaskService
	{
		private readonly ITaskRepository _taskRepository;

		public TaskService(ITaskRepository taskRepository)
		{
			_taskRepository = taskRepository;
		}

		public IEnumerable<Task> GetAllTasks()
		{
			return _taskRepository.GetAll();
		}

		public Task GetTaskById(int id)
		{
			return _taskRepository.Get(id);
		}

		public int CreateTask(Task task)
		{
			return _taskRepository.Create(task);
		}

		public bool UpdateTask(Task task)
		{
			return _taskRepository.Update(task);
		}

		public bool DeleteTask(int id)
		{
			return _taskRepository.Delete(id);
		}
	}
}
