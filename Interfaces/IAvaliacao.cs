using Bolos_do_Jacquin.Models;

namespace Bolos_do_Jacquin.Interfaces
{
    public interface IAvaliacao
    {
        Task Cadastrar(Avaliacao avaliacao);

        Task Deletar(Guid id);

        Task<List<Avaliacao>> Listar();

        Task<List<Avaliacao>> ListarPorProduto(Guid idProduto);

        Task<Avaliacao?> BuscarPorId(Guid id);
    }
}
