using TodoList.Server.Dtos;
using TodoList.Server.Entities;

namespace TodoList.Server.Services
{
    public interface ITodoItemService
	{
        public Task<IReadOnlyList<TodoItemDTO>> GetAll();
        public Task<TodoItemDTO?> GetById(int id);

        public Task<TodoItemDTO?> Create(TodoItemDTO dto);
        protected bool IsCreateValid(TodoItem? entity);

        public Task<bool> Update(TodoItemDTO dto);
        protected bool IsUpdateValid(TodoItem? entity);

        public Task<bool> Delete(int id);
        protected bool IsDeleteValid(TodoItem? entity);
    }
}
