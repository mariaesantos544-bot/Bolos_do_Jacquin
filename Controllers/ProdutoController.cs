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

    public class ProdutoController : ControllerBase
    {
        private readonly IProduto _produto;

        public ProdutoController(IProduto produto)
        {
            _produto = produto;
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Cadastrar([FromBody] ProdutoDTO dto)
        {
            try
            {
                var produto = new Produto
                {
                    Nome = dto.Nome,
                    Preco = dto.Preco,
                    Imagem = dto.Imagem,
                    DescricaoCurta = dto.DescricaoCurta,
                    DescricaoLonga = dto.DescricaoLonga,
                    Disponibilidade = dto.Disponibilidade,
                    Situacao = dto.Situacao,
                    IdCategoria = dto.IdCategoria
                };

                await _produto.Cadastrar(produto);

                return StatusCode(201, produto);
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
            [FromBody] ProdutoDTO dto)
        {
            try
            {
                var novoProduto = new Produto
                {
                    Nome = dto.Nome,
                    Preco = dto.Preco,
                    Imagem = dto.Imagem,
                    DescricaoCurta = dto.DescricaoCurta,
                    DescricaoLonga = dto.DescricaoLonga,
                    Disponibilidade = dto.Disponibilidade,
                    Situacao = dto.Situacao,
                    IdCategoria = dto.IdCategoria
                };

                await _produto.Atualizar(id, novoProduto);

                return Ok(novoProduto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Administrador, Cliente")]
        public async Task<IActionResult> ListarPorProduto(Guid id)
        {
            var produtoBuscado = await _produto.ListarPorProduto(id);

            if (produtoBuscado == null)
            {
                return NotFound("Produto não encontrado.");
            }

            return Ok(produtoBuscado);
        }

        [HttpGet]
        [Authorize(Roles = "Administrador, Cliente")]
        public async Task<IActionResult> ListarTodos()
        {
            try
            {
                var produtos = await _produto.ListarTodos();

                return Ok(produtos);
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
            await _produto.Deletar(id);

            return NoContent();
        }
    }
}