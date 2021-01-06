﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ABB.Catalogo.Entidades.Core;
using ABB.Catalogo.LogicaNegocio.Core;

namespace WebServicesApp.Controllers
{
    public class UsuariosController : ApiController
    {
        // GET: api/Usuarios
        public IEnumerable<Usuario> Get()
        {
            List<Usuario> usuarios = new List<Usuario>();
            usuarios = new UsuariosLN().ListarUsuarios();

            return usuarios;
        }


        // GET: api/Usuarios/{pUsuario}/{pPassword}
        public int Get([FromUri] string pUsuario, [FromUri] string pPassword)
        {
            try
            {
                UsuariosLN usuario = new UsuariosLN();
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
            Usuario usuario = new UsuariosLN().InsertarUsuario(value);
        }

        // PUT: api/Usuarios/5
        public Usuario Put(int id, [FromBody]Usuario value)
        {
            Usuario usuario = new Usuario();
            usuario = new UsuariosLN().ModificarUsuario(id, value);
            return usuario;
        }


        // DELETE: api/Usuarios/5
        public void Delete(int id)
        {
            new UsuariosLN().EliminarUsuario(id);
        }

        public Usuario GetUserId([FromUri] int IdUsuario)
        {
            try
            {
                UsuariosLN usuario = new UsuariosLN();
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
