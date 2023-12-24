using ControleLancamento.Api.Application.AppLancamento;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControleLancamento.Api.Api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class LancamentoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LancamentoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>Captura todos os lançamentos</summary>
        /// <returns>Captura todos os lançamentos</returns>
        [HttpGet("getAll")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> GetAllLoja(
            [FromQuery] GetAllLancamentoQuery query
        )
        {
            var result = await _mediator.Send(query);

            if (!result.Success)
            {
                return new BadRequestObjectResult(result.Data);
            }
            return new OkObjectResult(result.Data);
        }

        /// <summary>Cadastra Lancamento</summary>
        /// <returns>Cadastra Lancamento</returns>
        [HttpPost()]
        [AllowAnonymous]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> PostLoja(
            [FromBody] PostLancamentoCommand command
        )
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
            {
                return new BadRequestObjectResult(result.Data);
            }
            return new OkObjectResult(result.Data);
        }

        /// <summary>Captura todos os Lancamento</summary>
        /// <returns>Captura todos os Lancamento</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> DeleteCargo(
            [FromRoute] Guid id
        )
        {
            DeleteLancamentoCommand query = new DeleteLancamentoCommand();
            query.Id = id;

            var result = await _mediator.Send(query);

            if (!result.Success)
            {
                return new BadRequestObjectResult(result.Data);
            }
            return new OkObjectResult(result.Data);
        }
    }
}