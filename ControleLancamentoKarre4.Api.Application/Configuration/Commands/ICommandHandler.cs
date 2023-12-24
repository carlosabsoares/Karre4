using ControleLancamento.Api.Application.Configuration.Events;
using MediatR;

namespace ControleLancamento.Api.Application.Configuration.Commands
{
    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, IEvent> where TCommand : ICommand
    {
    }
}