using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ABB.Catalogo.Entidades.Core;
using ABB.Catalogo.LogicaNegocio.Core;

namespace WebServicesApp.Controllers
{
    public class ProductosController : ApiController
    {
        // GET: api/Productos
        public IEnumerable<Producto> Get()
        {
            List<Producto> productos = new List<Producto>();
            productos = new ProductoLN().ListarProductos();
            return productos;
        }

        // GET: api/Productos/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Productos
        public void Post([FromBody]Producto value)
        {
            Producto usuario = new ProductoLN().InsertarProducto(value);
        }

        // PUT: api/Productos/5
        public Producto Put(int id, [FromBody]Producto value)
        {
            Producto producto = new Producto();
            producto = new ProductoLN().ModificarProducto(id, value);
            return producto;
        }

        // DELETE: api/Productos/5
        public void Delete(int id)
        {
            new ProductoLN().EliminarProducto(id);
        }

        public Producto GetUserId([FromUri] int IdProducto)
        {
            try
            {
                ProductoLN producto = new ProductoLN();
                return producto.BuscaProductoId(IdProducto);
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
