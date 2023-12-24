using ControleLancamento.Api.Application.Configuration.Events;
using ControleLancamento.Api.Application.Configuration.Queries;
using ControleLancamento.Api.Domain.Repositories;

namespace ControleLancamento.Api.Application.AppLancamento
{
    public class GetAllLancamentoHandler : IQueryHandler<GetAllLancamentoQuery>
    {
        private readonly ILancamentoRepository _repoLancamento;

        public GetAllLancamentoHandler(ILancamentoRepository repoLancamento)
        {
            _repoLancamento = repoLancamento;
        }

        public async Task<IEvent> Handle(GetAllLancamentoQuery request, CancellationToken cancellationToken)
        {
            var result = (await _repoLancamento.FindAll()).OrderByDescending(x => x.DataCriacao);

            if (result.Any())
            {
                return new ResultEvent(true, result);
            }
            else
            {
                return new ResultEvent(true, null);
            }
        }
    }
}