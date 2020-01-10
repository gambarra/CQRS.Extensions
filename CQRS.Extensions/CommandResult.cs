using System.Collections.Generic;

namespace CQRS.Extensions
{
    public class CommandResult
    {
        public object Value { get; private set; }
        public bool IsSuccess { get; private set; }
        public object Errors { get; private set; }
        public int? StatusCode { get; private set; }

        private CommandResult()
        {
            this.IsSuccess = true;
        }

        private CommandResult(object errors, int? statusCode = null)
        {
            this.Errors = errors;
            this.IsSuccess = false;
            this.StatusCode = statusCode;
        }


        public static CommandResult Success(object value) => new CommandResult() { Value = value };
        public static CommandResult Fail(object errors, int? statusCode = null) => new CommandResult(errors, statusCode);
      
    }
}
