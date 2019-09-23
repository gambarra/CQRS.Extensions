using Automapper.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace CQRS.Extensions.AspNetMVC
{
    public static class CommandResultExtension
    {
        public static ObjectResult AsCreatedResult(this CommandResult commandResult)
        {
            if (commandResult.IsSuccess)
                return new ObjectResult(commandResult.Value) { StatusCode = StatusCodes.Status201Created };

            return new ObjectResult(commandResult.Errors) { StatusCode = StatusCodes.Status412PreconditionFailed };
        }
        public static ObjectResult AsCreatedResultWithProjection<TResult>(this CommandResult commandResult) where TResult : class
        {
            if (commandResult.IsSuccess)
                return new ObjectResult(commandResult.Value.As<TResult>()) { StatusCode = StatusCodes.Status201Created };

            return new ObjectResult(commandResult.Errors) { StatusCode = StatusCodes.Status412PreconditionFailed };
        }
        public static ObjectResult AsOKResult(this CommandResult commandResult)
        {
            if (commandResult.IsSuccess)
                return new ObjectResult(commandResult.Value) { StatusCode = StatusCodes.Status200OK };

            return new ObjectResult(commandResult.Errors) { StatusCode = StatusCodes.Status412PreconditionFailed };
        }

        public static ObjectResult AsOKResultWithProjection<TResult>(this CommandResult commandResult) where TResult : class
        {
            if (commandResult.IsSuccess)
                return new ObjectResult(commandResult.Value.As<TResult>()) { StatusCode = StatusCodes.Status200OK };

            return new ObjectResult(commandResult.Errors) { StatusCode = StatusCodes.Status412PreconditionFailed };
        }
    }
}
