using Bolos_do_Jacquin.DTO;
using Bolos_do_Jacquin.Interfaces;
using Bolos_do_Jacquin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bolos_do_Jacquin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class CategoriaController : ControllerBase
    {
        private readonly ICategoria _categoria;

        public CategoriaController(ICategoria categoria)
        {
            _categoria = categoria;
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Cadastrar([FromBody] CategoriaDTO dto)
        {
            try
            {
                var categoria = new Categoria
                {
                    Nome = dto.Nome
                };

                await _categoria.Cadastrar(categoria);

                return StatusCode(201, categoria);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Atualizar(
            Guid id,
            [FromBody] CategoriaDTO dto)
        {
            try
            {
                var novaCategoria = new Categoria
                {
                    Nome = dto.Nome
                };

                await _categoria.Atualizar(id, novaCategoria);

                return Ok(novaCategoria);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Administrador, Cliente")]
        public async Task<IActionResult> BuscarPorId(Guid id)
        {
            var categoriaBuscada = await _categoria.BuscarPorId(id);

            if (categoriaBuscada == null)
            {
                return NotFound("Categoria não encontrada.");
            }

            return Ok(categoriaBuscada);
        }

        [HttpGet]
        [Authorize(Roles = "Administrador, Cliente")]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var categorias = await _categoria.ListarTodos();

                return Ok(categorias);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            await _categoria.Deletar(id);

            return NoContent();
        }
    }
}
