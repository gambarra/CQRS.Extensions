using MediatR;

namespace CQRS.Extensions.Interfaces
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand, IRequestHandler<TCommand, CommandResult>
    {
    }
}
