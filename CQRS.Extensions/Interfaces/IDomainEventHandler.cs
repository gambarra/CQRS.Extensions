using MediatR;

namespace CQRS.Extensions.Interfaces
{
    public interface IDomainEventHandler<in T> : INotificationHandler<T> where T : IDomainEvent
    {
    }
}
