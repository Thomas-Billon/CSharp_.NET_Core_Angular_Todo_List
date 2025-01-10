using TodoList.Server.Queries;

namespace TodoList.Server.Services
{
    public interface ITodoItemService
	{
		public Task<IReadOnlyList<TodoItemQuery>> GetAll();
		public Task<TodoItemQuery?> GetById(int id);
		public Task<TodoItemQuery?> Create(TodoItemQuery task);
		public Task<bool> Update(TodoItemQuery task);
		public Task<bool> Delete(int id);
	}
}
