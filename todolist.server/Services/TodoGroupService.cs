using Microsoft.EntityFrameworkCore;
using TodoList.Server.Data;
using TodoList.Server.Entities;

namespace TodoList.Server.Services
{
    public class TodoGroupService : ITodoGroupService
    {
        private readonly ApplicationDbContext _context;

        public TodoGroupService(ApplicationDbContext context)
        {
            _context = context;
        }

        #region GET

        public async Task<IReadOnlyList<TodoGroupDTO>> GetAll()
        {
            return await _context.TodoGroups.AsNoTracking() 
                .Select_DTO()
                .ToListAsync();
        }

        public async Task<TodoGroupDTO?> GetById(int id)
        {
            return await _context.TodoGroups.AsNoTracking()
                .Where(x => x.Id == id)
                .Select_DTO()
                .FirstOrDefaultAsync();
        }

        #endregion GET

        #region CREATE

        public async Task<TodoGroupDTO?> Create(TodoGroupDTO dto)
        {
            var entity = dto.ToEntity();

            if (IsCreateValid(entity) == false)
            {
                return null;
            }

            _context.TodoGroups.Add(entity);

            await _context.SaveChangesAsync();

            return entity.ToDTO();
        }

        public bool IsCreateValid(TodoGroup? entity)
        {
            return true;
        }

        #endregion CREATE

        #region UPDATE

        public async Task<bool> Update(TodoGroupDTO dto)
        {
            var entity = await _context.TodoGroups.FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (entity == null || IsUpdateValid(entity) == false)
            {
                return false;
            }

            entity.Id = dto.Id;
            entity.Title = dto.Title;

            await _context.SaveChangesAsync();

            return true;
        }

        public bool IsUpdateValid(TodoGroup? entity)
        {
            return true;
        }

        #endregion UPDATE

        #region DELETE

        public async Task<bool> Delete(int id)
        {
            var entity = await _context.TodoGroups.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null || IsDeleteValid(entity) == false)
            {
                return false;
            }

            _context.TodoGroups.Remove(entity);

            await _context.SaveChangesAsync();

            return true;
        }

        public bool IsDeleteValid(TodoGroup? entity)
        {
            return true;
        }

        #endregion DELETE
    }

    public static class TodoGroupQuery
    {
        #region SELECT

        public static IQueryable<TodoGroupDTO> Select_DTO(this IQueryable<TodoGroup> source)
        {
            return source
                .Select(x => x.ToDTO());
        }

        #endregion SELECT
    }
}
