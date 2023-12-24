using ControleLancamento.Api.Api.Controllers;
using ControleLancamento.Api.Application.AppLancamento;
using ControleLancamento.Api.Application.Configuration.Events;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ControleLancamento.Api.XUnitTest.Back.Test.src.ControleLancamento.Api.Api.Test.Controllers
{
    public class LancamentoControllerTest
    {
        [Fact]
        public async Task GetAllLoja_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            var query = new GetAllLancamentoQuery();
            var mediator = new Mock<IMediator>();

            mediator.Setup(x => x.Send(query, default(CancellationToken))).ReturnsAsync(new ResultEvent(true, true));
            var controller = new LancamentoController(mediator.Object);

            // Act
            var actionResult = await controller.GetAllLoja(query);

            // Assert
            Assert.IsType<OkObjectResult>(actionResult);
        }

        [Fact]
        public async Task GetAllLoja_WhenCalled_ReturnsBadRequestResult()
        {
            // Arrange
            var query = new GetAllLancamentoQuery();
            var mediator = new Mock<IMediator>();
            mediator.Setup(x => x.Send(query, default(CancellationToken))).ReturnsAsync(new ResultEvent(false, false));
            var controller = new LancamentoController(mediator.Object);

            // Act
            var actionResult = await controller.GetAllLoja(query);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        [Fact]
        public async Task PostLoja_ReturnsOkResult_WhenCommandIsValid()
        {
            // Arrange
            var command = new PostLancamentoCommand();
            var mediator = new Mock<IMediator>();
            mediator.Setup(x => x.Send(command, default(CancellationToken)))
                .ReturnsAsync(new ResultEvent(true, true));
            var controller = new LancamentoController(mediator.Object);

            // Act
            var result = await controller.PostLoja(command);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task PostLoja_ReturnsBadRequestResult_WhenCommandIsInvalid()
        {
            // Arrange
            var command = new PostLancamentoCommand();
            var mediator = new Mock<IMediator>();
            mediator.Setup(x => x.Send(command, default(CancellationToken)))
                .ReturnsAsync(new ResultEvent(false, false));
            var controller = new LancamentoController(mediator.Object);

            // Act
            var result = await controller.PostLoja(command);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task DeleteCargo_ShouldReturnOkResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(x => x.Send(It.IsAny<DeleteLancamentoCommand>(), default(CancellationToken)))
                .ReturnsAsync(new ResultEvent(true, true));
            var controller = new LancamentoController(mediatorMock.Object);
            var id = Guid.NewGuid();

            // Act
            var result = await controller.DeleteCargo(id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteCargo_ShouldReturnBadRequestResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(x => x.Send(It.IsAny<DeleteLancamentoCommand>(), default(CancellationToken)))
                .ReturnsAsync(new ResultEvent(false, false));
            var controller = new LancamentoController(mediatorMock.Object);
            var id = Guid.NewGuid();

            // Act
            var result = await controller.DeleteCargo(id);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}