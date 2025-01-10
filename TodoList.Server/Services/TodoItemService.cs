using TodoList.Server.Data.Models;
using TodoList.Server.Data;

namespace TodoList.Server.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ApplicationDbContext _context;

        public TodoItemService(ApplicationDbContext context)
        {
            _context = context;
        }

		public IEnumerable<TodoItem> GetAllTodoItems()
        {
            return _context.TodoItems.Select(t => t);
        }

		public TodoItem GetTodoItemById(int id)
        {
            TodoItem? item = _context.TodoItems.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                throw new ArgumentException($"Error: Id {id} not found");
            }

            return item;
        }

		public int CreateTodoItem(TodoItem item)
        {
            _context.TodoItems.Add(item);
            _context.SaveChanges();

            return item.Id;
        }

		public bool UpdateTodoItem(TodoItem item)
        {
            TodoItem? dbItem = _context.TodoItems.FirstOrDefault(t => t.Id == item.Id);
            if (dbItem == null)
            {
                throw new ArgumentException($"Error: Id {item.Id} not found");
            }

            dbItem.Id = item.Id;
            dbItem.Label = item.Label;
            dbItem.IsCompleted = item.IsCompleted;
            _context.SaveChanges();

            return true;
        }

		public bool DeleteTodoItem(int id)
        {
            TodoItem? task = _context.TodoItems.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                throw new ArgumentException($"Error: Id {id} not found");
            }

            _context.TodoItems.Remove(task);
            _context.SaveChanges();

            return true;
        }
	}
}
