using System.ComponentModel.DataAnnotations;

namespace Api.Domain.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage ="Email é um campo obrigatório para o login")]
        [EmailAddress(ErrorMessage ="E-mail em formato inválido")]
        [StringLength(100, ErrorMessage ="E-mail deve ter no máximo {1} caracteres.")]
        public string Email { get; set; } = string.Empty;

        public string Name {get;set;} = string.Empty;
    }
}