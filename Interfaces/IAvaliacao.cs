using Bolos_do_Jacquin.Models;

namespace Bolos_do_Jacquin.Interfaces
{
    public interface IAvaliacao
    {
        Task Cadastrar(Avaliacao avaliacao);

        Task Deletar(Guid id);

        Task<List<Avaliacao>> ListarTodos();

        Task<Avaliacao?> BuscarPorId(Guid id);

        Task Atualizar(Guid id, Avaliacao avaliacao);

    }
}