using Bolos_do_Jacquin.Models;

namespace Bolos_do_Jacquin.Interfaces
{
    public interface IUsuario
    {
        Task Cadastrar(Usuario usuario);

        Task Atualizar(Guid id, Usuario novoUsuario);

        Task Deletar(Guid id);

        Task<List<Usuario>> Listar();

        Task<Usuario?> BuscarPorId(Guid id);

        Task<Usuario?> BuscarPorEmailESenha(string email, string senha);
    }
}
