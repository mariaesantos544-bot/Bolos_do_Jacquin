using Bolos_do_Jacquin.BdContextEvent;
using Bolos_do_Jacquin.Interfaces;
using Bolos_do_Jacquin.Models;
using Bolos_do_Jacquin.Utils;
using Microsoft.EntityFrameworkCore;

namespace Bolos_do_Jacquin.Repositories
{
    public class UsuarioRepository : IUsuario
    {
        private readonly BolosDoJacquinContext _context;

        public UsuarioRepository(BolosDoJacquinContext context)
        {
            _context = context;
        }

        public async Task Atualizar(Guid id, Usuario usuario)
        {
            var UsuarioBuscado = await
                _context.Usuario.FindAsync(id);

            if (UsuarioBuscado != null)
            {
                UsuarioBuscado.Nome = usuario.Nome;
                UsuarioBuscado.Email = usuario.Email;
                UsuarioBuscado.Perfil = usuario.Perfil;
                UsuarioBuscado.Situacao = usuario.Situacao;

                if (!string.IsNullOrEmpty(usuario.Senha))
                {
                    UsuarioBuscado.Senha =
                        Criptografia.GerarHash(usuario.Senha);
                }

                _context.Usuario.Update(UsuarioBuscado);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<Usuario?> BuscarPorEmailESenha(string email, string senha)
        {
            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(u => u.Email == email);

            if (usuario == null)
            {
                return null;
            }

            bool senhaValida =
                Criptografia.CompararHash(senha, usuario.Senha);

            if (!senhaValida)
            {
                return null;
            }

            return usuario;
        }

        public async Task<Usuario?> BuscarPorId(Guid id)
        {
            return await _context.Usuario
                .FirstOrDefaultAsync(u => u.IdUsuario == id);
        }

        public async Task Cadastrar(Usuario usuario)
        {
            usuario.Senha =
                Criptografia.GerarHash(usuario.Senha);

            usuario.DataCadastro = DateTime.Now;

            await _context.Usuario.AddAsync(usuario);

            await _context.SaveChangesAsync();
        }

        public async Task Deletar(Guid id)
        {
            var UsuarioBuscado = await
                _context.Usuario.FindAsync(id);

            if (UsuarioBuscado != null)
            {
                _context.Usuario.Remove(UsuarioBuscado);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Usuario>> Listar()
        {
            return await _context.Usuario
                .AsNoTracking()
                .ToListAsync();
        }
    }
}