using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace LoginRegistroAPI.Models
{
    public class Usuario
    {
        public int Id { get; set; }
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
        public string BioUsuario { get; set; }

        //Para poder obtener la imagen en bytes
        //public byte ImagenPerfil { get; set; }

        //para valdiar si existe o no la imagen
        //public bool ImgPerf { get; set; }

    }
}
