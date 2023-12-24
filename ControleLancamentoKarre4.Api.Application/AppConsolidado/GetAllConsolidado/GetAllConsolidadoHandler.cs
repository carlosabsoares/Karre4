using ControleLancamento.Api.Application.Configuration.Events;
using ControleLancamento.Api.Application.Configuration.Queries;
using ControleLancamento.Api.Domain.Entities;
using ControleLancamento.Api.Domain.Enum;
using ControleLancamento.Api.Domain.Repositories;

namespace ControleLancamento.Api.Application.AppConsolidado
{
    public class GetAllConsolidadoHandler : IQueryHandler<GetAllConsolidadoQuery>
    {
        private readonly ILancamentoRepository _repoLancamento;

        public GetAllConsolidadoHandler(ILancamentoRepository repoLancamento)
        {
            _repoLancamento = repoLancamento;
        }

        public async Task<IEvent> Handle(GetAllConsolidadoQuery request, CancellationToken cancellationToken)
        {
            var result = (await _repoLancamento.FindAll()).GroupBy(x => x.DataCriacao.Date);

            List<ConsolidadoDto> consolidados = [];

            foreach (var itemGroup in result)
            {
                ConsolidadoDto consolidado = new()
                {
                    Data = itemGroup.Key.Date,
                    SaldoInicial = itemGroup.OrderBy(x => x.DataCriacao).FirstOrDefault(x => x.DataCriacao.Date.Equals(itemGroup.Key.Date)).SaldoInicial,
                    SaldoFinal = itemGroup.OrderByDescending(x => x.DataCriacao).FirstOrDefault(x => x.DataCriacao.Date.Equals(itemGroup.Key.Date)).SaldoFinal,
                    TotalDebito = itemGroup.Where(x => x.DataCriacao.Date.Equals(itemGroup.Key.Date) && x.TipoOperacao.Equals(EnTipoOperacao.Debito)).Select(x => x.Valor).Sum(),
                    TotalCredito = itemGroup.Where(x => x.DataCriacao.Date.Equals(itemGroup.Key.Date) && x.TipoOperacao.Equals(EnTipoOperacao.Credito)).Select(x => x.Valor).Sum()
                };

                consolidados.Add(consolidado);
            }

            if (consolidados.Count != 0)
            {
                return new ResultEvent(true, consolidados.OrderByDescending(x => x.Data));
            }
            else
            {
                return new ResultEvent(true, null);
            }
        }
    }
}