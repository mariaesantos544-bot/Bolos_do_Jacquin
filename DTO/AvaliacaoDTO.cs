using System.ComponentModel.DataAnnotations;

namespace Bolos_do_Jacquin.DTO
{
    public class AvaliacaoDTO
    {

        public int Nota { get; set; }

        [Required(ErrorMessage = "O texto do comentário é obrigatório.")]
        [StringLength(250, ErrorMessage = "O comentário deve ter no máximo 250 caracteres.")]
        public string Comentario { get; set; } = string.Empty;

        public bool Situacao { get; set; }

        [StringLength(250, ErrorMessage = "O motivo da ocultação deve ter no máximo 250 caracteres.")]
        public string MotivoOcultacao { get; set; } = string.Empty;

        [Required(ErrorMessage = "A data de criação é obrigatória.")]
        public DateTime DataCriacao { get; set; } = DateTime.MinValue;

        public DateTime? DataAlteracao { get; set; } = DateTime.MinValue;

        public Guid IdUsuario { get; set; }

        public Guid IdProduto { get; set; }
    }
}
