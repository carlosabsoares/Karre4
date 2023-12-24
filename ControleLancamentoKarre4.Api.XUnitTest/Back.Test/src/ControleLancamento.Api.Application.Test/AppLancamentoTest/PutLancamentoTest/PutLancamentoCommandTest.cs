using ControleLancamento.Api.Application.AppLancamento;
using Xunit;

namespace ControleLancamento.Api.XUnitTest.Back.Test.src.ControleLancamento.Api.Application.Test.PutLancamentoTest
{
    public class PutLancamentoCommandTest
    {
        [Fact]
        public void Dado_um_comando_valido()
        {
            PutLancamentoCommand _validCommand = new PutLancamentoCommand()
            {
                Id = Guid.NewGuid(),
                Valor = 20,
                TipoOperacao = Domain.Enum.EnTipoOperacao.Credito,
                Data = DateTime.Now
            };

            _validCommand.Validate();

            Assert.Equal(_validCommand.Valid, true);
        }

        [Fact]
        public void Dado_um_comando_invalido_valorZerado()
        {
            PutLancamentoCommand _validCommand = new PutLancamentoCommand()
            {
                Id = Guid.NewGuid(),
                Valor = 0,
                TipoOperacao = Domain.Enum.EnTipoOperacao.Credito,
                Data = DateTime.Now
            };

            _validCommand.Validate();

            Assert.Equal(_validCommand.Valid, false);
        }

        [Fact]
        public void Dado_um_comando_invalido_valorNull()
        {
            PutLancamentoCommand _validCommand = new PutLancamentoCommand()
            {
                Id = Guid.NewGuid(),
                TipoOperacao = Domain.Enum.EnTipoOperacao.Credito,
                Data = DateTime.Now
            };

            _validCommand.Validate();

            Assert.Equal(_validCommand.Valid, false);
        }

        [Fact]
        public void Dado_um_comando_invalido_dataZerada()
        {
            PutLancamentoCommand _validCommand = new PutLancamentoCommand()
            {
                Id = Guid.NewGuid(),
                Valor = 10,
                TipoOperacao = Domain.Enum.EnTipoOperacao.Credito
            };

            _validCommand.Validate();

            Assert.Equal(_validCommand.Valid, false);
        }
    }
}