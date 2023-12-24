using AutoMapper;
using ControleLancamento.Api.Application.AppLancamento;
using ControleLancamento.Api.Domain.Entities;
using ControleLancamento.Api.Domain.Repositories;
using Moq;
using Xunit;

namespace ControleLancamento.Api.XUnitTest.Back.Test.src.ControleLancamento.Api.Application.Test.PutLancamentoTest
{
    public class PutLancamentoHandlerTest
    {
        private readonly PutLancamentoCommand _invalidCommand = new PutLancamentoCommand()
        {
            Id = Guid.Parse("08599daa-d95a-45da-b798-ee005e3ecfe8"),
            Data = DateTime.Now,
            TipoOperacao = Domain.Enum.EnTipoOperacao.Debito,
            Valor = 20
        };

        private readonly PutLancamentoCommand _validCommand = new PutLancamentoCommand()
        {
            Id = Guid.Parse("08599daa-d95a-45da-b798-ee005e3ecfe8"),
            Data = DateTime.Now,
            TipoOperacao = Domain.Enum.EnTipoOperacao.Credito,
            Valor = 100
        };

        [Fact]
        public async Task PutLancamentoHandler_valido()
        {
            var mockContextRepository = new Mock<ILancamentoRepository>();
            var _mockMapper = new Mock<IMapper>();

            var _lancamentos = new List<LancamentoEntity>();

            var _lancamento = new LancamentoEntity()
            {
                DataCriacao = DateTime.Now,
                Id = Guid.Parse("08599daa-d95a-45da-b798-ee005e3ecfe8"),
                SaldoFinal = 20,
                SaldoInicial = 10,
                TipoOperacao = Domain.Enum.EnTipoOperacao.Debito,
                Valor = 10
            };

            mockContextRepository.Setup(x => x.FindAll()).ReturnsAsync(_lancamentos);
            mockContextRepository.Setup(x => x.Update(It.IsAny<LancamentoEntity>())).ReturnsAsync(true);

            PutLancamentoHandler _handler = new PutLancamentoHandler(mockContextRepository.Object);

            var _return = await _handler.Handle(_validCommand, CancellationToken.None);
            var _data = _return.Data;

            Assert.True(_return.Success);
            Assert.Equal(true, (bool)_data);
        }

        [Fact]
        public async Task PutLancamentoHandler_invalido_ErroUpdate()
        {
            var mockContextRepository = new Mock<ILancamentoRepository>();
            var _mockMapper = new Mock<IMapper>();

            var _lancamentos = new List<LancamentoEntity>();

            var _lancamento = new LancamentoEntity()
            {
                DataCriacao = DateTime.Now,
                Id = Guid.Parse("08599daa-d95a-45da-b798-ee005e3ecfe8"),
                SaldoFinal = 20,
                SaldoInicial = 10,
                TipoOperacao = Domain.Enum.EnTipoOperacao.Debito,
                Valor = 10
            };

            mockContextRepository.Setup(x => x.FindAll()).ReturnsAsync(_lancamentos);
            mockContextRepository.Setup(x => x.Update(It.IsAny<LancamentoEntity>())).ReturnsAsync(false);

            PutLancamentoHandler _handler = new PutLancamentoHandler(mockContextRepository.Object);

            var _return = await _handler.Handle(_validCommand, CancellationToken.None);
            var _data = _return.Data;

            Assert.True(_return.Success);
            Assert.Null(_data);
        }

        [Fact]
        public async Task PutLancamentoHandler_invalido_ErroSaldo()
        {
            var mockContextRepository = new Mock<ILancamentoRepository>();
            var _mockMapper = new Mock<IMapper>();

            var _lancamentos = new List<LancamentoEntity>();

            var _lancamento = new LancamentoEntity()
            {
                DataCriacao = DateTime.Now,
                Id = Guid.Parse("08599daa-d95a-45da-b798-ee005e3ecfe8"),
                SaldoFinal = 20,
                SaldoInicial = 10,
                TipoOperacao = Domain.Enum.EnTipoOperacao.Debito,
                Valor = 10
            };

            mockContextRepository.Setup(x => x.FindAll()).ReturnsAsync(_lancamentos);
            mockContextRepository.Setup(x => x.Update(It.IsAny<LancamentoEntity>())).ReturnsAsync(false);

            PutLancamentoHandler _handler = new PutLancamentoHandler(mockContextRepository.Object);

            var _return = await _handler.Handle(_invalidCommand, CancellationToken.None);
            var _data = _return.Data;

            Assert.False(_return.Success);
            Assert.Equal("Saldo insuficiente.", _data);
        }
    }
}