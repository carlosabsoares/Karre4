using ControleLancamento.Api.Application.AppLancamento;
using Xunit;

namespace ControleLancamento.Api.XUnitTest.Back.src.ControleLancamento.Api.Application
{
    public class DeleteLancamentoCommandTest
    {
        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        public void Dado_um_comando_invalido(string param)
        {
            DeleteLancamentoCommand _invalidCommand = new DeleteLancamentoCommand()
            {
                Id = Guid.Parse(param)
            };

            _invalidCommand.Validate();

            Assert.Equal(_invalidCommand.Valid, false);
        }

        [Fact]
        public void Dado_um_comando_valido()
        {
            DeleteLancamentoCommand _validCommand = new DeleteLancamentoCommand()
            {
                Id = Guid.NewGuid()
            };

            _validCommand.Validate();

            Assert.Equal(_validCommand.Valid, true);
        }
    }
}