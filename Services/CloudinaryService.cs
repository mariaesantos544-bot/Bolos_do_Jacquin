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
            // desempacota as configurações (CloudName, ApiKey e ApiSecret)
            var credenciais = options.Value;


            // Account = "Carteira" com as três credenciais que autenticam na conta do Cloudinaryy
            var account = new Account(credenciais.CloudName, credenciais.ApiKey, credenciais.ApiSecret);


            // Cria o cliente de fato, já autenticado com as credenciais
            _cloudinary = new Cloudinary(account);

            // Definindo que as Urls geradas venham como https
            _cloudinary.Api.Secure = true;
        }




        public async Task<string> UploadImagem(IFormFile arquivo)
        {
            //Abre um fluxo de leitura do arquivo
            // using: garanteque o stream será fechado após o uso(libera a memória mesmo se der erro)
            using var stream = arquivo.OpenReadStream();

            // Monta os parámetros do upload
            var uploadParams = new ImageUploadParams
            {
                //o arquivo em si: nome original + o fluxo de bytes a enviar
                File = new FileDescription(arquivo.FileName, stream),

                // Pasta de destino dentro do Cloudinary
                Folder = "Bolos_do_Jacquin/avaliacao"
            };

            // Envia a imagem para o Cloudinary e aguarda a resposta com os dados do upload
            var resultado = await _cloudinary.UploadAsync(uploadParams);

            // Retorna só o que interessa para a aplicação (URL segura da imagem)
            // QUe depois será sala no campo ImagemUrl(em Eventos)
            return resultado.SecureUrl.AbsoluteUri;
        }
    }
}

