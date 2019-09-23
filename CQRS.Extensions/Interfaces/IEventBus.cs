using System.Collections.Generic;
using System.Threading.Tasks;

namespace CQRS.Extensions.Interfaces
{
    public interface IEventBus
    {
        void AddEvent(IEvent @event);
        void AddEvents(IReadOnlyCollection<IEvent> events);
        Task Publish();
    }
}
