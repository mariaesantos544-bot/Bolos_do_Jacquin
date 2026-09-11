using Bolos_do_Jacquin.BdContextEvent;
using Bolos_do_Jacquin.Interfaces;
using Bolos_do_Jacquin.Models;
using Microsoft.EntityFrameworkCore;

namespace Bolos_do_Jacquin.Repositories
{
    public class AvaliacaoRepository : IAvaliacao
    {
        private readonly BolosDoJacquinContext _context;

        public AvaliacaoRepository(BolosDoJacquinContext context)
        {
            _context = context;
        }

        public async Task<Avaliacao?> BuscarPorId(Guid id)
        {
            return await _context.Avaliacao.FirstOrDefaultAsync(a => a.IdAvaliacao == id);
        }

        public async Task Cadastrar(Avaliacao avaliacao)
        {
            avaliacao.DataCriacao = DateTime.Now;
            await _context.Avaliacao.AddAsync(avaliacao);
            await _context.SaveChangesAsync();
        }

        public async Task Deletar(Guid id)
        {
            var avaliacao = await _context.Avaliacao.FindAsync(id);
            if (avaliacao != null)
            {
                _context.Avaliacao.Remove(avaliacao);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Avaliacao>> Listar()
        {
            return await _context.Avaliacao.ToListAsync();
        }

        public async Task<List<Avaliacao>> ListarPorProduto(Guid idProduto)
        {
            return await _context.Avaliacao
                .Where(a => a.IdProduto == idProduto)
                .ToListAsync();
        }
    }
}
