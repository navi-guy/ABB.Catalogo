using ABB.Catalogo.Entidades.Core;
using ABB.Catalogo.LogicaNegocio.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebServicesAbb.Controllers
{
    public class ProductoController : ApiController
    {
        // GET: api/Producto
        [HttpGet]
        public IEnumerable<Producto> Get()
        {
            List<Producto> productos = new List<Producto>();
            productos = new ProductoLN().ListarProductos();

            return productos;
        }

        // GET: api/Producto/5
        public Producto GetProductoId([FromUri] int IdProducto)
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

        // POST: api/Producto
        public void Post([FromBody] Producto value)
        {
            Producto producto = new ProductoLN().InsertarProducto(value);
        }

        // PUT: api/Producto/5
        public Producto Put(int id, [FromBody] Producto value)
        {
            Producto producto = new ProductoLN().ModificarProducto(id, value);
            return producto;
        }

        // DELETE: api/Producto/5
        public void Delete(int id)
        {
            new ProductoLN().EliminarProducto(id);
        }
    }
}
