using Microsoft.AspNetCore.Mvc;
using TodoList.Server.Services;
using TodoList.Server.Queries;
using System.Threading.Tasks;

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

		[HttpGet]
		public async Task<ActionResult<IEnumerable<TodoItemQuery>>> GetAllTasks()
		{
			var todoItems = await _todoItemService.GetAll();

			return Ok(todoItems);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<TodoItemQuery>> GetTaskById(int id)
		{
			var todoItem = await _todoItemService.GetById(id);

			if (todoItem == null)
			{
				return StatusCode(400);
            }

			return Ok(todoItem);
		}

		
		[HttpPost]
		public async Task<ActionResult<int>> CreateTask(TodoItemQuery model)
        {
            var todoItem = await _todoItemService.Create(model);

            if (todoItem == null)
            {
                return StatusCode(400);
            }

            return Ok(todoItem);
		}

		[HttpPut]
		public async Task<ActionResult<bool>> UpdateTask(TodoItemQuery model)
        {
            var result = await _todoItemService.Update(model);

            if (result == false)
            {
                return StatusCode(400);
            }

            return Ok();
        }

		[HttpDelete("{id}")]
		public async Task<ActionResult<bool>> DeleteTask(int id)
        {
            var result = await _todoItemService.Delete(id);

            if (result == false)
            {
                return StatusCode(400);
            }

            return Ok();
        }
		
	}
}
