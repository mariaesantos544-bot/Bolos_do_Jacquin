namespace Bolos_do_Jacquin.Interfaces
{
    public interface IModerationService
    {
        Task<bool> ModerarTexto(string texto);

    }
}
