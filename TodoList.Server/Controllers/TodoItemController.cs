using Microsoft.AspNetCore.Mvc;
using TodoList.Server.Services;
using TodoList.Server.Data.Models;

namespace TodoList.Server.Controllers
{
    [ApiController]
	[Route("task")]
	public class TodoItemController : ControllerBase
	{
		private readonly ITodoItemService _todoItemService;

		public TodoItemController(ITodoItemService todoItemService)
		{
			_todoItemService = todoItemService;
		}

		[HttpGet("")]
		public ActionResult<IEnumerable<TodoItem>> GetAllTasks()
		{
			try
			{
				IEnumerable<TodoItem> tasks = _todoItemService.GetAllTodoItems();

				return Ok(tasks);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("{id}")]
		public ActionResult<TodoItem> GetTaskById(int id)
		{
			try
			{
				TodoItem task = _todoItemService.GetTodoItemById(id);

				return Ok(task);
			}
			catch (ArgumentException ex)
			{
				return StatusCode(404, ex.Message);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		
		[HttpPost]
		public ActionResult<int> CreateTask(TodoItem task)
		{
			try
			{
				int id = _todoItemService.CreateTodoItem(task);

				return Ok(id);
			}
			catch (ArgumentException ex)
			{
				return StatusCode(404, ex.Message);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPut]
		public ActionResult<bool> UpdateTask(TodoItem task)
		{
			try
			{
				bool result = _todoItemService.UpdateTodoItem(task);

				return Ok(result);
			}
			catch (ArgumentException ex)
			{
				return StatusCode(404, ex.Message);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpDelete("{id}")]
		public ActionResult<bool> DeleteTask(int id)
		{
			try
			{
				bool result = _todoItemService.DeleteTodoItem(id);

				return Ok(result);
			}
			catch (ArgumentException ex)
			{
				return StatusCode(404, ex.Message);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
		
	}
}
