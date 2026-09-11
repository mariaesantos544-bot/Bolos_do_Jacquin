using Bolos_do_Jacquin.BdContextEvent;
using Bolos_do_Jacquin.Interfaces;
using Bolos_do_Jacquin.Models;
using Microsoft.EntityFrameworkCore;

namespace Bolos_do_Jacquin.Repositories
{
    public class CategoriaRepository : ICategoria
    {
        private readonly BolosDoJacquinContext _context;

        public CategoriaRepository(BolosDoJacquinContext context)
        {
            _context = context;
        }

        public async Task Atualizar(Guid id, Categoria categoria)
        {
            var CategoriaBuscada = await
                _context.Categoria.FindAsync(id);

            if (CategoriaBuscada != null)
            {
                CategoriaBuscada.Nome = categoria.Nome;

                _context.Categoria.Update(CategoriaBuscada);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<Categoria?> BuscarPorId(Guid id)
        {
            return await _context.Categoria
                .FirstOrDefaultAsync(c => c.IdCategoria == id);
        }

        public async Task Cadastrar(Categoria categoria)
        {
            await _context.Categoria.AddAsync(categoria);

            await _context.SaveChangesAsync();
        }

        public async Task Deletar(Guid id)
        {
            var CategoriaBuscada = await
                _context.Categoria.FindAsync(id);

            if (CategoriaBuscada != null)
            {
                _context.Categoria.Remove(CategoriaBuscada);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Categoria>> Listar()
        {
            return await _context.Categoria
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
