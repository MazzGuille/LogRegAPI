using System.ComponentModel.DataAnnotations;

namespace LoginRegistroAPI.Models
{
    public class UsuarioLogin
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Contraseña { get; set; }
    }
}
