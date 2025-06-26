using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Server.Dtos;

namespace TodoList.Server.Controllers
{
    public abstract class CustomControllerBase : ControllerBase
    {
        private abstract record ServiceCallVariant;
        private record NoCommandNoResponse(Func<Task> Call) : ServiceCallVariant;
        private record CommandNoResponse<TEntity>(Func<TEntity, Task> Call) : ServiceCallVariant;
        private record NoCommandWithResponse<TEntity>(Func<Task<TEntity?>> Call) : ServiceCallVariant;
        private record CommandWithResponse<TEntity>(Func<TEntity, Task<TEntity?>> Call) : ServiceCallVariant;

        protected Task<ActionResult<ResponseBase>> HandleRequest(
            Func<Task> serviceCall
        ) {
            return HandleRequestInternal<object, object, ResponseBase>(
                command: null,
                mapCommand: null,
                serviceCall: new NoCommandNoResponse(serviceCall),
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
                serviceCall: new CommandNoResponse<TEntity>(serviceCall),
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
                serviceCall: new NoCommandWithResponse<TEntity>(serviceCall),
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
                serviceCall: new CommandWithResponse<TEntity>(serviceCall),
                mapResponse
            );
        }

        private async Task<ActionResult<TResponse>> HandleRequestInternal<TCommand, TEntity, TResponse>(
            TCommand? command,
            Func<TCommand, TEntity>? mapCommand,
            ServiceCallVariant serviceCall,
            Func<TEntity, TResponse>? mapResponse
        )
            where TResponse : ResponseBase
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400);
            }

            try
            {
                var entityPreCall = HandleCommand(command, mapCommand);
                var entityPostCall = await CallServiceMethod(entityPreCall, serviceCall);
                return HandleResponse(entityPostCall, mapResponse);
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

        private TEntity? HandleCommand<TCommand, TEntity>(TCommand? command, Func<TCommand, TEntity>? mapCommand) {
            if (command != null && mapCommand != null)
            {
                return mapCommand(command);
            }

            return default;
        }

        private async Task<TEntity?> CallServiceMethod<TEntity>(TEntity? entity, ServiceCallVariant serviceCall) {
            switch (serviceCall)
            {
                case NoCommandNoResponse(var call):
                    await call();
                    break;

                case CommandNoResponse<TEntity>(var call):
                    if (entity != null)
                    {
                        await call(entity);
                    }
                    break;

                case NoCommandWithResponse<TEntity>(var call):
                    return await call();

                case CommandWithResponse<TEntity>(var call):
                    if (entity != null)
                    {
                        return await call(entity);
                    }
                    break;
            }

            return default;
        }

        private ActionResult<TResponse> HandleResponse<TEntity, TResponse>(TEntity? entity, Func<TEntity, TResponse>? mapResponse)
            where TResponse : ResponseBase
        {
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
    }
}
