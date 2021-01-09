using ABB.Catalogo.Entidades.Core;
using ABB.Catalogo.LogicaNegocio.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ABB.Catalogo.ClienteWeb.Controllers
{
    public class ProductoController : Controller
    {
        string RutaApi = "http://localhost:51335/api/"; //define la ruta del web api
        string jsonMediaType = "application/json"; // define el tipo de dat

        // GET: Producto
        public ActionResult Index()
        {
            string controladora = "Producto";
            string metodo = "Get";

            List<Producto> listaProductos = new List<Producto>();

            using (WebClient producto = new WebClient())
            {
                producto.Headers.Clear();//borra datos anteriores
                producto.Headers[HttpRequestHeader.ContentType] = jsonMediaType;    //establece el tipo de dato de tranferencia
                producto.Encoding = UTF8Encoding.UTF8; //typo de decodificador reconocimiento carecteres especiales
                string rutacompleta = RutaApi + controladora;
                var data = producto.DownloadString(new Uri(rutacompleta)); //ejecuta la busqueda en la web api usando metodo GET
                listaProductos = JsonConvert.DeserializeObject<List<Producto>>(data); // convierte los datos traidos por la api a tipo lista de usuarios
            }
            return View(listaProductos);
        }

        // GET: Producto/Details/5
        public ActionResult Details(int id)
        {
            UsuarioController uc = new UsuarioController();
            string controladora = "Producto";
            Producto product = new Producto();
            using (WebClient producto = new WebClient())
            {
               producto.Headers.Clear();//borra datos anteriores
                producto.Headers[HttpRequestHeader.ContentType] = jsonMediaType; //establece el tipo de dato de tranferencia
                producto.Encoding = UTF8Encoding.UTF8; //typo de decodificador reconocimiento carecteres especiales

                string rutacompleta = RutaApi + controladora + "?IdProducto=" + id; //ejecuta la busqueda en la web api usando metodo GET
                var data = producto.DownloadString(new Uri(rutacompleta));

                product = JsonConvert.DeserializeObject<Producto>(data); // convierte los datos traidos por la api a tipo lista de usuarios
            }
            return View(product);
        }

        // GET: Producto/Create
        public ActionResult Create()
        {
            Producto producto = new Producto();// se crea uns instancia de la clase producto

            List<Categoria> listaCategoria = new List<Categoria>();
            listaCategoria = new CategoriaLN().ListaCategoria();
            listaCategoria.Add(new Categoria() { IdCategoria = 0, DescCategoria = "Selecciona categoria" });
            ViewBag.listaCategoria = listaCategoria;

            return View(producto);
        }

        // POST: Producto/Create
        [HttpPost]
        public ActionResult Create(Producto collection , HttpPostedFileBase img)
        {
            string controladora = "Producto";
            try
            {
                byte[] imagenData = null;
                using (var imagen = new BinaryReader(img.InputStream))
                {
                    imagenData = imagen.ReadBytes(img.ContentLength);
                }
                string imagenDataTxt = "";
                imagenDataTxt = Convert.ToBase64String(imagenData);
                collection.ImagenTxt = imagenDataTxt;
                // TODO: Add insert logic here
                using (WebClient producto = new WebClient())
                {
                    #region
                    /* producto.BaseAddress = new Uri(RutaApi + controladora);

                     var postTask = producto.PostAsJsonAsync<Producto>("producto", collection);
                     postTask.Wait();

                     var result = postTask.Result;
                     if (result.IsSuccessStatusCode)
                     {
                         return RedirectToAction("Index");
                     }*/
                    #endregion
                    producto.Headers.Clear();//borra datos anteriores
                     producto.Headers[HttpRequestHeader.ContentType] = jsonMediaType;  //establece el tipo de dato de tranferencia
                     producto.Encoding = UTF8Encoding.UTF8; //typo de decodificador reconocimiento carecteres especiales
                    var productoJson = JsonConvert.SerializeObject(collection); //convierte el objeto de tipo Usuarios a una trama Json
                     string rutacompleta = RutaApi + controladora;
                     var resultado = producto.UploadString(new Uri(rutacompleta),"POST", productoJson);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // GET: Producto/Edit/5
        public ActionResult Edit(int id)
        {
            UsuarioController uc = new UsuarioController();
            string controladora = "Producto";
            Producto products = new Producto();
            using (WebClient producto = new WebClient())
            {
                producto.Headers.Clear();//borra datos anteriores
                producto.Headers[HttpRequestHeader.ContentType] = jsonMediaType; //establece el tipo de dato de tranferencia
                producto.Encoding = UTF8Encoding.UTF8; //typo de decodificador reconocimiento carecteres especiales

                string rutacompleta = RutaApi + controladora + "?IdProducto=" + id; //ejecuta la busqueda en la web api usando metodo GET
                var data = producto.DownloadString(new Uri(rutacompleta));

                products = JsonConvert.DeserializeObject<Producto>(data); // convierte los datos traidos por la api a tipo lista de usuarios

                List<Categoria> listaCategoria = new List<Categoria>();
                listaCategoria = new CategoriaLN().ListaCategoria();
                listaCategoria.Add(new Categoria() { IdCategoria = 0, DescCategoria = "Selecciona categoria" });
                ViewBag.listaCategoria = listaCategoria;
            }
            return View(products);
        }

        // POST: Producto/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Producto collection)
        {
            string controladora = "Producto";
            try
            {
                using (WebClient producto = new WebClient())
                {
                    producto.Headers.Clear();//borra datos anteriores

                    producto.Headers[HttpRequestHeader.ContentType] = jsonMediaType; //establece el tipo de dato de tranferencia
                    producto.Encoding = UTF8Encoding.UTF8; //typo de decodificador reconocimiento carecteres especiales
                    var productoJson = JsonConvert.SerializeObject(collection); //convierte el objeto de tipo Usuarios a una trama Json
                    string rutacompleta = RutaApi + controladora + "/" + id;
                    var resultado = producto.UploadString(new Uri(rutacompleta), "PUT", productoJson);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                List<Categoria> listaCategoria = new List<Categoria>();
                listaCategoria = new CategoriaLN().ListaCategoria();
                listaCategoria.Add(new Categoria() { IdCategoria = 0, DescCategoria = "Selecciona categoria" });
                ViewBag.listaCategoria = listaCategoria;
                return View();
            }
        }

        // GET: Producto/Delete/5
        public ActionResult Delete(int id)
        {
            UsuarioController uc = new UsuarioController();
            string controladora = "Producto";
            Producto products = new Producto();
            using (HttpClient producto = new HttpClient())
            {
                producto.BaseAddress = new Uri(RutaApi + controladora + "/" + id);

                //HTTP DELETE
                var deleteTask = producto.DeleteAsync(id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        // POST: Producto/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
