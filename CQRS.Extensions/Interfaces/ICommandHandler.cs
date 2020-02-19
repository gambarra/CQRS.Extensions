using MediatR;

namespace CQRS.Extensions.Interfaces
{
    public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result<object>> 
        where TCommand : ICommand
    {
    }

    public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>> 
        where TCommand : ICommand<TResponse>
        where TResponse : class
    {
    }
}
