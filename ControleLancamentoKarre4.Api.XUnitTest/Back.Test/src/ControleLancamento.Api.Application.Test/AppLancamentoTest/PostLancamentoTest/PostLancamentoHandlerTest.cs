using AutoMapper;
using ControleLancamento.Api.Application.AppLancamento;
using ControleLancamento.Api.Domain.Entities;
using ControleLancamento.Api.Domain.Repositories;
using Moq;
using Xunit;

namespace ControleLancamento.Api.XUnitTest.Back.Test.src.ControleLancamento.Api.Application
{
    public class PostLancamentoHandlerTest
    {
        private readonly PostLancamentoCommand _invalidCommand = new PostLancamentoCommand()
        {
            TipoOperacao = Domain.Enum.EnTipoOperacao.Debito,
            Valor = 20
        };

        private readonly PostLancamentoCommand _validCommand = new PostLancamentoCommand()
        {
            TipoOperacao = Domain.Enum.EnTipoOperacao.Credito,
            Valor = 20
        };

        [Fact]
        public async Task PostLancamentoHandler_invalido_saldo()
        {
            var mockContextRepository = new Mock<ILancamentoRepository>();
            var _mockMapper = new Mock<IMapper>();

            var _lancamentos = new List<LancamentoEntity>();

            var _lancamento = new LancamentoEntity()
            {
                DataCriacao = DateTime.Now,
                Id = Guid.NewGuid(),
                SaldoFinal = 20,
                SaldoInicial = 10,
                TipoOperacao = Domain.Enum.EnTipoOperacao.Debito,
                Valor = 10
            };

            mockContextRepository.Setup(x => x.FindAll()).ReturnsAsync(_lancamentos);
            mockContextRepository.Setup(x => x.Add(It.IsAny<LancamentoEntity>())).ReturnsAsync(true);

            PostLancamentoHandler _handler = new PostLancamentoHandler(mockContextRepository.Object);

            var _return = await _handler.Handle(_invalidCommand, CancellationToken.None);
            var _data = _return.Data;

            Assert.False(_return.Success);
            Assert.Equal("Saldo insuficiente.", _data);
        }

        [Fact]
        public async Task PostLancamentoHandler_invalido_erro_insert()
        {
            var mockContextRepository = new Mock<ILancamentoRepository>();
            var _mockMapper = new Mock<IMapper>();

            var _lancamentos = new List<LancamentoEntity>();

            var _lancamento = new LancamentoEntity()
            {
                DataCriacao = DateTime.Now,
                Id = Guid.NewGuid(),
                SaldoFinal = 20,
                SaldoInicial = 10,
                TipoOperacao = Domain.Enum.EnTipoOperacao.Debito,
                Valor = 10
            };

            mockContextRepository.Setup(x => x.FindAll()).ReturnsAsync(_lancamentos);
            mockContextRepository.Setup(x => x.Add(It.IsAny<LancamentoEntity>())).ReturnsAsync(false);

            PostLancamentoHandler _handler = new PostLancamentoHandler(mockContextRepository.Object);

            var _return = await _handler.Handle(_validCommand, CancellationToken.None);
            var _data = _return.Data;

            Assert.True(_return.Success);
            Assert.Null(_data);
        }

        [Fact]
        public async Task PostLancamentoHandler_valido_saldo_primeiro_lancamento()
        {
            var mockContextRepository = new Mock<ILancamentoRepository>();
            var _mockMapper = new Mock<IMapper>();

            var _lancamentos = new List<LancamentoEntity>();

            var _lancamento = new LancamentoEntity()
            {
                DataCriacao = DateTime.Now,
                Id = Guid.NewGuid(),
                SaldoFinal = 20,
                SaldoInicial = 10,
                TipoOperacao = Domain.Enum.EnTipoOperacao.Debito,
                Valor = 10
            };

            mockContextRepository.Setup(x => x.FindAll()).ReturnsAsync(_lancamentos);
            mockContextRepository.Setup(x => x.Add(It.IsAny<LancamentoEntity>())).ReturnsAsync(true);

            PostLancamentoHandler _handler = new PostLancamentoHandler(mockContextRepository.Object);

            var _return = await _handler.Handle(_validCommand, CancellationToken.None);
            var _data = _return.Data;

            Assert.True(_return.Success);
            Assert.True((bool)_data);
        }

        [Fact]
        public async Task PostLancamentoHandler_valido_saldo_sem_ser_primeiro_lancamento()
        {
            var mockContextRepository = new Mock<ILancamentoRepository>();
            var _mockMapper = new Mock<IMapper>();

            var _lancamentos = new List<LancamentoEntity>();

            var _lancamento = new LancamentoEntity()
            {
                DataCriacao = DateTime.Now,
                Id = Guid.NewGuid(),
                SaldoFinal = 20,
                SaldoInicial = 10,
                TipoOperacao = Domain.Enum.EnTipoOperacao.Debito,
                Valor = 10
            };

            _lancamentos.Add(_lancamento);

            mockContextRepository.Setup(x => x.FindAll()).ReturnsAsync(_lancamentos);
            mockContextRepository.Setup(x => x.Add(It.IsAny<LancamentoEntity>())).ReturnsAsync(true);

            PostLancamentoHandler _handler = new PostLancamentoHandler(mockContextRepository.Object);

            var _return = await _handler.Handle(_validCommand, CancellationToken.None);
            var _data = _return.Data;

            Assert.True(_return.Success);
            Assert.True((bool)_data);
        }
    }
}