using LoginRegistroAPI.Models;
using LoginRegistroAPI.Servicios.Logica;

namespace LoginRegistroAPI.Servicios.Interfaces
{
    public interface IUsuario
    {
        Task<bool> PostRegistrar(Usuario Ob);
        Task<string> PostLogin(UsuarioLogin Ob);
        Task<List<Usuario>> GetAllUsuarios();
        Task<string> PostArchivos(Documentos Ob);


    }
}
