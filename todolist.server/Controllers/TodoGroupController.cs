using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Server.Dtos;
using TodoList.Server.Mappers;
using TodoList.Server.Queries;
using TodoList.Server.Services;

namespace TodoList.Server.Controllers
{
    [ApiController]
	[Route("todolist")]
	public class TodoGroupController : CustomControllerBase
    {
		private readonly TodoGroupService _todoGroupService;
        private readonly TodoGroupMapper _todoGroupMapper;

        public TodoGroupController(
            TodoGroupService todoGroupService,
            TodoGroupMapper todoGroupMapper
        ) {
			_todoGroupService = todoGroupService;
            _todoGroupMapper = todoGroupMapper;
        }

        [HttpGet]
        public Task<ActionResult<TodoGroupDTO.GetAll.Response>> GetAll()
        {
            return HandleRequest(
                async () => await _todoGroupService.GetAll(),
                _todoGroupMapper.ToGetAllResponse
            );
        }

        [HttpGet("{id:int}")]
        public Task<ActionResult<TodoGroupDTO.Get.Response>> GetById(int id)
        {
            return HandleRequest(
                async () => await _todoGroupService.GetById(id),
                _todoGroupMapper.ToGetResponse
            );
        }

        [HttpPost]
        public Task<ActionResult<TodoGroupDTO.Create.Response>> Create(TodoGroupDTO.Create.Command command)
        {
            return HandleRequest(
                command,
                _todoGroupMapper.ToEntity,
                async entity => await _todoGroupService.Create(entity),
                _todoGroupMapper.ToCreateResponse
            );
        }

        [HttpPut("{id:int}")]
        public Task<ActionResult<TodoGroupDTO.Update.Response>> Update(int id, TodoGroupDTO.Update.Command command)
        {
            return HandleRequest(
                command,
                _todoGroupMapper.ToEntity,
                async entity => await _todoGroupService.Update(id, entity),
                _todoGroupMapper.ToUpdateResponse
            );
        }

        [HttpPut("{id:int}/title")]
        public Task<ActionResult<TodoGroupDTO.UpdateTitle.Response>> UpdateTitle(int id, TodoGroupDTO.UpdateTitle.Command command)
        {
            return HandleRequest(
                command,
                _todoGroupMapper.ToTitle,
                async title => await _todoGroupService.UpdateTitle(id, title),
                _todoGroupMapper.ToUpdateTitleResponse
            );
        }

        [HttpDelete("{id:int}")]
        public Task<ActionResult<ResponseBase>> Delete(int id)
        {
            return HandleRequest(
                async () => await _todoGroupService.Delete(id)
            );
        }
    }
}
