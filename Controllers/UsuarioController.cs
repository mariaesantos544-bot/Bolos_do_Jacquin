using Bolos_do_Jacquin.DTO;
using Bolos_do_Jacquin.Interfaces;
using Bolos_do_Jacquin.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bolos_do_Jacquin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuario _usuario;

        public UsuarioController(IUsuario usuario)
        {
            _usuario = usuario;
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] UsuarioDTO dto)
        {
            try
            {
                var usuario = new Usuario
                {
                    Nome = dto.Nome,
                    Email = dto.Email,
                    Senha = dto.Senha,
                    Perfil = dto.Perfil,
                    Situacao = dto.Situacao,
                    DataCadastro = DateTime.Now
                };

                await _usuario.Cadastrar(usuario);

                return StatusCode(201, usuario);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] UsuarioDTO dto)
        {
            try
            {
                var novoUsuario = new Usuario
                {
                    Nome = dto.Nome,
                    Email = dto.Email,
                    Senha = dto.Senha,
                    Perfil = dto.Perfil,
                    Situacao = dto.Situacao
                };

                await _usuario.Atualizar(id, novoUsuario);

                return Ok(novoUsuario);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> BuscarPorId(Guid id)
        {
            var usuarioBuscado = await _usuario.BuscarPorId(id);

            if (usuarioBuscado == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            return Ok(usuarioBuscado);
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var usuarios = await _usuario.Listar();

                return Ok(usuarios);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            await _usuario.Deletar(id);

            return NoContent();
        }
    }
}