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
		public Task<ActionResult<TodoItemDTO.GetAll.Response>> GetAll()
        {
            return HandleRequest(
                async () => await _todoItemService.GetAll(),
                _todoItemMapper.ToGetAllResponse
            );
		}

		[HttpGet("{id:int}")]
		public Task<ActionResult<TodoItemDTO.Get.Response>> GetById(int id)
		{
            return HandleRequest(
                async () => await _todoItemService.GetById(id),
                _todoItemMapper.ToGetResponse
            );
		}

        [HttpPost]
        public Task<ActionResult<TodoItemDTO.Create.Response>> Create(TodoItemDTO.Create.Command command)
        {
            return HandleRequest(
                command,
                _todoItemMapper.ToEntity,
                async entity => await _todoItemService.Create(entity),
                _todoItemMapper.ToCreateResponse
            );
        }

        [HttpPut("{id:int}")]
        public Task<ActionResult<TodoItemDTO.Update.Response>> Update(int id, TodoItemDTO.Update.Command command)
        {
            return HandleRequest(
                command,
                _todoItemMapper.ToEntity,
                async entity => await _todoItemService.Update(id, entity),
                _todoItemMapper.ToUpdateResponse
            );
        }

        [HttpPut("{id:int}/title")]
        public Task<ActionResult<TodoItemDTO.UpdateTitle.Response>> UpdateTitle(int id, TodoItemDTO.UpdateTitle.Command command)
        {
            return HandleRequest(
                command,
                _todoItemMapper.ToTitle,
                async title => await _todoItemService.UpdateTitle(id, title),
                _todoItemMapper.ToUpdateTitleResponse
            );
        }

        [HttpPut("{id:int}/iscompleted")]
        public Task<ActionResult<TodoItemDTO.UpdateIsCompleted.Response>> UpdateIsCompleted(int id, TodoItemDTO.UpdateIsCompleted.Command command)
        {
            return HandleRequest(
                command,
                _todoItemMapper.ToIsCompleted,
                async isCompleted => await _todoItemService.UpdateIsCompleted(id, isCompleted),
                _todoItemMapper.ToUpdateIsCompletedResponse
            );
        }

        [HttpDelete("{id:int}")]
		public Task<ActionResult<ResponseBase>> Delete(int id)
        {
            return HandleRequest(
                async () => await _todoItemService.Delete(id)
            );
        }
    }
}
