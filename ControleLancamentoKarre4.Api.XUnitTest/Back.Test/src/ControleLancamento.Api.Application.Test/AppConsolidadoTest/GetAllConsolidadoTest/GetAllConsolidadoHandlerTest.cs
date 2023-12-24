using AutoMapper;
using ControleLancamento.Api.Application.AppConsolidado;
using ControleLancamento.Api.Domain.Entities;
using ControleLancamento.Api.Domain.Repositories;
using Moq;
using Xunit;

namespace ControleLancamento.Api.XUnitTest.Back.Test.src.ControleLancamento.Api.Application.Test.AppConsolidadoTest.GetAllConsolidadoTest
{
    public class GetAllConsolidadoHandlerTest
    {
        private readonly GetAllConsolidadoQuery _command = new GetAllConsolidadoQuery();

        [Fact]
        public async Task GetAllConsolidadoHandler_valid_ReturnValue()
        {
            var mockContextRepository = new Mock<ILancamentoRepository>();

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

            GetAllConsolidadoHandler _handler = new GetAllConsolidadoHandler(mockContextRepository.Object);

            var _return = await _handler.Handle(_command, CancellationToken.None);
            var _data = _return.Data;
            Assert.NotNull(_data);
            Assert.True(_return.Success);
        }

        [Fact]
        public async Task GetAllLancamentoHandler_valid_ReturnNull()
        {
            var mockContextRepository = new Mock<ILancamentoRepository>();
            var _mockMapper = new Mock<IMapper>();

            List<LancamentoEntity> _lancamentos = new List<LancamentoEntity>();

            mockContextRepository.Setup(x => x.FindAll()).ReturnsAsync(_lancamentos);
            mockContextRepository.Setup(x => x.Delete(It.IsAny<LancamentoEntity>())).ReturnsAsync(true);

            GetAllConsolidadoHandler _handler = new GetAllConsolidadoHandler(mockContextRepository.Object);

            var _return = await _handler.Handle(_command, CancellationToken.None);
            var _data = _return.Data;
            Assert.Null(_data);
            Assert.True(_return.Success);
        }
    }
}