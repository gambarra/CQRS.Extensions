using MediatR;

namespace CQRS.Extensions.Interfaces
{
    public interface  IQuery<TResponse> : IRequest<TResponse>
    {
    }
}
