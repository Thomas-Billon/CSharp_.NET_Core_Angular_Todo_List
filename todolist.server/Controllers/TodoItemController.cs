using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Server.Dtos;
using TodoList.Server.Entities;
using TodoList.Server.Mappers;
using TodoList.Server.Queries;
using TodoList.Server.Services;

namespace TodoList.Server.Controllers
{
    [ApiController]
	[Route("todo")]
	public class TodoItemController : CustomControllerBase
    {
		private readonly TodoItemService _todoItemService;
        private readonly TodoItemMapper _todoItemMapper;

        public TodoItemController(
            TodoItemService todoItemService,
            TodoItemMapper todoItemMapper
        ) {
			_todoItemService = todoItemService;
            _todoItemMapper = todoItemMapper;
        }

		[HttpGet]
		public async Task<ActionResult<IEnumerable<TodoItemQuery>>> GetAll()
		{
			var todoItems = await _todoItemService.GetAll();

			return Ok(todoItems);
		}

		[HttpGet("{id:int}")]
		public Task<ActionResult<TodoItemDTO.Get.Response>> GetById(int id)
		{
            return HandleRequest(
                () => _todoItemService.GetById(id),
                _todoItemMapper.ToGetResponse
            );
		}

        [HttpPost]
        public Task<ActionResult<TodoItemDTO.Create.Response>> Create(TodoItemDTO.Create.Command command)
        {
            return HandleRequest(
                command,
                _todoItemMapper.ToEntity,
                entity => _todoItemService.Create(entity) as Task<TodoItem?>,
                _todoItemMapper.ToCreateResponse
            );
        }

        [HttpPut("{id:int}")]
        public Task<ActionResult<TodoItemDTO.Update.Response>> Update(int id, TodoItemDTO.Update.Command command)
        {
            return HandleRequest(
                command,
                _todoItemMapper.ToEntity,
                entity => _todoItemService.Update(id, entity) as Task<TodoItem?>,
                _todoItemMapper.ToUpdateResponse
            );
        }

        [HttpPut("{id:int}/title")]
        public Task<ActionResult<TodoItemDTO.UpdateTitle.Response>> UpdateTitle(int id, TodoItemDTO.UpdateTitle.Command command)
        {
            return HandleRequest(
                command,
                _todoItemMapper.ToTitle,
                title => _todoItemService.UpdateTitle(id, title) as Task<string?>,
                _todoItemMapper.ToUpdateTitleResponse
            );
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
