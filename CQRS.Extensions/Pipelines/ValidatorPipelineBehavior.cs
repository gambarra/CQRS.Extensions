using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Models.Response;

namespace CQRS.Extensions.Pipelines
{
    public class ValidatorPipelineBehavior<TRequest, TResponse> 
        : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> Validators;

        public ValidatorPipelineBehavior(IEnumerable<IValidator<TRequest>> validators) 
        {
            this.Validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, 
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next) 
        {
            var errors = this.Validators.Select(x => x.Validate(request))
                .SelectMany(x => x.Errors)
                .Where(x => x != null)
                .Select(x =>
                {
                    var property = x.PropertyName;

                    if (PropertyName.Resolver != null)
                    {
                        property = PropertyName.Resolver.Invoke(property);
                    }

                    return new ErrorItemResponse(x.ErrorMessage, property);
                }).ToList();

            if (errors.Any() == false)
            {
                return next();
            }

            if (typeof(IResult).IsAssignableFrom(typeof(TResponse)))
            {
                var errorsResponse = new ErrorsResponse { Errors = errors };
                var result = (TResponse) Activator.CreateInstance(typeof(TResponse), new object[] { errorsResponse, 400 });
                return Task.FromResult(result);
            }

            throw new Exception(string.Join(",", errors.Select(r => r.Message)));
        }
    }
}
