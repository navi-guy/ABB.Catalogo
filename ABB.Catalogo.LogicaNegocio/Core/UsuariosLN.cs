﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABB.Catalogo.Entidades.Core;
using ABB.Catalogo.AccesoDatos.Core;
using ABB.Catalogo.Entidades.Base;
using ABB.Catalogo.Utiles.Helpers;

namespace ABB.Catalogo.LogicaNegocio.Core
{

    public class UsuariosLN:BaseLN
    {
        public List<Usuario> ListarUsuarios()
        {
            List<Usuario> lista = new List<Usuario>();
            try
            {
                UsuariosDA usuarios = new UsuariosDA();
                return usuarios.ListarUsuarios();


            }
            catch (Exception ex)
            {
                string innerException = (ex.InnerException == null) ? "" : ex.InnerException.ToString();
                //Logger.paginaNombre = this.GetType().Name;
                //Logger.Escribir("Error en Logica de Negocio: " + ex.Message + ". " + ex.StackTrace + ". " + innerException);
                return lista;
            }
        }

        public int GetUsuarioId(string pUsuario, string pPassword)
        {
            try
            {
                UsuariosDA usuario = new UsuariosDA();
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

        public Usuario InsertarUsuario(Usuario usuario)
        {
            try
            {
                return new UsuariosDA().InsertarUsuario(usuario);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }

        public Usuario ModificarUsuario(int id, Usuario usuario)
        {
            try
            {
                UsuariosDA usuarioDA = new UsuariosDA();
                return usuarioDA.ModificarUsuario(id, usuario);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }

        public Usuario BuscaUsuarioId(int pUsuarioId)
        {
            try
            {
                UsuariosDA usuario = new UsuariosDA();
                return usuario.BuscaUsuarioId(pUsuarioId);
            }
            catch (Exception ex)
            {
                string innerException = (ex.InnerException == null) ? "" : ex.InnerException.ToString();
                //Logger.paginaNombre = this.GetType().Name;
                //Logger.Escribir("Error en Logica de Negocio: " + ex.Message + ". " + ex.StackTrace + ". " + innerException);
                throw;
            }
        }

        public void EliminarUsuario(int id)
        {
            try
            {
                new UsuariosDA().EliminarUsuario(id);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }

    }

}
