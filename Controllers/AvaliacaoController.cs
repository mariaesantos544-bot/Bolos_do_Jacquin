using Bolos_do_Jacquin.DTO;
using Bolos_do_Jacquin.Interfaces;
using Bolos_do_Jacquin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bolos_do_Jacquin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrador, Cliente")]
    public class AvaliacaoController : ControllerBase
    {
        private readonly IAvaliacao _avaliacao;
        private readonly IModerationService _moderationService;

        public AvaliacaoController(
            IAvaliacao avaliacao,
            IModerationService moderationService)
        {
            _avaliacao = avaliacao;
            _moderationService = moderationService;
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] AvaliacaoDTO dto)
        {
            try
            {
                bool reprovado =
                    await _moderationService.ModerarTexto(dto.Comentario);

                var avaliacao = new Avaliacao
                {
                    DataCriacao = DateTime.Now,
                    Comentario = dto.Comentario,
                    Nota = dto.Nota,
                    Situacao = !reprovado,
                    MotivoOcultacao = reprovado
                        ? "Conteúdo reprovado pela moderação."
                        : null,
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(
            Guid id,
            [FromBody] AvaliacaoDTO dto)
        {
            try
            {
                var avaliacao = await _avaliacao.BuscarPorId(id);

                if (avaliacao == null)
                    return NotFound("Avaliação não encontrada.");

                bool reprovado =
                    await _moderationService.ModerarTexto(dto.Comentario);

                avaliacao.Nota = dto.Nota;
                avaliacao.Comentario = dto.Comentario;
                avaliacao.Situacao = !reprovado;
                avaliacao.MotivoOcultacao = reprovado
                    ? "Conteúdo reprovado pela moderação."
                    : null;

                await _avaliacao.Atualizar(id, avaliacao);

                return Ok(avaliacao);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ListarTodos()
        {
            try
            {
                var lista = await _avaliacao.ListarTodos();

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