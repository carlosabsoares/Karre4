using AutoMapper;
using ControleLancamento.Api.Application.AppLancamento;
using ControleLancamento.Api.Domain.Entities;
using ControleLancamento.Api.Domain.Repositories;
using Moq;
using Xunit;

namespace ControleLancamento.Api.XUnitTest.Back.Test.src.ControleLancamento.Api.Application.Test
{
    public class DeleteLancamentoHandlerTest
    {
        private readonly DeleteLancamentoCommand _invalidCommand = new DeleteLancamentoCommand() { Id = Guid.Parse("00000000-0000-0000-0000-000000000000") };
        private readonly DeleteLancamentoCommand _validCommand = new DeleteLancamentoCommand() { Id = Guid.Parse("dc697766-3251-4bbb-98e5-72b2a06047c4") };

        [Fact]
        public async Task DeleteLancamentoHandler_delete_valid()
        {
            var mockContextRepository = new Mock<ILancamentoRepository>();
            var _mockMapper = new Mock<IMapper>();

            List<LancamentoEntity> _lancamentos = new List<LancamentoEntity>();

            _lancamentos.Add(new LancamentoEntity()
            {
                DataCriacao = DateTime.Now,
                Id = Guid.NewGuid(),
                SaldoFinal = 20,
                SaldoInicial = 10,
                TipoOperacao = Domain.Enum.EnTipoOperacao.Debito,
                Valor = 10
            });

            _lancamentos.Add(new LancamentoEntity()
            {
                DataCriacao = DateTime.Now,
                Id = Guid.Parse("dc697766-3251-4bbb-98e5-72b2a06047c4"),
                SaldoFinal = 20,
                SaldoInicial = 10,
                TipoOperacao = Domain.Enum.EnTipoOperacao.Credito,
                Valor = 10
            });

            _lancamentos.Add(new LancamentoEntity()
            {
                DataCriacao = DateTime.Now,
                Id = Guid.NewGuid(),
                SaldoFinal = 20,
                SaldoInicial = 10,
                TipoOperacao = Domain.Enum.EnTipoOperacao.Debito,
                Valor = 10
            });

            mockContextRepository.Setup(x => x.FindAll()).ReturnsAsync(_lancamentos);
            mockContextRepository.Setup(x => x.Delete(It.IsAny<LancamentoEntity>())).ReturnsAsync(true);

            DeleteLancamentoHandler _handler = new DeleteLancamentoHandler(mockContextRepository.Object);

            var _return = await _handler.Handle(_validCommand, CancellationToken.None);
            var _data = _return.Data;

            Assert.True(_return.Success);
        }

        [Fact]
        public async Task DeleteLancamentoHandler_delete_invalid_command()
        {
            var mockContextRepository = new Mock<ILancamentoRepository>();
            var _mockMapper = new Mock<IMapper>();

            List<LancamentoEntity> _lancamentos = new List<LancamentoEntity>();

            _lancamentos.Add(new LancamentoEntity()
            {
                DataCriacao = DateTime.Now,
                Id = Guid.NewGuid(),
                SaldoFinal = 20,
                SaldoInicial = 10,
                TipoOperacao = Domain.Enum.EnTipoOperacao.Debito,
                Valor = 10
            });

            _lancamentos.Add(new LancamentoEntity()
            {
                DataCriacao = DateTime.Now,
                Id = Guid.Parse("dc697766-3251-4bbb-98e5-72b2a06047c4"),
                SaldoFinal = 20,
                SaldoInicial = 10,
                TipoOperacao = Domain.Enum.EnTipoOperacao.Credito,
                Valor = 10
            });

            _lancamentos.Add(new LancamentoEntity()
            {
                DataCriacao = DateTime.Now,
                Id = Guid.NewGuid(),
                SaldoFinal = 20,
                SaldoInicial = 10,
                TipoOperacao = Domain.Enum.EnTipoOperacao.Debito,
                Valor = 10
            });

            mockContextRepository.Setup(x => x.FindAll()).ReturnsAsync(_lancamentos);
            mockContextRepository.Setup(x => x.Delete(It.IsAny<LancamentoEntity>())).ReturnsAsync(true);

            DeleteLancamentoHandler _handler = new DeleteLancamentoHandler(mockContextRepository.Object);

            var _return = await _handler.Handle(_invalidCommand, CancellationToken.None);
            var _data = _return.Data;

            Assert.False(_return.Success);
        }

