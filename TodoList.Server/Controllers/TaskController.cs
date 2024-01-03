using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using TodoList.Server.Services;
using Task = TodoList.Server.Models.Task;

namespace TodoList.Server.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class TaskController : ControllerBase
	{
		private readonly ITaskService _todoService;

		public TaskController(ITaskService todoService)
		{
			_todoService = todoService;
		}

		[HttpGet("")]
		[Produces(typeof(IEnumerable<Task>))]
		public ActionResult<IEnumerable<Task>> GetAllTasks()
		{
			try
			{
				IEnumerable<Task> tasks = _todoService.GetAllTasks();

				return Ok(tasks);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("{id}")]
		[Produces(typeof(Task))]
		public ActionResult<Task> GetTaskById(int id)
		{
			try
			{
				Task task = _todoService.GetTaskById(id);

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
		[Produces(typeof(int))]
		public ActionResult<int> CreateTask(Task task)
		{
			try
			{
				int id = _todoService.CreateTask(task);

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
		[Produces(typeof(bool))]
		public ActionResult<bool> UpdateTask(Task task)
		{
			try
			{
				bool result = _todoService.UpdateTask(task);

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
		[Produces(typeof(bool))]
		public ActionResult<bool> DeleteTask(int id)
		{
			try
			{
				bool result = _todoService.DeleteTask(id);

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
