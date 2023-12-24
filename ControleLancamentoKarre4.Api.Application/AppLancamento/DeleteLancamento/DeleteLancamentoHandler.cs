using ControleLancamento.Api.Application.Configuration.Commands;
using ControleLancamento.Api.Application.Configuration.Events;
using ControleLancamento.Api.Domain.Entities;
using ControleLancamento.Api.Domain.Enum;
using ControleLancamento.Api.Domain.Repositories;
using Flunt.Notifications;

namespace ControleLancamento.Api.Application.AppLancamento
{
    public class DeleteLancamentoHandler : Notifiable, ICommandHandler<DeleteLancamentoCommand>
    {
        private readonly ILancamentoRepository _repoLancamento;

        public DeleteLancamentoHandler(ILancamentoRepository repoLancamento)
        {
            _repoLancamento = repoLancamento;
        }

        public async Task<IEvent> Handle(DeleteLancamentoCommand request, CancellationToken cancellationToken)
        {
            request.Validate();
            if (request.Invalid)
                return new ResultEvent(false, request.Notifications);

            var _lancamentoRepo = (await _repoLancamento.FindAll()).OrderByDescending(x => x.DataCriacao).ToList();

            int? indexIdDelete = _lancamentoRepo.FindIndex(x => x.Id.Equals(request.Id));
            int indexIdAtualizar = 0;
            decimal valorSaldoFinal = 0;
            bool _result = false;

            LancamentoEntity registroDeletar = _lancamentoRepo.FirstOrDefault(x => x.Id.Equals(request.Id));

            if (registroDeletar == null)
                return new ResultEvent(false, "Registro não localizado.");

            if (indexIdDelete != null && indexIdDelete > 0)
            {
                indexIdAtualizar = indexIdDelete.Value - 1;
            }

            var registroAtualizar = _lancamentoRepo.ElementAt(indexIdAtualizar);

            valorSaldoFinal = EnTipoOperacao.Debito.Equals(registroDeletar.TipoOperacao) ?
                                        registroDeletar.SaldoInicial + (registroAtualizar.Valor * -1) :
                                        registroDeletar.SaldoInicial + registroAtualizar.Valor;

            registroAtualizar.SaldoInicial = registroDeletar.SaldoInicial;
            registroAtualizar.SaldoFinal = valorSaldoFinal;

            await _repoLancamento.BeginTransactionAsync();

            _result = await _repoLancamento.Delete(registroDeletar);

            if (indexIdDelete is not null and not 0)
            {
                _result = await _repoLancamento.Update(registroAtualizar);
            }

            await _repoLancamento.CommitTransactionAsync();

            return new ResultEvent(true, _result);
        }
    }
}