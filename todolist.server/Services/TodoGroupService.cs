using Microsoft.EntityFrameworkCore;
using TodoList.Server.Data;
using TodoList.Server.Entities;
using TodoList.Server.Queries;

namespace TodoList.Server.Services
{
    public class TodoGroupService : CustomServiceBase
    {
        private readonly ApplicationDbContext _context;

        public TodoGroupService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<TodoGroupQuery>> GetAll()
        {
            return await _context.TodoGroups.AsNoTracking() 
                .Select(TodoGroupQuery.Select)
                .ToListAsync();
        }

        public async Task<TodoGroupQuery?> GetById(int id)
        {
            return await _context.TodoGroups.AsNoTracking()
                .Where(x => x.Id == id)
                .Select(TodoGroupQuery.Select)
                .FirstOrDefaultAsync();
        }

        public async Task<TodoGroup?> Create(TodoGroup entity)
        {
            Validate(entity, nameof(Create));

            _context.TodoGroups.Add(entity);

            await _context.SaveChangesAsync();

            EnsureCreated(entity);

            return entity;
        }

        public async Task<TodoGroup> Update(int id, TodoGroup entity)
        {
            Validate(entity, nameof(Update));

            var updateCount = await _context.TodoGroups
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(x => x.Title, entity.Title)
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

            var updateCount = await _context.TodoGroups
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

        public async Task Delete(int id)
        {
            var deleteCount = await _context.TodoGroups
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();

            if (deleteCount != 1)
            {
                throw new DbUpdateException("No entries were deleted");
            }
        }
    }
}
