using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace CQRS.Extensions.Pipelines
{
    public class ValidatorPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidatorPipelineBehavior(IEnumerable<IValidator<TRequest>> validators) {
            _validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next) {
            var messages = _validators.Select(x => x.Validate(request))
                .SelectMany(x => x.Errors)
                .Where(x => x != null)
                .Select(x => x.ErrorMessage)
                .ToList();

            if (messages.Any() == false) return next();

            if (typeof(CommandResult).IsAssignableFrom(typeof(TResponse))) {
                return Task.FromResult((TResponse)Convert.ChangeType(CommandResult.Fail(messages), typeof(TResponse)));
            }

            throw new Exception(string.Join(",", messages));
        }
    }
}
