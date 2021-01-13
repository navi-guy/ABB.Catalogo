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
    [Authorize]
    public class ProductosController : ApiController
    {
        // GET: api/Productos
        public IEnumerable<Producto> Get()
        {
            List<Producto> productos = new List<Producto>();
            productos = new ProductoLN().ListarProductos();
            return productos;
        }

        // GET: api/Productos/{idProducto}
        public IHttpActionResult GetUserId([FromUri] int IdProducto)
        {

            if (IdProducto <= 0)
            {
                return BadRequest("el Id debe ser mayor que 0");
            }
            try
            {
                Producto prod = new Producto();
                ProductoLN producto = new ProductoLN();
                prod = producto.BuscaProductoId(IdProducto);
                return Ok(prod);
            }
            catch (Exception ex)
            {
                string innerException = (ex.InnerException == null) ? "" : ex.InnerException.ToString();
                //Logger.paginaNombre = this.GetType().Name;
                //Logger.Escribir("Error en Logica de Negocio: " + ex.Message + ". " + ex.StackTrace + ". " + innerException);
                throw;
            }
        }

        // POST: api/Productos
        public IHttpActionResult Post([FromBody]Producto value)
        {
            if (value.IdCategoria <= 0)
            {
                return BadRequest("IdCategoria es nulo");
            }
            if (value.NomProducto == null)
            {
                return BadRequest("NomProducto es nulo");
            }
            if (value.MarcaProducto == null)
            {
                return BadRequest("MarcaProducto es nulo");
            }
            if (value.ModeloProducto == null)
            {
                return BadRequest("ModeloProducto es nulo");
            }
            if (value.LineaProducto == null)
            {
                return BadRequest("LineaProducto es nulo");
            }
            Producto producto = new ProductoLN().InsertarProducto(value);
            return Ok(producto);
        }

        // PUT: api/Productos/5
        public IHttpActionResult Put(int id, [FromBody]Producto value)
        {
            if (id <= 0)
            {
                return BadRequest("IdProducto es nulo");
            }
            Producto producto = new Producto();
            producto = new ProductoLN().ModificarProducto(id, value);
            return Ok(producto);
        }

        // DELETE: api/Productos/5
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("IdProducto es nulo");
            }
            new ProductoLN().EliminarProducto(id);
            return Ok();
        }

    }
}
