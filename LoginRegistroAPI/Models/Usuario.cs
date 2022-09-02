using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace LoginRegistroAPI.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        [Required]
        //[DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(4)]
        public string Nombre { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(8)]
        public string UserName { get; set; }
        [Required]
        [MinLength(8)]
        public string Contraseña { get; set; }
        [Required]
        [Compare("Contraseña")]        
        public string ConfirmarClave { get; set; }
        public string BioUsuario { get; set; }

        //Para poder obtener la imagen en bytes
        //public byte ImagenPerfil { get; set; }

        //para valdiar si existe o no la imagen
        //public bool ImgPerf { get; set; }

    }
}
