using MediatR;

namespace CQRS.Extensions.Interfaces
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
        where TResponse : class
    {
    }

    public interface IQuery : IRequest<Result<object>>
    {
    }
}
