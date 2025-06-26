using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Server.Dtos;
using TodoList.Server.Queries;
using TodoList.Server.Services;

namespace TodoList.Server.Controllers
{
    [ApiController]
	[Route("todolist")]
	public class TodoGroupController : CustomControllerBase
    {
		private readonly TodoGroupService _todoGroupService;

		public TodoGroupController(TodoGroupService todoGroupService)
		{
			_todoGroupService = todoGroupService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<TodoGroupQuery>>> GetAll()
		{
			var todoItems = await _todoGroupService.GetAll();

			return Ok(todoItems);
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<TodoGroupQuery>> GetById(int id)
		{
			var todoItem = await _todoGroupService.GetById(id);

			if (todoItem == null)
			{
				return StatusCode(400);
            }

			return Ok(todoItem);
		}

		[HttpPost]
		public Task<ActionResult<int>> Create(TodoGroupDTO.Create.Command command)
        {
            return HandleRequest(
                command,
                _mapper.ToEntity,
                entity => _todoGroupService.Create(entity),
                _mapper.ToResponse
            );
        }

        [HttpPut("{id:int}")]
        public Task<ActionResult<TodoGroupDTO.Update>> Update(int id, TodoGroupDTO.Update.Command command)
        {
            return HandleRequest(
                command,
                _mapper.ToEntity,
                entity => _todoGroupService.Update(id, entity),
                _mapper.ToResponse
            );
        }

        [HttpPut("{id:int}/title")]
        public async Task<ActionResult<string>> UpdateTitle(int id, TodoGroupDTO.UpdateTitle.Command command)
        {
            var entity = _mapper.ToEntity(command);

            try
            {
                entity = await _todoGroupService.UpdateTitle(id, entity);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is DbUpdateException)
            {
                return StatusCode(400);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

            var response = _mapper.ToResponse(entity);

            return Ok(response);
        }

        [HttpDelete("{id:int}")]
		public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _todoGroupService.Delete(id);
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
