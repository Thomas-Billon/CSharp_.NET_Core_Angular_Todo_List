using TodoList.Server.Data.Models;

namespace TodoList.Server.Services
{
    public interface ITodoItemService
	{
		public IEnumerable<TodoItem> GetAllTodoItems();
		public TodoItem GetTodoItemById(int id);
		public int CreateTodoItem(TodoItem task);
		public bool UpdateTodoItem(TodoItem task);
		public bool DeleteTodoItem(int id);
	}
}
