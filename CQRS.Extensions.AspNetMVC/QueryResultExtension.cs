using Automapper.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Extensions.AspNetMVC
{
    public static class QueryResultExtension
    {
        public static ObjectResult AsOKResult(this QueryResult queryResult)
        {
            if (queryResult.IsSuccess)
                return new ObjectResult(queryResult.Value) { StatusCode = StatusCodes.Status200OK };

            return new ObjectResult(queryResult.Errors) { StatusCode = queryResult.StatusCode ?? StatusCodes.Status400BadRequest };
        }

        public static ObjectResult AsOKResultWithProjection<TResult>(this QueryResult queryResult) where TResult : class
        {
            if (queryResult.IsSuccess)
                return new ObjectResult(queryResult.Value.As<TResult>()) { StatusCode = StatusCodes.Status200OK };

            return new ObjectResult(queryResult.Errors) { StatusCode = queryResult.StatusCode ?? StatusCodes.Status400BadRequest };
        }
    }
}