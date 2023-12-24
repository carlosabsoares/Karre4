﻿using ControleLancamento.Api.Application.Configuration.Commands;
using ControleLancamento.Api.Application.Configuration.Events;
using ControleLancamento.Api.Domain.Entities;
using ControleLancamento.Api.Domain.Enum;
using ControleLancamento.Api.Domain.Repositories;
using Flunt.Notifications;

namespace ControleLancamento.Api.Application.AppLancamento
{
    public class PostLancamentoHandler : Notifiable, ICommandHandler<PostLancamentoCommand>
    {
        private ILancamentoRepository _repoLancamento;

        public PostLancamentoHandler(ILancamentoRepository repoLancamento)
        {
            _repoLancamento = repoLancamento;
        }

        public async Task<IEvent> Handle(PostLancamentoCommand request, CancellationToken cancellationToken)
        {
            request.Validate();
            if (request.Invalid)
                return new ResultEvent(false, request.Notifications);

            decimal valorSaldoFinal = 0;
            decimal _saldoAnterior = 0;

            var _ultimoResgistro = await _repoLancamento.FindAll();

            if (_ultimoResgistro.Any())
            {
                _saldoAnterior = _ultimoResgistro.OrderByDescending(x => x.DataCriacao).FirstOrDefault().SaldoFinal;
            }

            if (EnTipoOperacao.Debito.Equals(request.TipoOperacao) &&
                _saldoAnterior < request.Valor)
            {
                return new ResultEvent(false, "Saldo insuficiente.");
            }

            valorSaldoFinal = EnTipoOperacao.Debito.Equals(request.TipoOperacao) ?
                                                    _saldoAnterior + (request.Valor * -1) :
                                                    _saldoAnterior + request.Valor;

            var _lancamento = new LancamentoEntity()
            {
                TipoOperacao = request.TipoOperacao,
                Valor = request.Valor,
                SaldoInicial = _saldoAnterior,
                SaldoFinal = valorSaldoFinal
            };

            var result = await _repoLancamento.Add(_lancamento);

            return new ResultEvent(true, result ? result : null);
        }
    }
}