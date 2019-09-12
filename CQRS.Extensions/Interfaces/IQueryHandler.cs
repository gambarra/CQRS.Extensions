using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Extensions.Interfaces
{
    public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>
    {
    }
}
