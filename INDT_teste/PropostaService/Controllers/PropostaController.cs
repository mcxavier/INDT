using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropostaService.Application.Tools;
using PropostaService.Core.DTO;
using PropostaService.Core.Interfaces;

namespace PropostaService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropostaController : ControllerBase
    {
        private readonly IPropostaUseCase _propostaUseCase;
        private readonly ILogger<PropostaController> _logger;

        public PropostaController(
            IPropostaUseCase propostaUseCase,
            ILogger<PropostaController> logger
        )
        {
            _propostaUseCase = propostaUseCase;
            _logger = logger;
        }

        [HttpGet("GetById/{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                var response = await _propostaUseCase.GetPropostaById(id);

                switch (response.statusCode)
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
        public async Task<ActionResult<PagedResponseBaseDTO<PropostaDTO>>> List()
        {
            try
            {
                var queryStringDictionary = Utils.GetQueryStringDictionary(Request.QueryString.Value ?? "");
                var response = await _propostaUseCase.ListPropostaByParams(queryStringDictionary);

                switch (response.statusCode)
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

        [HttpPut("Upsert/{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PropostaDTO>> Upsert(int id, [FromBody] PropostaDTO propostaRequest)
        {
            try
            {
                propostaRequest.id = id;
                var response = await _propostaUseCase.UpsertProposta(propostaRequest);

                switch (response.statusCode)
                {
                    case 409:
                        return Conflict(response);
                    case 200:
                        return Ok(response);
                    case 201:
                        return CreatedAtAction(nameof(Upsert), response);
                    default:
                        return Problem(response.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao inserir/atualizar proposta");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno no servidor");
            }
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
                var response = await _propostaUseCase.ContrataProposta(id);

                switch (response.statusCode)
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

    }
}
