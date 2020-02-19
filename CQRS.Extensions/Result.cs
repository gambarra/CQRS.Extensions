using WebApi.Models.Response;

namespace CQRS.Extensions
{
    public class Result<TSuccess> : IResult where TSuccess : class
    {
        public TSuccess Value { get; private set; }

        public bool IsSuccess { get; private set; }

        public ErrorsResponse Errors { get; private set; }

        public int? StatusCode { get; private set; }

        public Result()
        {
            this.IsSuccess = true;
            this.StatusCode = 200;
        }

        public Result(TSuccess value, int? statusCode = 200)
        {
            this.IsSuccess = true;
            this.StatusCode = statusCode;
            this.Value = value;
        }

        public Result(ErrorItemResponse error, int? statusCode = 400)
        {
            this.Errors = new ErrorsResponse();
            this.Errors.AddError(error);
            this.IsSuccess = false;
            this.StatusCode = statusCode;
        }

        public Result(ErrorsResponse errors, int? statusCode = 400)
        {
            this.Errors = errors;
            this.IsSuccess = false;
            this.StatusCode = statusCode;
        }

        public Result(string errorMessage, int? statusCode = 400)
        {
            this.Errors = new ErrorsResponse();
            this.Errors.AddError(errorMessage);
            this.IsSuccess = false;
            this.StatusCode = statusCode;
        }

        public Result(string errorMessage, string property, int? statusCode = 400)
        {
            this.Errors = new ErrorsResponse();
            this.Errors.AddError(errorMessage, property);
            this.IsSuccess = false;
            this.StatusCode = statusCode;
        }

        public static implicit operator Result<TSuccess>(TSuccess value)
        {
            return new Result<TSuccess>(value);
        }

        public static implicit operator Result<TSuccess>(ErrorsResponse value)
        {
            return new Result<TSuccess>(value, 400);
        }

        public static implicit operator Result<TSuccess>(ErrorItemResponse value)
        {
            return new Result<TSuccess>(value, 400);
        }
    }

    public interface IResult
    { 
    }

    public static class Result
    {
        public static Result<TSuccess> Success<TSuccess>(TSuccess value = null, int? statusCode = 200) where TSuccess : class
            => new Result<TSuccess>(value, statusCode);

        public static Result<TSuccess> Fail<TSuccess>(string errorMessage, string property, int? statusCode = 400) where TSuccess : class
            => new Result<TSuccess>(errorMessage, property, statusCode);

        public static Result<TSuccess> Fail<TSuccess>(string errorMessage, int? statusCode = 400) where TSuccess : class
            => new Result<TSuccess>(errorMessage, statusCode);

        public static Result<TSuccess> Fail<TSuccess>(ErrorsResponse errors, int? statusCode = 400) where TSuccess : class
            => new Result<TSuccess>(errors, statusCode);

        public static Result<TSuccess> Fail<TSuccess>(ErrorItemResponse error, int? statusCode = 400) where TSuccess : class
            => new Result<TSuccess>(error, statusCode);

        public static Result<TSuccess> Fail<TSuccess>(int statusCode = 400) where TSuccess : class
        {
            ErrorsResponse error = null;
            return new Result<TSuccess>(error, statusCode);
        }

        public static Result<object> Fail(string errorMessage, string property, int? statusCode = 400)
            => new Result<object>(errorMessage, property, statusCode);

        public static Result<object> Fail(string errorMessage, int? statusCode = 400) 
            => new Result<object>(errorMessage, statusCode);

        public static Result<object> Fail(ErrorsResponse errors, int? statusCode = 400)
            => new Result<object>(errors, statusCode);

        public static Result<object> Fail(ErrorItemResponse error, int? statusCode = 400)
            => new Result<object>(error, statusCode);

        public static Result<object> Fail(int statusCode = 400)
        {
            ErrorsResponse error = null;
            return new Result<object>(error, statusCode);
        }
    }
}
