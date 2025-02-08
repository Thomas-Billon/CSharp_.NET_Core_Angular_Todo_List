using Microsoft.EntityFrameworkCore;
using TodoList.Server.Data;
using TodoList.Server.Dtos;
using TodoList.Server.Entities;

namespace TodoList.Server.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ApplicationDbContext _context;

        public TodoItemService(ApplicationDbContext context)
        {
            _context = context;
        }

        #region GET

        public async Task<IReadOnlyList<TodoItemDTO>> GetAll()
        {
            return await _context.TodoItems.AsNoTracking()
                .Select_DTO()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<TodoItemDTO>> GetAllCompletedItems()
        {
            return await _context.TodoItems.AsNoTracking()
                .Where_IsCompleted()
                .Select_DTO()
                .ToListAsync();
        }

        public async Task<TodoItemDTO?> GetById(int id)
        {
            return await _context.TodoItems.AsNoTracking()
                .Where(x => x.Id == id)
                .Select_DTO()
                .FirstOrDefaultAsync();
        }

        #endregion GET

        #region CREATE

        public async Task<TodoItemDTO?> Create(TodoItemDTO dto)
        {
            var entity = dto.ToEntity();

            if (IsCreateValid(entity) == false)
            {
                return null;
            }

            _context.TodoItems.Add(entity);

            await _context.SaveChangesAsync();

            return entity.ToDTO();
        }

        public bool IsCreateValid(TodoItem? entity)
        {
            return true;
        }

        #endregion CREATE

        #region UPDATE

        public async Task<bool> Update(TodoItemDTO dto)
        {
            var entity = await _context.TodoItems.FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (entity == null || IsUpdateValid(entity) == false)
            {
                return false;
            }

            entity.Id = dto.Id;
            entity.Title = dto.Title;
            entity.IsCompleted = dto.IsCompleted;

            await _context.SaveChangesAsync();

            return true;
        }

        public bool IsUpdateValid(TodoItem? entity)
        {
            return true;
        }

        #endregion UPDATE

        #region DELETE

        public async Task<bool> Delete(int id)
        {
            var entity = await _context.TodoItems.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null || IsDeleteValid(entity) == false)
            {
                return false;
            }

            _context.TodoItems.Remove(entity);

            await _context.SaveChangesAsync();

            return true;
        }

        public bool IsDeleteValid(TodoItem? entity)
        {
            return true;
        }

        #endregion DELETE
    }

    public static class TodoItemQuery
    {
        #region SELECT

        public static IQueryable<TodoItemDTO> Select_DTO(this IQueryable<TodoItem> source)
        {
            return source
                .Select(x => x.ToDTO());
        }

        #endregion SELECT

        #region WHERE

        public static IQueryable<TodoItem> Where_IsCompleted(this IQueryable<TodoItem> source)
        {
            return source
                .Where(x => x.IsCompleted);
        }

        #endregion WHERE
    }
}
