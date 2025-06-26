using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Server.Dtos;
using TodoList.Server.Entities;

namespace TodoList.Server.Controllers
{
    public abstract class CustomControllerBase : ControllerBase
    {
        protected Task<ActionResult<TResponse>> HandleRequest<TEntity, TResponse>(
            Func<Task<TEntity?>> serviceCall,
            Func<TEntity, TResponse> mapResponse
        )
            where TResponse : ResponseBase
        {
            var serviceCallWrapper = null as Func<Task<TEntity?>>;
            if (serviceCall != null)
            {
                serviceCallWrapper = () => serviceCall();
            }

            return HandleRequestInternal(
                serviceCallWrapper,
                mapResponse,
                isResponseRequired
            );
        }

        protected Task<ActionResult<TResponse>> HandleRequest<TCommand, TEntity, TResponse>(
            TCommand? command,
            Func<TCommand, TEntity>? mapCommand,
            Func<TEntity, Task<TEntity?>> serviceCall,
            Func<TEntity, TResponse>? mapResponse,
            bool isResponseRequired = true
        )
            where TResponse : ResponseBase
        {
            var serviceCallWrapper = null as Func<Task<TEntity?>>;
            if (serviceCall != null && mapCommand != null && command != null)
            {
                serviceCallWrapper = () => serviceCall(mapCommand(command));
            }

            return HandleRequestInternal(
                serviceCallWrapper,
                mapResponse,
                isResponseRequired
            );
        }

        private async Task<ActionResult<TResponse>> HandleRequestInternal<TEntity, TResponse>(
            Func<Task<TEntity?>>? serviceCall,
            Func<TEntity, TResponse>? mapResponse,
            bool isResponseRequired
        )
            where TResponse : ResponseBase
        {
            if (serviceCall == null)
            {
                return StatusCode(500);
            }
            if (!ModelState.IsValid)
            {
                return StatusCode(400);
            }

            try
            {
                return HandleResponse(await serviceCall(), mapResponse, isResponseRequired);
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

        private ActionResult<TResponse> HandleResponse<TEntity, TResponse>(
            TEntity? entity,
            Func<TEntity, TResponse>? mapResponse,
            bool isResponseRequired = true
        )
            where TResponse : ResponseBase
        {
            var response = default(TResponse);
            if (mapResponse != null && entity != null)
            {
                response = mapResponse(entity);
            }

            if (response == null)
            {
                if (isResponseRequired)
                {
                    return StatusCode(400);
                }
                else
                {
                    return Ok();
                }
            }
            else
            {
                response.IsSuccess = true;
                return Ok(response);
            }
        }
    }
}
