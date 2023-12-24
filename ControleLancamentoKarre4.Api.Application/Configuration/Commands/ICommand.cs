using ControleLancamento.Api.Application.Configuration.Events;
using Flunt.Validations;
using MediatR;

namespace ControleLancamento.Api.Application.Configuration.Commands
{
    public interface ICommand : IRequest<IEvent>, IValidatable
    {
    }
}