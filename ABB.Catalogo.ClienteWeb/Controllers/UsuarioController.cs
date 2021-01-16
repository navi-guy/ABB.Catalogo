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
using System.Configuration;

namespace ABB.Catalogo.ClienteWeb.Controllers
{
    public class UsuarioController : Controller
    {
        string RutaApi = "http://localhost:50892/Api/"; //define la ruta del web api
        string jsonMediaType = "application/json"; // define el tipo de dat
        
        // GET: Usuarios
        public ActionResult Index()
        {
            string controladora = "Usuarios";
          //  string metodo = "Get";
            TokenResponse tokenrsp = new TokenResponse();
            //llamada al web Api de Autorizacion.
            tokenrsp = Respuest();
            //llamada al Web Api para listar Usuarios.


            List<Usuario> listausuarios = new List<Usuario>();
            using (WebClient usuario = new WebClient())
            {
                usuario.Headers.Clear();//borra datos anteriores
                //establece el token de autorizacion en la cabecera
                usuario.Headers[HttpRequestHeader.Authorization] = "Bearer " + tokenrsp.Token;
                //establece el tipo de dato de tranferencia
                usuario.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                //typo de decodificador reconocimiento carecteres especiales
                usuario.Encoding = UTF8Encoding.UTF8;
                string rutacompleta = RutaApi + controladora;
                //ejecuta la busqueda en la web api usando metodo GET
                var data = usuario.DownloadString(new Uri(rutacompleta));
                // convierte los datos traidos por la api a tipo lista de usuarios
                listausuarios = JsonConvert.DeserializeObject<List<Usuario>>(data);
            }

            return View(listausuarios);
        }


        // GET: Usuario/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            Usuario usuario = new Usuario();// se crea uns instancia de la clase usuario

            List<Rol> listarol = new List<Rol>();
            listarol = new RolLN().ListaRol();
            listarol.Add(new Rol() { IdRol = 0, DesRol = "[Seleccione Rol...]" });
            ViewBag.listaRoles = listarol;

            return View(usuario);
        }


