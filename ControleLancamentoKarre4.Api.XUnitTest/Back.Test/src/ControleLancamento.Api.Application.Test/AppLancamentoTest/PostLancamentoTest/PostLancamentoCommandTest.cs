using ControleLancamento.Api.Application.AppLancamento;
using Xunit;

namespace ControleLancamento.Api.XUnitTest.Back.Test.src.ControleLancamento.Api.Application
{
    public class PostLancamentoCommandTest
    {
        [Fact]
        public void Dado_um_comando_valido_credito()
        {
            PostLancamentoCommand _validCommand = new PostLancamentoCommand()
            {
                TipoOperacao = Domain.Enum.EnTipoOperacao.Credito,
                Valor = 20
            };

            _validCommand.Validate();

            Assert.Equal(_validCommand.Valid, true);
        }

        [Fact]
        public void Dado_um_comando_valido_debito()
        {
            PostLancamentoCommand _validCommand = new PostLancamentoCommand()
            {
                TipoOperacao = Domain.Enum.EnTipoOperacao.Debito,
                Valor = 20
            };

            _validCommand.Validate();

            Assert.Equal(_validCommand.Valid, true);
        }

        [Fact]
        public void Dado_um_comando_invalido_ValorZero()
        {
            PostLancamentoCommand _validCommand = new PostLancamentoCommand()
            {
                TipoOperacao = Domain.Enum.EnTipoOperacao.Credito,
                Valor = 0
            };

            _validCommand.Validate();

            Assert.Equal(_validCommand.Valid, false);
        }
    }
}