using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Bolos_do_Jacquin.Interfaces;
using Bolos_do_Jacquin.Utils;
using Microsoft.Extensions.Options;

namespace Bolos_do_Jacquin.Services
{
    public class CloudinaryService : ICloudinaryService
    {

        private readonly Cloudinary _cloudinary;
        public CloudinaryService(IOptions<CloudinarySettings> options)
        {
            var credenciais = options.Value;

            var account = new Account(credenciais.CloudName, credenciais.ApiKey, credenciais.ApiSecret);

            _cloudinary = new Cloudinary(account);

            _cloudinary.Api.Secure = true;
        }




        public async Task<string> UploadImagem(IFormFile arquivo)
        {
            using var stream = arquivo.OpenReadStream();

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(arquivo.FileName, stream),

                Folder = "Bolos_do_Jacquin/avaliacao"
            };

            var resultado = await _cloudinary.UploadAsync(uploadParams);

            return resultado.SecureUrl.AbsoluteUri;
        }
    }
}

