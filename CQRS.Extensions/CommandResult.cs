using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Extensions
{
    public class CommandResult
    {
        private CommandResult()
        {
            this.IsSuccess = true;
            this.Errors = new List<string>();
        }

        private CommandResult(string error)
        {
            this.AddError(error);
        }

        private CommandResult(List<string> errors)
        {
            this.Errors = errors;
            this.IsSuccess = false;
        }

        public object Value { get; private set; }
        public bool IsSuccess { get; private set; }
        public IList<string> Errors { get; private set; }
        private void AddError(string error)
        {
            if (this.Errors == null)
                this.Errors = new List<string>();
            this.Errors.Add(error);
            this.IsSuccess = false;
        }

        public static CommandResult Success(object value) => new CommandResult() { Value = value };
        public static CommandResult Fail(string error) => new CommandResult(error);
        public static CommandResult Fail(List<string> erros) => new CommandResult(erros);
      
    }
}
