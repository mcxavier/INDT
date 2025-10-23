using ContratacaoService.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContratacaoService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContratacaoController : ControllerBase
    {
        private readonly IContratacaoUseCase _contratacaoUseCase;
        private readonly ILogger<ContratacaoController> _logger;


        public ContratacaoController(
            IContratacaoUseCase contratacaoUseCase,
            ILogger<ContratacaoController> logger
        )
        {
            _contratacaoUseCase = contratacaoUseCase;
            _logger = logger;
        }



        [HttpPost("Contrata/{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Contrata(int id)
        {
            try
            {
                var response = await _contratacaoUseCase.Contrata(id);

                switch (response.StatusCode)
                {
                    case 200:
                        return Ok(response);
                    case 404:
                        return NotFound($"Registro com ID {id} não encontrado.");
                    case 409:
                        return Conflict(response);
                    default:
                        return Problem(response.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter registro com ID {id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno no servidor");
            }
        }



        [HttpGet("List")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<dynamic>> List()
        {
            try
            {
                var response = await _contratacaoUseCase.List();

                switch (response.StatusCode)
                {
                    case 409:
                        return Conflict(response);
                    case 400:
                        return BadRequest(response);
                    case 200:
                        return Ok(response);
                    default:
                        return Problem(response.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar propostas");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno no servidor");
            }
        }

    }
}
