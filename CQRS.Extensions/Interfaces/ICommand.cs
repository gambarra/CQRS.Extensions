using MediatR;

namespace CQRS.Extensions.Interfaces
{
    public interface ICommand : IRequest<CommandResult>
    {
    }
}
