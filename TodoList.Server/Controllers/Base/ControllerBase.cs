using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Server.Dtos;

namespace TodoList.Server.Controllers
{
    public abstract class CustomControllerBase : ControllerBase
    {
        protected async Task<ActionResult<TResponse>> HandleRequest<TCommand, TModel, TResponse>(
            TCommand command,
            Func<TCommand, TModel> mapCommand,
            Func<Task<TModel>> serviceCall,
            Func<TModel, TResponse> mapResponse
        )
            where TResponse : ResponseBase
        {
            try
            {
                var entity = mapCommand(command);
                entity = await serviceCall();
                
                var response = mapResponse(entity);
                response.IsSuccess = true;

                return Ok(response);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is DbUpdateException)
            {
                return StatusCode(400);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
