using Microsoft.AspNetCore.Mvc;
using TodoList.Server.Dtos;
using TodoList.Server.Queries;
using TodoList.Server.Services;

namespace TodoList.Server.Controllers
{
    [ApiController]
	[Route("task")]
	public class TodoItemController : CustomControllerBase
    {
		private readonly TodoItemService _todoItemService;

		public TodoItemController(TodoItemService todoItemService)
		{
			_todoItemService = todoItemService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<TodoItemQuery>>> GetAll()
		{
			var todoItems = await _todoItemService.GetAll();

			return Ok(todoItems);
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<TodoItemQuery>> GetById(int id)
		{
			var todoItem = await _todoItemService.GetById(id);

			if (todoItem == null)
			{
				return StatusCode(400);
            }

			return Ok(todoItem);
		}

		[HttpPost]
		public Task<ActionResult<int>> Create(TodoItemDto.Create.Command command)
        {
            return HandleRequest(
                command,
                _mapper.ToModel,
                model => _todoItemService.Create(model),
                _mapper.ToResponse
            );
        }

        [HttpPut("{id:int}")]
        public Task<ActionResult<TodoItemDto.Update>> Update(int id, TodoItemDto.Update.Command command)
        {
            return HandleRequest(
                command,
                _mapper.ToModel,
                model => _todoItemService.Update(id, model),
                _mapper.ToResponse
            );
        }

        [HttpPut("label/{id:int}")]
        public async Task<ActionResult<string>> UpdateLabel(int id, TodoItemDto.UpdateLabel.Command command)
        {
            var model = _mapper.ToModel(command);

            try
            {
                model = await _todoItemService.UpdateLabel(id, model);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is DbUpdateException)
            {
                return StatusCode(400);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

            var response = _mapper.ToResponse(model);

            return Ok(response);
        }

        [HttpPut("iscompleted/{id:int}")]
        public async Task<ActionResult<bool>> UpdateIsCompleted(int id, bool isCompleted)
        {
            try
            {
                isCompleted = await _todoItemService.UpdateIsCompleted(id, isCompleted);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is DbUpdateException)
            {
                return StatusCode(400);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

            return Ok(isCompleted);
        }

        [HttpDelete("{id:int}")]
		public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _todoItemService.Delete(id);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is DbUpdateException)
            {
                return StatusCode(400);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

            return Ok();
        }
		
	}
}
