using ControleLancamento.Api.Application.AppConsolidado;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ControleLancamento.Api.Api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class ConsolidadoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConsolidadoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>Captura todos os lançamentos</summary>
        /// <returns>Captura todos os lançamentos</returns>
        [HttpGet("getAll")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> GetAllConsolidado(
            [FromQuery] GetAllConsolidadoQuery query
        )
        {
            var result = await _mediator.Send(query);

            if (!result.Success)
            {
                return new BadRequestObjectResult(result.Data);
            }
            return new OkObjectResult(result.Data);
        }

        /// <summary>Captura todos os lançamentos</summary>
        /// <returns>Captura todos os lançamentos</returns>
        [HttpGet("getByData")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> GetByDataConsolidado(
            [FromQuery] GetByDataConsolidadoQuery query
        )
        {
            var result = await _mediator.Send(query);

            if (!result.Success)
            {
                return new BadRequestObjectResult(result.Data);
            }
            return new OkObjectResult(result.Data);
        }
    }
}