using Bolos_do_Jacquin.BdContextEvent;
using Bolos_do_Jacquin.Interfaces;
using Bolos_do_Jacquin.Models;
using Microsoft.EntityFrameworkCore;

namespace Bolos_do_Jacquin.Repositories
{
    public class ProdutoRepository : IProduto
    {
        private readonly BolosDoJacquinContext _context;

        public ProdutoRepository(BolosDoJacquinContext context)
        {
            _context = context;
        }

        public async Task Atualizar(Guid id, Produto produto)
        {
            var ProdutoBuscado = await
                _context.Produto.FindAsync(id);

            if (ProdutoBuscado != null)
            {
                ProdutoBuscado.IdCategoria = produto.IdCategoria;
                ProdutoBuscado.Nome = produto.Nome;
                ProdutoBuscado.Preco = produto.Preco;
                ProdutoBuscado.Imagem = produto.Imagem;
                ProdutoBuscado.DescricaoCurta = produto.DescricaoCurta;
                ProdutoBuscado.DescricaoLonga = produto.DescricaoLonga;
                ProdutoBuscado.Disponibilidade = produto.Disponibilidade;
                ProdutoBuscado.Situacao = produto.Situacao;

                _context.Produto.Update(ProdutoBuscado);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<Produto?> ListarPorProduto(Guid id)
        {
            return await _context.Produto
                .FirstOrDefaultAsync(p => p.IdProduto == id);
        }

        public async Task Cadastrar(Produto produto)
        {
            await _context.Produto.AddAsync(produto);

            await _context.SaveChangesAsync();
        }

        public async Task Deletar(Guid id)
        {
            var ProdutoBuscado = await
                _context.Produto.FindAsync(id);

            if (ProdutoBuscado != null)
            {
                _context.Produto.Remove(ProdutoBuscado);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Produto>> ListarTodos()
        {
            return await _context.Produto
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
