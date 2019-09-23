using CQRS.Extensions.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Extensions
{
    public abstract class AggregateRoot
    {
        private List<IDomainEvent> events;
        public IReadOnlyCollection<IDomainEvent> Events => events;

        protected void RaiseEvent(IDomainEvent @event) {
            events = events ?? new List<IDomainEvent>();
            events.Add(@event);
        }

        public void ClearEvents() {
            events?.Clear();
        }
    }
}
