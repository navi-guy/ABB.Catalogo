using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABB.Catalogo.Entidades.Core;

namespace ABB.Catalogo.AccesoDatos.Core
{ 
    public class ProductoDA
    {
        public Producto LlenarEntidad(IDataReader reader)
        {
            Producto producto = new Producto();

            reader.GetSchemaTable().DefaultView.RowFilter = "ColumnName = 'IdProducto'";
            if (reader.GetSchemaTable().DefaultView.Count.Equals(1))
            {
                if (!Convert.IsDBNull(reader["IdProducto"]))
                {
                    producto.IdProducto = Convert.ToInt32(reader["IdProducto"]);
                }
            }

            reader.GetSchemaTable().DefaultView.RowFilter = "ColumnName = 'IdCategoria'";
            if (reader.GetSchemaTable().DefaultView.Count.Equals(1))
            {
                if (!Convert.IsDBNull(reader["IdCategoria"]))
                {
                    producto.IdCategoria = Convert.ToInt32(reader["IdCategoria"]);
                }
            }

            reader.GetSchemaTable().DefaultView.RowFilter = "ColumnName = 'DescCategoria'";
            if (reader.GetSchemaTable().DefaultView.Count.Equals(1))
            {
                if (!Convert.IsDBNull(reader["DescCategoria"]))
                   producto.DescCategoria = Convert.ToString(reader["DescCategoria"]);

            }

            reader.GetSchemaTable().DefaultView.RowFilter = "ColumnName = 'NomProducto'";
            if (reader.GetSchemaTable().DefaultView.Count.Equals(1))
            {
                if (!Convert.IsDBNull(reader["NomProducto"]))
                {
                    producto.NomProducto = Convert.ToString(reader["NomProducto"]);
                }
            }

            reader.GetSchemaTable().DefaultView.RowFilter = "ColumnName = 'MarcaProducto'";
            if (reader.GetSchemaTable().DefaultView.Count.Equals(1))
            {
                if (!Convert.IsDBNull(reader["MarcaProducto"]))
                {
                    producto.MarcaProducto = Convert.ToString(reader["MarcaProducto"]);
                }
            }

            reader.GetSchemaTable().DefaultView.RowFilter = "ColumnName = 'ModeloProducto'";
            if (reader.GetSchemaTable().DefaultView.Count.Equals(1))
            {
                if (!Convert.IsDBNull(reader["ModeloProducto"]))
                {
                   producto.ModeloProducto = Convert.ToString(reader["ModeloProducto"]);
                }
            }

            reader.GetSchemaTable().DefaultView.RowFilter = "ColumnName = 'LineaProducto'";
            if (reader.GetSchemaTable().DefaultView.Count.Equals(1))
            {
                if (!Convert.IsDBNull(reader["LineaProducto"]))
                {
                    producto.LineaProducto = Convert.ToString(reader["LineaProducto"]);
                }
            }

            reader.GetSchemaTable().DefaultView.RowFilter = "ColumnName = 'GarantiaProducto'";
            if (reader.GetSchemaTable().DefaultView.Count.Equals(1))
            {
                if (!Convert.IsDBNull(reader["GarantiaProducto"]))
                {
                    producto.GarantiaProducto = Convert.ToString(reader["GarantiaProducto"]);
                }
            }

            reader.GetSchemaTable().DefaultView.RowFilter = "ColumnName = 'Precio'";
            if (reader.GetSchemaTable().DefaultView.Count.Equals(1))
            {
                if (!Convert.IsDBNull(reader["Precio"]))
                {
                    producto.Precio = Convert.ToDecimal(reader["Precio"]);
                }
            }

            reader.GetSchemaTable().DefaultView.RowFilter = "ColumnName = 'Imagen'";
            if (reader.GetSchemaTable().DefaultView.Count.Equals(1))
            {
                if (!Convert.IsDBNull(reader["Imagen"]))
                {
                    Debug.WriteLine("OBJECT IMAGEN");
                    Debug.WriteLine(reader["Imagen"]);

                    var imgBytes = (byte[])reader["Imagen"];// casteo a array de bytes 

                    if (imgBytes != null && imgBytes.Length > 0)
                    {
                        producto.ImagenTxt = "data:image;base64," + Convert.ToBase64String(imgBytes);
                        Debug.WriteLine("Img Bytes entro al IF");
                        Debug.WriteLine(producto.ImagenTxt);
                    }
                    //       producto.ImagenTxt = Convert.ToBase64String(imgBytes);// casteo a base64 str
                }
            }

            reader.GetSchemaTable().DefaultView.RowFilter = "ColumnName = 'DescripcionTecnica'";
            if (reader.GetSchemaTable().DefaultView.Count.Equals(1))
            {
                if (!Convert.IsDBNull(reader["DescripcionTecnica"]))
                {
                    producto.DescripcionTecnica = Convert.ToString(reader["DescripcionTecnica"]);
                }
            }

            return producto;
        }

