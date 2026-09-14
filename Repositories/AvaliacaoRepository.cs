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

        public async Task Atualizar(Guid id, Avaliacao avaliacao)
        {
            var AvaliacaoBuscada = await
                _context.Avaliacao.FindAsync(id);

            if (AvaliacaoBuscada != null)
            {
                AvaliacaoBuscada.Nota = avaliacao.Nota;
                AvaliacaoBuscada.Comentario = avaliacao.Comentario;
                AvaliacaoBuscada.Situacao = avaliacao.Situacao;
                AvaliacaoBuscada.MotivoOcultacao = avaliacao.MotivoOcultacao;
                AvaliacaoBuscada.DataAlteracao = DateTime.Now;

                _context.Avaliacao.Update(AvaliacaoBuscada);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<Avaliacao?> BuscarPorId(Guid id)
        {
            return await _context.Avaliacao
                .FirstOrDefaultAsync(a => a.IdAvaliacao == id);
        }

        public async Task Cadastrar(Avaliacao avaliacao)
        {
            avaliacao.DataCriacao = DateTime.Now;

            await _context.Avaliacao.AddAsync(avaliacao);

            await _context.SaveChangesAsync();
        }

        public async Task Deletar(Guid id)
        {
            var avaliacao = await
                _context.Avaliacao.FindAsync(id);

            if (avaliacao != null)
            {
                _context.Avaliacao.Remove(avaliacao);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Avaliacao>> ListarTodos()
        {
            return await _context.Avaliacao
                .ToListAsync();
        }

    }
}