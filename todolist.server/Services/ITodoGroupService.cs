using TodoList.Server.Dtos;
using TodoList.Server.Entities;

namespace TodoList.Server.Services
{
    public interface ITodoGroupService
    {
        public Task<IReadOnlyList<TodoGroupDTO>> GetAll();
        public Task<TodoGroupDTO?> GetById(int id);

        public Task<TodoGroupDTO?> Create(TodoGroupDTO dto);
        protected bool IsCreateValid(TodoGroup? entity);

        public Task<bool> Update(TodoGroupDTO dto);
        protected bool IsUpdateValid(TodoGroup? entity);

        public Task<bool> Delete(int id);
        protected bool IsDeleteValid(TodoGroup? entity);
    }
}
