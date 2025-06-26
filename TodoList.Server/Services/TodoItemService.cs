using Microsoft.EntityFrameworkCore;
using System.Linq;
using TodoList.Server.Data;
using TodoList.Server.Dtos;
using TodoList.Server.Models;
using TodoList.Server.Queries;

namespace TodoList.Server.Services
{
    public class TodoItemService : ServiceBase
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

        public async Task<TodoItem> Create(TodoItem model)
        {
            Validate(model, nameof(Create));

            _context.TodoItems.Add(model);

            await _context.SaveChangesAsync();

            EnsureCreated(model);

            return model;
        }

        public async Task<TodoItem> Update(int id, TodoItem model)
        {
            Validate(model, nameof(Update));

            var updateCount = await _context.TodoItems
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(x => x.Label, model.Label)
                    .SetProperty(x => x.IsCompleted, model.IsCompleted)
                );

            EnsureUpdated(updateCount);

            return model;
        }

        public async Task<string> UpdateLabel(int id, string label)
        {
            if (!IsLabelValid(label))
            {
                throw new ArgumentException(nameof(label), "Parameter is invalid for update");
            }

            var updateCount = await _context.TodoItems
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(x => x.Label, label)
                );

            if (updateCount != 1)
            {
                throw new DbUpdateException("No entries were updated");
            }

            return label;
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

        private void Validate(TodoItem model, string operation)
        {
            if (!IsValid(model))
            {
                throw new ArgumentException(nameof(model), $"Parameter is invalid for {operation}");
            }
        }

        private bool IsValid(TodoItem model) => IsLabelValid(model.Label);

        private bool IsLabelValid(string label) => label != null;

        private void EnsureCreated(TodoItem model)
        {
            if (model == null)
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
