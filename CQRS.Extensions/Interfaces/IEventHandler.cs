using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Extensions.Interfaces
{
    public interface IEventHandler<in T> : INotificationHandler<T> where T : IEvent
    {
    }
}
