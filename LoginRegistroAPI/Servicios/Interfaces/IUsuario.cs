using LoginRegistroAPI.Models;
using LoginRegistroAPI.Servicios.Logica;

namespace LoginRegistroAPI.Servicios.Interfaces
{
    public interface IUsuario
    {
        Task<string> PostRegistrar(Usuario Ob);
        Task<string> PostLogin(Usuario Ob);
        Task<List<Usuario>> GetAllUsuarios();
        Task<string> PostArchivos(Documentos Ob);


    }
}
