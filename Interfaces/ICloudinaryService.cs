namespace Bolos_do_Jacquin.Interfaces
{
    public interface ICloudinaryService
    {
        //IFormFile: arquivo binário que chega bo multipart/form-data 
        //É a imagem!
        Task<string> UploadImagem(IFormFile arquivo);
    }
}
