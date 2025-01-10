using Microsoft.EntityFrameworkCore;
using TodoList.Server.Data;
using TodoList.Server.Models;
using TodoList.Server.Queries;
using TodoList.Server.Services.Base;

namespace TodoList.Server.Services
{
    public class TodoItemService : ServiceBase, ITodoItemService
    {
        private readonly ApplicationDbContext _context;

        public TodoItemService(ApplicationDbContext context)
        {
            _context = context;
        }

		public async Task<IReadOnlyList<TodoItemQuery>> GetAll()
        {
            return await _context.TodoItems.AsNoTracking()
                .Select(TodoItemQuery.Select)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<TodoItemQuery>> GetAllCompletedItems()
        {
            return await _context.TodoItems.AsNoTracking()
                .Where_IsCompleted()
                .Select(TodoItemQuery.Select)
                .ToListAsync();
        }

        public async Task<TodoItemQuery?> GetById(int id)
        {
            return await _context.TodoItems.AsNoTracking()
                .Where(x => x.Id == id)
                .Select(TodoItemQuery.Select)
                .FirstOrDefaultAsync();
        }

        public async Task<TodoItemQuery?> Create(TodoItemQuery query)
        {
            var data = query.ToTodoItem();

            if (IsCreateValid(data) == false)
            {
                return null;
            }

            _context.TodoItems.Add(data);

            await _context.SaveChangesAsync();

            return data.ToTodoItemQuery();
        }

        private bool IsCreateValid(TodoItem? data)
        {
            return true;
        }

        public async Task<bool> Update(TodoItemQuery query)
        {
            var data = await _context.TodoItems.FirstOrDefaultAsync(x => x.Id == query.Id);
            if (data == null || IsUpdateValid(data) == false)
            {
                return false;
            }

            data.Id = query.Id;
            data.Label = query.Label;
            data.IsCompleted = query.IsCompleted;

            await _context.SaveChangesAsync();

            return true;
        }

        private bool IsUpdateValid(TodoItem? data)
        {
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var data = await _context.TodoItems.FirstOrDefaultAsync(x => x.Id == id);
            if (data == null || IsDeleteValid(data) == false)
            {
                return false;
            }

            _context.TodoItems.Remove(data);

            await _context.SaveChangesAsync();

            return true;
        }

        private bool IsDeleteValid(TodoItem? data)
        {
            return true;
        }
    }
}
