using LoginRegistroAPI.Models;
using LoginRegistroAPI.Servicios.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoginRegistroAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuario _usuario;

        public UsuarioController(IUsuario Usuario)
        {
            _usuario = Usuario;
        }

        private string Exception()
        {
            throw new NotImplementedException();
        }

        [HttpPost("Login")]
        public async Task<string> Login([FromBody] UsuarioLogin Ob)
        {
            try
            {
                var response = await _usuario.PostLogin(Ob);
                if (response != "Error")
                {
                    return response;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }

        }

        [HttpPost("CrearUsuario")]
        public async Task<string> CrearUsuario([FromBody] Usuario ob)
        {

            try
            {
                var response = await _usuario.PostRegistrar(ob);
                return response.ToString();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message.ToString());
            }


        }



        [HttpPost("CargarArchivo")]
        public async Task<string> CargarArchivo([FromForm] Documentos ob)
        {
            try
            {
                var response = await _usuario.PostArchivos(ob);
                return response.ToString();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message.ToString());
            }
        }

        [Authorize]
        [HttpGet("ListaDeUsuarios")]
        public async Task<List<Usuario>> Listar()
        {
            try
            {
                return await _usuario.GetAllUsuarios();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message.ToString());
            }
        }

    }
}
