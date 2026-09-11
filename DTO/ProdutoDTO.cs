using System.ComponentModel.DataAnnotations;

namespace Bolos_do_Jacquin.DTO
{
    public class ProdutoDTO
    {

        [Required(ErrorMessage = "Campo é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres!")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O preço é obrigatório.")]
        [Range(0.01, 99999999.99, ErrorMessage = "Informe um preço válido.")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "A imagem é obrigatória.")]
        [StringLength(500, ErrorMessage = "A imagem deve ter no máximo 500 caracteres!")]
        public string? Imagem { get; set; } = string.Empty;

        public IFormFile? ArquivoImagem { get; set; }

        [Required(ErrorMessage = "Campo é obrigatório")]
        [StringLength(100, ErrorMessage = "a descrição curta deve ter no máximo 100 caracteres!")]
        public string DescricaoCurta { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo é obrigatório")]
        [StringLength(250, ErrorMessage = "a descrição longa deve ter no máximo 250 caracteres!")]
        public string DescricaoLonga { get; set; } = string.Empty;

        public bool Disponibilidade { get; set; }

        public bool Situacao { get; set; }

        public Guid IdProduto { get; set; }

        public Guid IdCategoria { get; set; }
    }
}
