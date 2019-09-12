using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Extensions.Interfaces
{
    public interface  IQuery<TResponse> : IRequest<TResponse>
    {
    }
}
