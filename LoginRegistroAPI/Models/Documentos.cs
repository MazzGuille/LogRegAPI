namespace LoginRegistroAPI.Models
{
    public class Documentos
    {
        public int IdDocuemtno { get; set; }
        public string Descripcion { get; set; }
        public string Ruta { get; set; }
        public string EmailUsu { get; set; }
        public IFormFile Archivo { get; set; }

    }
}
