using System.ComponentModel.DataAnnotations;

namespace Bolos_do_Jacquin.DTO
{
    public class CategoriaDTO
    {
        [Required(ErrorMessage = "Campo é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome da categoria deve ter no máximo 100 caracteres!")]
        public string Nome { get; set; } = string.Empty;
    }
}
