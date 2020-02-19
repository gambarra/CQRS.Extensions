using MediatR;

namespace CQRS.Extensions.Interfaces
{
    public interface IQueryHandler<TQuery> : IRequestHandler<TQuery, Result<object>>
       where TQuery : IQuery
    {
    }

    public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
        where TQuery : IQuery<TResponse>
        where TResponse : class
    {
    }
}
