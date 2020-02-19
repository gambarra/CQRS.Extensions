using MediatR;

namespace CQRS.Extensions.Interfaces
{
    public interface ICommand<TResponse> : IRequest<Result<TResponse>>
        where TResponse : class
    {
    }

    public interface ICommand : IRequest<Result<object>>
    {
    }
}
