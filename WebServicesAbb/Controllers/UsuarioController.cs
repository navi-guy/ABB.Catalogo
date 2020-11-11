using ABB.Catalogo.Entidades.Core;
using ABB.Catalogo.LogicaNegocio.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace WebServicesAbb.Controllers
{
    public class UsuarioController : ApiController
    {
        // GET: api/Usuario
        [HttpGet]
        public IEnumerable<Usuario> Get()
        {
            List<Usuario> usuarios = new List<Usuario>();
            usuarios = new UsuarioLN().ListarUsuarios();

            return usuarios;

        }

        // GET: api/Usuarios/5
        public int Get([FromUri] string pUsuario, [FromUri] string pPassword)
        {
            try
            {
                UsuarioLN usuario = new UsuarioLN();
                return usuario.GetUsuarioId(pUsuario, pPassword);
            }
            catch (Exception ex)
            {
                string innerException = (ex.InnerException == null) ? "" : ex.InnerException.ToString();
                //Logger.paginaNombre = this.GetType().Name;
                //Logger.Escribir("Error en Logica de Negocio: " + ex.Message + ". " + ex.StackTrace + ". " + innerException);
                return -1;
            }

        }

        // POST: api/Usuarios
        public void Post([FromBody] Usuario value)
        {
            Usuario usuario = new UsuarioLN().InsertarUsuario(value);
        }

        // PUT: api/Usuarios/5
        public void Put(int id, [FromBody]Usuario value)
        {
            Usuario usuario = new Usuario();
            usuario = new UsuarioLN().ModificarUsuario(id, value);

        }


        // DELETE: api/Usuarios/5
        public void Delete(int id)
        {
        }

        public Usuario GetUserId([FromUri] int IdUsuario)
        {
            try
            {
                UsuarioLN usuario = new UsuarioLN();
                return usuario.BuscaUsuarioId(IdUsuario);
            }
            catch (Exception ex)
            {
                string innerException = (ex.InnerException == null) ? "" : ex.InnerException.ToString();
                //Logger.paginaNombre = this.GetType().Name;
                //Logger.Escribir("Error en Logica de Negocio: " + ex.Message + ". " + ex.StackTrace + ". " + innerException);
                throw;
            }
        }
    }
}