        // POST: Usuario/Create
        [HttpPost]
        public ActionResult Create(Usuario collection)
        {
            string controladora = "Usuarios";
            try
            {
                TokenResponse tokenrsp = new TokenResponse();
                //llamada al web Api de Autorizacion.
                tokenrsp = Respuest();

                // TODO: Add insert logic here
                using (WebClient usuario = new WebClient())
                {
                    usuario.Headers.Clear();//borra datos anteriores
                    //establece el token de autorizacion en la cabecera
                    usuario.Headers[HttpRequestHeader.Authorization] = "Bearer " + tokenrsp.Token;
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
            catch
            {
                return View();
            }
        }

        // GET: Usuario/Edit/5
        public ActionResult Edit(int id)
        {
            string controladora = "Usuarios";
            // string metodo = "GetUserId";
            Usuario users = new Usuario();
            TokenResponse tokenrsp = new TokenResponse();
            //llamada al web Api de Autorizacion.
            tokenrsp = Respuest();
            using (WebClient usuario = new WebClient())
            {
                usuario.Headers.Clear();//borra datos anteriores

                usuario.Headers[HttpRequestHeader.Authorization] = "Bearer " + tokenrsp.Token;
                //establece el tipo de dato de tranferencia
                usuario.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                //typo de decodificador reconocimiento carecteres especiales
                usuario.Encoding = UTF8Encoding.UTF8;
                string rutacompleta = RutaApi + controladora + "?IdUsuario=" + id;
                //ejecuta la busqueda en la web api usando metodo GET
                var data = usuario.DownloadString(new Uri(rutacompleta));

                // convierte los datos traidos por la api a tipo lista de usuarios
                users = JsonConvert.DeserializeObject<Usuario>(data);
            }

            List<Rol> listarol = new List<Rol>();
            listarol = new RolLN().ListaRol();
            listarol.Add(new Rol() { IdRol = 0, DesRol = "[Seleccione Rol...]" });
            ViewBag.listaRoles = listarol;

            return View(users);

        }

        // POST: Usuario/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Usuario collection)
        {
            string controladora = "Usuarios";
            TokenResponse tokenrsp = new TokenResponse();
            //llamada al web Api de Autorizacion.
            tokenrsp = Respuest();
            //llamada al Web Api para listar Usuarios.
            try
            {
                using (WebClient usuario = new WebClient())
                {
                    usuario.Headers.Clear();//borra datos anteriores
                    usuario.Headers[HttpRequestHeader.Authorization] = "Bearer " + tokenrsp.Token;
                    usuario.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                    usuario.Encoding = UTF8Encoding.UTF8;
                    var usuarioJson = JsonConvert.SerializeObject(collection);
                    string rutacompleta = RutaApi + controladora + "/"+id;
                    var resultado = usuario.UploadString(new Uri(rutacompleta), "PUT", usuarioJson);
                    }
                return RedirectToAction("Index");
            }
            catch
            {
                // var usuarioJson = JsonConvert.SerializeObject(collection);
                // return "DATA: "+ usuarioJson + "Error: " + e;
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

        }

        // GET: Usuario/Delete/5
        public ActionResult Delete(int id)
        {
            string controladora = "Usuarios";
            Usuario users = new Usuario();

            TokenResponse tokenrsp = new TokenResponse();
            //llamada al web Api de Autorizacion.
            tokenrsp = Respuest();
            //llamada al Web Api para listar Usuarios.

            using (WebClient usuario = new WebClient())
            {
                usuario.Headers.Clear();//borra datos anteriores
                usuario.Headers[HttpRequestHeader.Authorization] = "Bearer " + tokenrsp.Token;
                //establece el tipo de dato de tranferencia
                usuario.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                //typo de decodificador reconocimiento carecteres especiales
                usuario.Encoding = UTF8Encoding.UTF8;
                string rutacompleta = RutaApi + controladora + "?IdUsuario=" + id;
                //ejecuta la busqueda en la web api usando metodo GET
                var data = usuario.DownloadString(new Uri(rutacompleta));

                // convierte los datos traidos por la api a tipo lista de usuarios
                users = JsonConvert.DeserializeObject<Usuario>(data);
            }

            return View(users);
        }

        // POST: Usuario/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        { 
            string controladora = "Usuarios";
            TokenResponse tokenrsp = new TokenResponse();
            //llamada al web Api de Autorizacion.
            tokenrsp = Respuest();
            //llamada al Web Api para listar Usuarios.

            try
            {
                using (WebClient usuario = new WebClient())
                {
                    usuario.Headers.Clear();//borra datos anteriores
                    usuario.Headers[HttpRequestHeader.Authorization] = "Bearer " + tokenrsp.Token;
                    usuario.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                    usuario.Encoding = UTF8Encoding.UTF8;
                    var usuarioJson = JsonConvert.SerializeObject(collection);
                    string rutacompleta = RutaApi + controladora + "/" + id;
                    var resultado = usuario.UploadString(new Uri(rutacompleta), "DELETE", usuarioJson);
                }
                    return RedirectToAction("Index");
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        private TokenResponse Respuest()
        {
            TokenResponse respuesta = new TokenResponse();
            string controladora = "Auth";
           // string metodo = "Post";
            var resultado = "";
            UsuariosApi usuapi = new UsuariosApi();
            usuapi.Codigo = Convert.ToInt32(ConfigurationManager.AppSettings["UsuApiCodigo"]);
            usuapi.UserName = ConfigurationManager.AppSettings["UsuApiUserName"];
            usuapi.Clave = ConfigurationManager.AppSettings["UsuApiClave"];
            usuapi.Nombre = ConfigurationManager.AppSettings["UsuApiNombre"];
            usuapi.Rol = ConfigurationManager.AppSettings["UsuApiRol"];
            using (WebClient usuarioapi = new WebClient())
            {
                usuarioapi.Headers.Clear();//borra datos anteriores
                //establece el tipo de dato de tranferencia
                usuarioapi.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                //typo de decodificador reconocimiento carecteres especiales
                usuarioapi.Encoding = UTF8Encoding.UTF8;
                //convierte el objeto de tipo Usuarios a una trama Json
                var usuarioJson = JsonConvert.SerializeObject(usuapi);
                string rutacompleta = RutaApi + controladora;
                resultado = usuarioapi.UploadString(new Uri(rutacompleta), usuarioJson);
                respuesta = JsonConvert.DeserializeObject<TokenResponse>(resultado);
            }
            return respuesta;
        }


    }
}
