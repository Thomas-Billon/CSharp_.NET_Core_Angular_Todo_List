using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Server.Dtos;

namespace TodoList.Server.Controllers
{
    public abstract class CustomControllerBase : ControllerBase
    {
        protected Task<ActionResult<ResponseBase>> HandleRequest(
            Func<Task> serviceCall
        ) {
            return HandleRequestInternal<object, object, ResponseBase>(
                command: null,
                mapCommand: null,
                serviceCall,
                null,
                null,
                null,
                mapResponse: null
            );
        }

        protected Task<ActionResult<ResponseBase>> HandleRequest<TCommand, TEntity>(
            TCommand command,
            Func<TCommand, TEntity> mapCommand,
            Func<TEntity, Task> serviceCall
        ) {
            return HandleRequestInternal<TCommand, TEntity, ResponseBase>(
                command,
                mapCommand,
                null,
                serviceCall,
                null,
                null,
                mapResponse: null
            );
        }

        protected Task<ActionResult<TResponse>> HandleRequest<TEntity, TResponse>(
            Func<Task<TEntity?>> serviceCall,
            Func<TEntity, TResponse> mapResponse
        )
            where TResponse : ResponseBase
        {
            return HandleRequestInternal<object, TEntity, TResponse>(
                command: null,
                mapCommand: null,
                null,
                null,
                serviceCall,
                null,
                mapResponse
            );
        }

        protected Task<ActionResult<TResponse>> HandleRequest<TCommand, TEntity, TResponse>(
            TCommand command,
            Func<TCommand, TEntity> mapCommand,
            Func<TEntity, Task<TEntity?>> serviceCall,
            Func<TEntity, TResponse> mapResponse
        )
            where TResponse : ResponseBase
        {
            return HandleRequestInternal(
                command,
                mapCommand,
                null,
                null,
                null,
                serviceCall,
                mapResponse
            );
        }

        private async Task<ActionResult<TResponse>> HandleRequestInternal<TCommand, TEntity, TResponse>(
            TCommand? command,
            Func<TCommand, TEntity>? mapCommand,
            Func<Task>? serviceCallWithoutCommandAndWithoutResponse,
            Func<TEntity, Task>? serviceCallWithCommandAndWithoutResponse,
            Func<Task<TEntity?>>? serviceCallWithoutCommandAndWithResponse,
            Func<TEntity, Task<TEntity?>>? serviceCallWithCommandAndWithResponse,
            Func<TEntity, TResponse>? mapResponse
        )
            where TResponse : ResponseBase
        {
            if (serviceCallWithoutCommandAndWithoutResponse == null &&
                serviceCallWithCommandAndWithoutResponse == null &&
                serviceCallWithoutCommandAndWithResponse == null &&
                serviceCallWithCommandAndWithResponse == null)
            {
                return StatusCode(500);
            }
            if (!ModelState.IsValid)
            {
                return StatusCode(400);
            }

            try
            {
                var entity = default(TEntity);

                if (command != null && mapCommand != null)
                {
                    if (serviceCallWithCommandAndWithoutResponse != null)
                    {
                        await serviceCallWithCommandAndWithoutResponse(mapCommand(command));
                    }
                    if (serviceCallWithCommandAndWithResponse != null)
                    {
                        entity = await serviceCallWithCommandAndWithResponse(mapCommand(command));
                    }
                }
                if (serviceCallWithoutCommandAndWithoutResponse != null)
                {
                    await serviceCallWithoutCommandAndWithoutResponse();
                }
                if (serviceCallWithoutCommandAndWithResponse != null)
                {
                    entity = await serviceCallWithoutCommandAndWithResponse();
                }

                if (mapResponse == null)
                {
                    return Ok(new ResponseBase { IsSuccess = true });
                }
                if (entity == null)
                {
                    return StatusCode(400);
                }

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