        [Fact]
        public async Task DeleteLancamentoHandler_delete_invalid_IdNotFound()
        {
            var mockContextRepository = new Mock<ILancamentoRepository>();
            var _mockMapper = new Mock<IMapper>();

            List<LancamentoEntity> _lancamentos = new List<LancamentoEntity>();

            _lancamentos.Add(new LancamentoEntity()
            {
                DataCriacao = DateTime.Now,
                Id = Guid.NewGuid(),
                SaldoFinal = 20,
                SaldoInicial = 10,
                TipoOperacao = Domain.Enum.EnTipoOperacao.Debito,
                Valor = 10
            });

            _lancamentos.Add(new LancamentoEntity()
            {
                DataCriacao = DateTime.Now,
                Id = Guid.NewGuid(),
                SaldoFinal = 20,
                SaldoInicial = 10,
                TipoOperacao = Domain.Enum.EnTipoOperacao.Credito,
                Valor = 10
            });

            _lancamentos.Add(new LancamentoEntity()
            {
                DataCriacao = DateTime.Now,
                Id = Guid.NewGuid(),
                SaldoFinal = 20,
                SaldoInicial = 10,
                TipoOperacao = Domain.Enum.EnTipoOperacao.Debito,
                Valor = 10
            });

            mockContextRepository.Setup(x => x.FindAll()).ReturnsAsync(_lancamentos);
            mockContextRepository.Setup(x => x.Delete(It.IsAny<LancamentoEntity>())).ReturnsAsync(true);

            DeleteLancamentoHandler _handler = new DeleteLancamentoHandler(mockContextRepository.Object);

            var _return = await _handler.Handle(_validCommand, CancellationToken.None);
            var _data = _return.Data;

            Assert.False(_return.Success);
            Assert.Equal("Registro não localizado.", _data);
        }

        [Fact]
        public async Task DeleteLancamentoHandler_delete_valid_IdLastPosition()
        {
            var mockContextRepository = new Mock<ILancamentoRepository>();
            var _mockMapper = new Mock<IMapper>();

            List<LancamentoEntity> _lancamentos = new List<LancamentoEntity>();

            _lancamentos.Add(new LancamentoEntity()
            {
                DataCriacao = DateTime.Now,
                Id = Guid.NewGuid(),
                SaldoFinal = 20,
                SaldoInicial = 10,
                TipoOperacao = Domain.Enum.EnTipoOperacao.Debito,
                Valor = 10
            });

            _lancamentos.Add(new LancamentoEntity()
            {
                DataCriacao = DateTime.Now,
                Id = Guid.NewGuid(),
                SaldoFinal = 20,
                SaldoInicial = 10,
                TipoOperacao = Domain.Enum.EnTipoOperacao.Credito,
                Valor = 10
            });

            _lancamentos.Add(new LancamentoEntity()
            {
                DataCriacao = DateTime.Now,
                Id = Guid.Parse("dc697766-3251-4bbb-98e5-72b2a06047c4"),
                SaldoFinal = 20,
                SaldoInicial = 10,
                TipoOperacao = Domain.Enum.EnTipoOperacao.Debito,
                Valor = 10
            });

            mockContextRepository.Setup(x => x.FindAll()).ReturnsAsync(_lancamentos);
            mockContextRepository.Setup(x => x.Delete(It.IsAny<LancamentoEntity>())).ReturnsAsync(true);

            DeleteLancamentoHandler _handler = new DeleteLancamentoHandler(mockContextRepository.Object);

            var _return = await _handler.Handle(_validCommand, CancellationToken.None);
            var _data = _return.Data;

            Assert.True(_return.Success);
            Assert.True((bool)_data);
        }
    }
}