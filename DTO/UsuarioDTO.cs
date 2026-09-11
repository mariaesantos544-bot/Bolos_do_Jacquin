using System.ComponentModel.DataAnnotations;

namespace Bolos_do_Jacquin.DTO
{
    public class UsuarioDTO
    {

        [Required(ErrorMessage = "Campo é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres!")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo é obrigatório!")]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido!")]
        [StringLength(100, ErrorMessage = "O e-mail deve ter no máximo 100 caracteres!")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo é obrigatório")]
        [StringLength(60, ErrorMessage = "A senha deve ter no entre 6 e 60 caracteres!")]
        public string Senha { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo é obrigatório")]
        [StringLength(20, ErrorMessage = "O perfil deve ter no máximo 20 caracteres!")]
        public string Perfil { get; set; } = string.Empty;

        public bool Situacao { get; set; }

        [Required(ErrorMessage = "A data de cadastro é obrigatória.")]
        public DateTime DataCadastro { get; set; } = DateTime.MinValue;

    }
}