        public List<Producto> ListarProductos()
        {
            List<Producto> ListaEntidad = new List<Producto>();
            Producto entidad = null;
            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnSql"]].ConnectionString))
            {
                using (SqlCommand comando = new SqlCommand("paListarProductos", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    conexion.Open();
                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        entidad = LlenarEntidad(reader);
                        ListaEntidad.Add(entidad);
                    }
                }
                conexion.Close();
            }
            return ListaEntidad;
        }

        public Producto InsertarProducto(Producto producto)
        {
            byte[] bytes = Convert.FromBase64String(producto.ImagenTxt);
            producto.Imagen = bytes;
            Debug.WriteLine("Tamaño Array ");
            Debug.WriteLine(bytes.Length);
            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnSql"]].ConnectionString))
            {
                using (SqlCommand comando = new SqlCommand("paProducto_insertar", conexion))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@pIdCategoria", producto.IdCategoria);
                    comando.Parameters.AddWithValue("@pNomProducto", producto.NomProducto);
                    comando.Parameters.AddWithValue("@pMarcaProducto", producto.MarcaProducto);
                    comando.Parameters.AddWithValue("@pModeloProducto", producto.ModeloProducto);
                    comando.Parameters.AddWithValue("@pLineaProducto", producto.LineaProducto);
                    comando.Parameters.AddWithValue("@pGarantiaProducto", producto.GarantiaProducto);
                    comando.Parameters.AddWithValue("@pPrecio", producto.Precio);
                    comando.Parameters.AddWithValue("@pImagen", producto.Imagen);
                    comando.Parameters.AddWithValue("@pDescripcion", producto.DescripcionTecnica);

                    conexion.Open();
                    producto.IdProducto = Convert.ToInt32(comando.ExecuteScalar());
                    conexion.Close();
                }
            }
            return producto;
        }

        public Producto ModificarProducto(int id, Producto producto)
        {
            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnSql"]].ConnectionString))
            {

                using (SqlCommand comando = new SqlCommand("paProducto_Modificar", conexion))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@pIdProducto", id);
                    comando.Parameters.AddWithValue("@pIdCategoria", producto.IdCategoria);
                    comando.Parameters.AddWithValue("@pNomProducto", producto.NomProducto);
                    comando.Parameters.AddWithValue("@pMarcaProducto", producto.MarcaProducto);
                    comando.Parameters.AddWithValue("@pModeloProducto", producto.ModeloProducto);
                    comando.Parameters.AddWithValue("@pLineaProducto", producto.LineaProducto);
                    comando.Parameters.AddWithValue("@pGarantiaProducto", producto.GarantiaProducto);
                    comando.Parameters.AddWithValue("@pPrecio", producto.Precio);
                    //comando.Parameters.AddWithValue("@pImagen", producto.Imagen);
                    comando.Parameters.AddWithValue("@pDescripcion", producto.DescripcionTecnica);
                    conexion.Open();
                    SqlDataReader reader = comando.ExecuteReader();
                    
                    conexion.Close();
                }
            }
            return producto;
        }

        public Producto BuscaProductoId(int pProductoId)
        {
            Producto entidad = null;
            try
            {

                using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnSql"]].ConnectionString))
                {
                    using (SqlCommand comando = new SqlCommand("paProducto_BuscaProductoId", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@pIdProducto", pProductoId);
                        conexion.Open();
                        SqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            entidad = LlenarEntidad(reader);
                        }
                        conexion.Close();
                    }
                }
                return entidad;
            }
            catch (Exception ex)
            {
                string innerException = (ex.InnerException == null) ? "" : ex.InnerException.ToString();
                //Logger.paginaNombre = this.GetType().Name;
                //Logger.Escribir("Error en Logica de Negocio: " + ex.Message + ". " + ex.StackTrace + ". " + innerException);
                return entidad;
            }
        }

        public void EliminarProducto(int id)
        {
            int returnedVal = 0;
            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnSql"]].ConnectionString))
            {
                using (SqlCommand comando = new SqlCommand("paProducto_Eliminar", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@pIdProducto", id);
                    conexion.Open();
                    //comando.ExecuteNonQuery;
                    returnedVal = Convert.ToInt32(comando.ExecuteScalar());
                    conexion.Close();
                }
            }
        }
    }
}
