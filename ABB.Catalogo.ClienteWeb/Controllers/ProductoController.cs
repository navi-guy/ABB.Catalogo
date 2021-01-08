using ABB.Catalogo.Entidades.Core;
using ABB.Catalogo.LogicaNegocio.Core;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ABB.Catalogo.ClienteWeb.Controllers
{
    public class ProductoController : Controller
    {
        string RutaApi = "http://localhost:50892/Api/"; //define la ruta del web api
        string jsonMediaType = "application/json"; // define el tipo de dat

        // GET: Producto
        public ActionResult Index()
        {
            string controladora = "Productos";
            // string metodo = "Get";

            List<Producto> listaproductos = new List<Producto>();
            using (WebClient usuario = new WebClient())
            {
                usuario.Headers.Clear();//borra datos anteriores
                //establece el tipo de dato de tranferencia
                usuario.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                //typo de decodificador reconocimiento carecteres especiales
                usuario.Encoding = UTF8Encoding.UTF8;
                string rutacompleta = RutaApi + controladora;
                //ejecuta la busqueda en la web api usando metodo GET
                var data = usuario.DownloadString(new Uri(rutacompleta));
                // convierte los datos traidos por la api a tipo lista de usuarios
                listaproductos = JsonConvert.DeserializeObject<List<Producto>>(data);
            }

            return View(listaproductos);
        }

        // GET: Producto/Details/5
        public ActionResult Details(int id)
        {
            string controladora = "Productos";

            Producto productos = new Producto();
            using (WebClient producto = new WebClient())
            {
                producto.Headers.Clear();//borra datos anteriores
                //establece el tipo de dato de tranferencia
                producto.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                //typo de decodificador reconocimiento carecteres especiales
                producto.Encoding = UTF8Encoding.UTF8;
                string rutacompleta = RutaApi + controladora + "?IdProducto=" + id;
                //ejecuta la busqueda en la web api usando metodo GET
                var data = producto.DownloadString(new Uri(rutacompleta));

                // convierte los datos traidos por la api a tipo lista de usuarios
                productos = JsonConvert.DeserializeObject<Producto>(data);
            }
            return View(productos);
        }

        // GET: Producto/Create
        public ActionResult Create()
        {
            Producto producto = new Producto();// se crea uns instancia de la clase usuario
            List<Categoria> listaCategorias = new List<Categoria>();
            listaCategorias = new CategoriaLN().ListaCategoria();
            listaCategorias.Add(new Categoria() { IdCategoria = 0, DescCategoria = "[Seleccione Categoria...]" });
            ViewBag.listaRoles = listaCategorias;

            return View(producto);
        }

        // POST: Producto/Create
        [HttpPost]
        public ActionResult Create(Producto collection, HttpPostedFileBase upload)
        {
            string controladora = "Productos";
            try
            {
                byte[] imagenData = null;
                using (var imagen = new BinaryReader(upload.InputStream))
                {
                    imagenData = imagen.ReadBytes(upload.ContentLength);
                }
                string imagenDataTxt= "";
                imagenDataTxt = Convert.ToBase64String(imagenData);
                collection.ImagenTxt = imagenDataTxt;

                using (WebClient usuario = new WebClient())
                {
                    usuario.Headers.Clear();//borra datos anteriores
                    //establece el tipo de dato de tranferencia
                    usuario.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                    //typo de decodificador reconocimiento carecteres especiales
                    usuario.Encoding = UTF8Encoding.UTF8;
                    //convierte el objeto de tipo Usuarios a una trama Json
                    var usuarioJson = JsonConvert.SerializeObject(collection);
                    string rutacompleta = RutaApi + controladora;
                    var resultado = usuario.UploadString(new Uri(rutacompleta), usuarioJson);
                }

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                // var usuarioJson = JsonConvert.SerializeObject(collection);
                // return "DATA: "+ usuarioJson + "Error: " + e;
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // GET: Producto/Edit/5
        public ActionResult Edit(int id)
        {   string controladora = "Productos";
            // string metodo = "GetUserId";
            Producto productos = new Producto();
            using (WebClient producto = new WebClient())
            {
                producto.Headers.Clear();//borra datos anteriores
                //establece el tipo de dato de tranferencia
                producto.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                //typo de decodificador reconocimiento carecteres especiales
                producto.Encoding = UTF8Encoding.UTF8;
                string rutacompleta = RutaApi + controladora + "?IdProducto=" + id;
                //ejecuta la busqueda en la web api usando metodo GET
                var data = producto.DownloadString(new Uri(rutacompleta));

                // convierte los datos traidos por la api a tipo lista de usuarios
                productos = JsonConvert.DeserializeObject<Producto>(data);
            }

            List<Categoria> listaCategorias = new List<Categoria>();
            listaCategorias = new CategoriaLN().ListaCategoria();
            listaCategorias.Add(new Categoria() { IdCategoria = 0, DescCategoria = "[Seleccione Categoria...]" });
            ViewBag.listaCategorias = listaCategorias;

            return View(productos);
        }

        // POST: Producto/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Producto collection,HttpPostedFileBase upload)
        {
            string controladora = "Productos";
            try
            {
                byte[] imagenData = null;
                using (var imagen = new BinaryReader(upload.InputStream))
                {
                    imagenData = imagen.ReadBytes(upload.ContentLength);
                }
                string imagenDataTxt = "";
                imagenDataTxt = Convert.ToBase64String(imagenData);
                collection.ImagenTxt = imagenDataTxt;

                using (WebClient producto = new WebClient())
                {
                    producto.Headers.Clear();//borra datos anteriores
                    producto.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                    producto.Encoding = UTF8Encoding.UTF8;
                    var usuarioJson = JsonConvert.SerializeObject(collection);
                    string rutacompleta = RutaApi + controladora + "/" + id;
                    var resultado = producto.UploadString(new Uri(rutacompleta), "PUT", usuarioJson);
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                // var usuarioJson = JsonConvert.SerializeObject(collection);
                // return "DATA: "+ usuarioJson + "Error: " + e;
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // POST: Producto/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            string controladora = "Productos";
            try
            {
                using (WebClient producto = new WebClient())
                {
                    producto.Headers.Clear();//borra datos anteriores
                    producto.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                    producto.Encoding = UTF8Encoding.UTF8;
                    var productoJson = JsonConvert.SerializeObject(collection);
                    string rutacompleta = RutaApi + controladora + "/" + id;
                    var resultado = producto.UploadString(new Uri(rutacompleta), "DELETE", productoJson);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }
    }
}
