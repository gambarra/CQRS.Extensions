using MediatR;

namespace CQRS.Extensions.Interfaces
{
    public interface ICommandHandler<TCommand> : IRequestHandler<TCommand,CommandResult> where TCommand : ICommand
    {
    }
}
