using ControleLancamento.Api.Api.Controllers;
using ControleLancamento.Api.Application.AppConsolidado;
using ControleLancamento.Api.Application.Configuration.Events;
using ControleLancamento.Api.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ControleLancamento.Api.XUnitTest.Back.Test.src.ControleLancamento.Api.Api.Test.Controllers
{
    public class ConsolidadoControllerTest
    {
        private readonly List<ConsolidadoDto> consolidadoDto = new List<ConsolidadoDto>()
        {
            new ConsolidadoDto()
            {
                Data = DateTime.Now,
                SaldoFinal = 10,
                SaldoInicial = 0,
                TotalCredito = 10
            }
        };

        [Fact]
        public async Task GetAllConsolidado_ReturnsOkObjectResult()
        {
            // Arrange
            var query = new GetAllConsolidadoQuery();
            var mediator = new Mock<IMediator>();
            mediator.Setup(x => x.Send(query, default(CancellationToken))).ReturnsAsync(new ResultEvent(true, consolidadoDto));
            var controller = new ConsolidadoController(mediator.Object);

            // Act
            var result = await controller.GetAllConsolidado(query);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAllConsolidado_ReturnsBadRequestObjectResult()
        {
            // Arrange
            var query = new GetAllConsolidadoQuery();
            var mediator = new Mock<IMediator>();
            mediator.Setup(x => x.Send(query, default(CancellationToken))).ReturnsAsync(new ResultEvent(false, null));
            var controller = new ConsolidadoController(mediator.Object);

            // Act
            var result = await controller.GetAllConsolidado(query);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GetByDataConsolidado_ReturnsOkObjectResult_WhenSuccess()
        {
            // Arrange
            var query = new GetByDataConsolidadoQuery()
            {
                Data = DateTime.Now
            };
            var mediator = new Mock<IMediator>();
            mediator.Setup(x => x.Send(query, default(CancellationToken))).ReturnsAsync(new ResultEvent(true, consolidadoDto));
            var controller = new ConsolidadoController(mediator.Object);

            // Act
            var result = await controller.GetByDataConsolidado(query);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetByDataConsolidado_ReturnsBadRequestObjectResult_WhenFailed()
        {
            // Arrange
            var query = new GetByDataConsolidadoQuery();
            var mediator = new Mock<IMediator>();
            mediator.Setup(x => x.Send(query, default(CancellationToken))).ReturnsAsync(new ResultEvent(false, null));
            var controller = new ConsolidadoController(mediator.Object);

            // Act
            var result = await controller.GetByDataConsolidado(query);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}