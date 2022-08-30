using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace LoginRegistroAPI.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Contraseña { get; set; }
        [Required]
        public string ConfirmarClave { get; set; }
        //[MaybeNull]
        //public byte ImagenPerfil { get; set; }
        //[MaybeNull]
        //public string BioUsuario { get; set; } = string.Empty;


    }
}
