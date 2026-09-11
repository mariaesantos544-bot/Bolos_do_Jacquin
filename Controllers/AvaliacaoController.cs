using Bolos_do_Jacquin.DTO;
using Bolos_do_Jacquin.Interfaces;
using Bolos_do_Jacquin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bolos_do_Jacquin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvaliacaoController : ControllerBase
    {
        private readonly IAvaliacao _avaliacao;
        private readonly IModerationService _moderationService;

        public AvaliacaoController(IAvaliacao avaliacao, IModerationService moderationService)
        {
            _avaliacao = avaliacao;
            _moderationService = moderationService;
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] AvaliacaoDTO dto)
        {
            try
            {
                bool reprovado = await _moderationService.ModerarTexto(dto.Comentario);

                var avaliacao = new Avaliacao
                {
                    DataCriacao = DateTime.Now,
                    Comentario = dto.Comentario,
                    Nota = dto.Nota,
                    Situacao = !reprovado,
                    MotivoOcultacao = reprovado ? "Conteúdo reprovado pela moderação." : null,
                    IdProduto = dto.IdProduto,
                    IdUsuario = dto.IdUsuario
                };

                await _avaliacao.Cadastrar(avaliacao);
                return StatusCode(201, avaliacao);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]

        public async Task<IActionResult> Listar()
        {
            try
            {
                var lista = await _avaliacao.Listar();
                return Ok(lista);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> BuscarPorId(Guid id)
        {
            try
            {
                var avaliacao = await _avaliacao.BuscarPorId(id);

                if (avaliacao == null)
                    return NotFound("Avaliação não encontrada.");

                return Ok(avaliacao);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("ListarPorProduto/{idProduto}")]

        public async Task<IActionResult> ListarPorProduto(Guid idProduto)
        {
            try
            {
                var lista = await _avaliacao.ListarPorProduto(idProduto);
                return Ok(lista);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Deletar(Guid id)
        {
            try
            {
                await _avaliacao.Deletar(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
