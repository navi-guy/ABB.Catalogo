﻿using ABB.Catalogo.Entidades.Core;
using System;
using System.Configuration;
using System.Data.SqlClient;

namespace ABB.Catalogo.AccesoDatos.Core
{
    public class UsuarioApiDA
    {
        public UsuarioApi BuscaUsuarioApi(UsuarioApi ParamUserApi)
        {
            UsuarioApi entidad = new UsuarioApi();

            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnSql"]].ConnectionString))
            {
                using (SqlCommand comando = new SqlCommand("users_UsuarioApi", conexion))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Codigo", ParamUserApi.Codigo);
                    comando.Parameters.AddWithValue("@UserName", ParamUserApi.UserName);
                    comando.Parameters.AddWithValue("@Clave", ParamUserApi.Clave);
                    comando.Parameters.AddWithValue("@Nombre", ParamUserApi.Nombre);
                    comando.Parameters.AddWithValue("@Rol", ParamUserApi.Rol);
                    conexion.Open();
                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        entidad = new UsuarioApi();
                        entidad.Codigo = Convert.ToInt32(reader["Codigo"]);
                        entidad.UserName = reader["UserName"].ToString();
                        entidad.Clave = reader["Clave"].ToString();
                        entidad.Nombre = reader["Nombre"].ToString();
                        entidad.Rol = reader["Rol"].ToString();
                    }
                    conexion.Close();
                }
            }
            return entidad;
        }
    }
}
