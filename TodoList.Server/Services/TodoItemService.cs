using Microsoft.EntityFrameworkCore;
using TodoList.Server.Data;
using TodoList.Server.Entities;
using TodoList.Server.Queries;

namespace TodoList.Server.Services
{
    public class TodoItemService : CustomServiceBase
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

        public Task<TodoItemQuery?> GetById(int id)
        {
            return _context.TodoItems.AsNoTracking()
                .Where(x => x.Id == id)
                .Select(TodoItemQuery.Select)
                .FirstOrDefaultAsync();
        }

        public async Task<TodoItem> Create(TodoItem entity)
        {
            Validate(entity, nameof(Create));

            _context.TodoItems.Add(entity);

            await _context.SaveChangesAsync();

            EnsureCreated(entity);

            return entity;
        }

        public async Task<TodoItem> Update(int id, TodoItem entity)
        {
            Validate(entity, nameof(Update));

            var updateCount = await _context.TodoItems
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(x => x.Title, entity.Title)
                    .SetProperty(x => x.IsCompleted, entity.IsCompleted)
                );

            EnsureUpdated(updateCount);

            return entity;
        }

        public async Task<string> UpdateTitle(int id, string title)
        {
            if (!IsTitleValid(title))
            {
                throw new ArgumentException(nameof(title), "Parameter is invalid for update");
            }

            var updateCount = await _context.TodoItems
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(x => x.Title, title)
                );

            if (updateCount != 1)
            {
                throw new DbUpdateException("No entries were updated");
            }

            return title;
        }

        public async Task<bool> UpdateIsCompleted(int id, bool isCompleted)
        {
            var updateCount = await _context.TodoItems
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(x => x.IsCompleted, isCompleted)
                );

            if (updateCount != 1)
            {
                throw new DbUpdateException("No entries were updated");
            }

            return isCompleted;
        }

        public async Task Delete(int id)
        {
            var deleteCount = await _context.TodoItems
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();

            if (deleteCount != 1)
            {
                throw new DbUpdateException("No entries were deleted");
            }
        }

        private void Validate(TodoItem entity, string operation)
        {
            if (!IsValid(entity))
            {
                throw new ArgumentException(nameof(entity), $"Parameter is invalid for {operation}");
            }
        }

        private bool IsValid(TodoItem entity) => IsTitleValid(entity.Title);

        private bool IsTitleValid(string title) => title != null;

        private void EnsureCreated(TodoItem entity)
        {
            if (entity == null)
            {
                throw new DbUpdateException("No entries were created");
            }
        }

        private void EnsureUpdated(int updateCount)
        {
            if (updateCount != 1)
            {
                throw new DbUpdateException("No entries were updated");
            }
        }
    }
}
