using ABB.Catalogo.Entidades.Core;
using ABB.Catalogo.LogicaNegocio.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
//using System.Web.Http;

namespace ABB.Catalogo.ClienteWeb.Controllers
{
    public class UsuarioController : Controller
    {
        string RutaApi = "http://localhost:51335/api/"; //define la ruta del web api
        string jsonMediaType = "application/json"; // define el tipo de dat

        // GET: Usuarios
        public ActionResult Index()
        {
            string controladora = "Usuario";
            string metodo = "Get";
            List<Usuario> listausuarios = new List<Usuario>();

            using (WebClient usuario = new WebClient())
            {
                usuario.Headers.Clear();//borra datos anteriores
                usuario.Headers[HttpRequestHeader.ContentType] = jsonMediaType;    //establece el tipo de dato de tranferencia
                usuario.Encoding = UTF8Encoding.UTF8; //typo de decodificador reconocimiento carecteres especiales
                string rutacompleta = RutaApi + controladora;
                var data = usuario.DownloadString(new Uri(rutacompleta)); //ejecuta la busqueda en la web api usando metodo GET
                listausuarios = JsonConvert.DeserializeObject<List<Usuario>>(data); // convierte los datos traidos por la api a tipo lista de usuarios
            }

            return View(listausuarios);
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            Usuario usuario = new Usuario();// se crea uns instancia de la clase usuario

            List<Rol> listaRol = new List<Rol>();
            listaRol = new RolLN().ListaRol();
            listaRol.Add(new Rol() { IdRol = 0, DesRol = "Selecciona rol" });
            ViewBag.listaRoles = listaRol;

            return View(usuario);
        }

        // POST: Usuarios/Create
        [HttpPost]
        public ActionResult Create(Usuario collection)
        {
            string controladora = "Usuario";
            try
            {
                // TODO: Add insert logic here
                using (HttpClient usuario = new HttpClient())
                {
                    usuario.BaseAddress = new Uri(RutaApi + controladora);

                    var postTask = usuario.PostAsJsonAsync<Usuario>("usuario", collection);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    #region
                    /* usuario.Headers.Clear();//borra datos anteriores
                     usuario.Headers[HttpRequestHeader.ContentType] = jsonMediaType;  //establece el tipo de dato de tranferencia
                     usuario.Encoding = UTF8Encoding.UTF8; //typo de decodificador reconocimiento carecteres especiales
                     var usuarioJson = JsonConvert.SerializeObject(collection); //convierte el objeto de tipo Usuarios a una trama Json
                     string rutacompleta = RutaApi + controladora;
                     var resultado = usuario.UploadString(new Uri(rutacompleta), usuarioJson);*/
                    #endregion
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        
        public ActionResult Edit(int id)
        {
            UsuarioController uc = new UsuarioController();
            string controladora = "Usuario";
            //string metodo = "GetUserId";
            Usuario users = new Usuario();
            using (WebClient usuario = new WebClient())
            {
                usuario.Headers.Clear();//borra datos anteriores
                usuario.Headers[HttpRequestHeader.ContentType] = jsonMediaType; //establece el tipo de dato de tranferencia
                usuario.Encoding = UTF8Encoding.UTF8; //typo de decodificador reconocimiento carecteres especiales

                string rutacompleta = RutaApi + controladora + "?IdUsuario=" + id; //ejecuta la busqueda en la web api usando metodo GET
                var data = usuario.DownloadString(new Uri(rutacompleta));

                users = JsonConvert.DeserializeObject<Usuario>(data); // convierte los datos traidos por la api a tipo lista de usuarios
                
                List<Rol> listaRol = new List<Rol>();
                listaRol = new RolLN().ListaRol();
                listaRol.Add(new Rol() { IdRol = 0, DesRol = "Selecciona rol" });
                ViewBag.listaRoles = listaRol;
            }

            return View(users);
        }

        //PUT: Usuario/Edit/id
        [HttpPost]
        public ActionResult Edit(int id, Usuario collection)
        {
            string controladora = "Usuario";
            try
            {
                using (WebClient usuario = new WebClient())
                {
                    #region
                    /*usuario.BaseAddress = new Uri(RutaApi + controladora + "/" + id);
                    collection.ClaveTxt = "";
                    var putTask = usuario.PuttAsJsonAsync<Usuario>("usuario", collection);
                    putTask.Wait();
                    
                    var result = putTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }*/
                    #endregion

                    usuario.Headers.Clear();//borra datos anteriores

                    usuario.Headers[HttpRequestHeader.ContentType] = jsonMediaType; //establece el tipo de dato de tranferencia
                    usuario.Encoding = UTF8Encoding.UTF8; //typo de decodificador reconocimiento carecteres especiales
                    //collection.ClaveTxt = "";
                    var usuarioJson = JsonConvert.SerializeObject(collection); //convierte el objeto de tipo Usuarios a una trama Json
                    string rutacompleta = RutaApi + controladora +"/" + id;
                    var resultado = usuario.UploadString(new Uri(rutacompleta), "PUT", usuarioJson);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                List<Rol> listaRol = new List<Rol>();
                listaRol = new RolLN().ListaRol();
                listaRol.Add(new Rol() { IdRol = 0, DesRol = "Selecciona rol" });
                ViewBag.listaRoles = listaRol;
                return View();
            }
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int id)
        {
            UsuarioController uc = new UsuarioController();
            string controladora = "Usuario";
            //string metodo = "GetUserId";
            Usuario users = new Usuario();
            using (HttpClient usuario = new HttpClient())
            {
                usuario.BaseAddress = new Uri(RutaApi + controladora + "/" + id);

                //HTTP DELETE
                var deleteTask = usuario.DeleteAsync(id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
                return View();
        }

        // POST: Usuarios/Delete/5
        /* [HttpPost]
        public ActionResult Delete(Usuario collection)
         {
             try
             {
                 return RedirectToAction("Index");
             }
             catch
             {
                 return View();
             }
         }*/
        #region
        public ActionResult CambioClave(int id)
        {
            UsuarioController uc = new UsuarioController();
            using (WebClient usuario = new WebClient())
            {
                usuario.Headers.Clear();//borra datos anteriores
                usuario.Headers[HttpRequestHeader.ContentType] = jsonMediaType; //establece el tipo de dato de tranferencia
                usuario.Encoding = UTF8Encoding.UTF8; //typo de decodificador reconocimiento carecteres especiales
            }
            return View();
        }

        //PUT: Usuario/Edit/id
        [HttpPost]
        public ActionResult CambioClave(int id, Clave collection)
        {
            string controladora = "Usuario";
            string metodo = "CambioClave";
            try
            {
                using (WebClient clave = new WebClient())
                {
                    clave.Headers.Clear();//borra datos anteriores

                    clave.Headers[HttpRequestHeader.ContentType] = jsonMediaType; //establece el tipo de dato de tranferencia
                    clave.Encoding = UTF8Encoding.UTF8; //typo de decodificador reconocimiento carecteres especiales
                    //collection.ClaveTxt = "";
                    var claveJson = JsonConvert.SerializeObject(collection); //convierte el objeto de tipo Usuarios a una trama Json
                    string rutacompleta = RutaApi + controladora + "/" + id + "?pOldPass=" + collection.oldPass + "?pNewPass=" + collection.newPass;
                    var resultado = clave.UploadString(new Uri(rutacompleta), metodo, claveJson);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        #endregion
    }
}