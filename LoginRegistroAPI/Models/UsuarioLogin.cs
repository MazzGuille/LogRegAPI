using System.ComponentModel.DataAnnotations;

namespace LoginRegistroAPI.Models
{
    public class UsuarioLogin
    {
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Contraseña { get; set; }
    }
}
