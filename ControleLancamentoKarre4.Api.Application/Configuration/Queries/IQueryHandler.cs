using ControleLancamento.Api.Application.Configuration.Events;
using MediatR;

namespace ControleLancamento.Api.Application.Configuration.Queries
{
    public interface IQueryHandler<in TQuery> : IRequestHandler<TQuery, IEvent> where TQuery : IQuery
    {
    }
}