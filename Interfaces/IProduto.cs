using Bolos_do_Jacquin.Models;

namespace Bolos_do_Jacquin.Interfaces
{
    public interface IProduto
    {
        Task Cadastrar(Produto produto);

        Task<List<Produto>> Listar();

        Task Atualizar(Guid id, Produto produto);

        Task Deletar(Guid id);

        Task<Produto?> BuscarPorId(Guid id);
    }
}
