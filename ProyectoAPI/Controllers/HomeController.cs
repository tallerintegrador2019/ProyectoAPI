using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using ProyectoAPI.Models;
using ProyectoAPI.Services;
namespace ProyectoAPI.Controllers
{
    public class HomeController : Controller
    {
        LoginService login = new LoginService();

        public ActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registro(Usuario usu)
        {
            ViewBag.nombreUsuario = usu.nombre;
            Session["usuarioLogueado"] = ViewBag.nombreUsuario;
            login.RegistrarUsuario(usu);
            return RedirectToAction("Login");
        }

        public ActionResult Login() {
             ViewBag.nombreUsuario = Session["usuarioLogueado"];
             return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario usu)
        {
            if (ModelState.IsValid)
            {
                bool usuarioEncontrado = login.verificarDatos(usu);

                if (!usuarioEncontrado)
                {
                    ViewBag.msg = "Usuario y/o Contraseña inválidos.";

                    return View();
                }
                else
                {
                    if (Request.QueryString["redirigir"] != null)
                    {
                        return Redirect(Request.QueryString["redirigir"]);
                    }
                    else
                    {
                        return RedirectToAction("Publicacion", "Home");
                    }
                }

            }

            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Publicacion", "Home");
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        public ActionResult Home()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        public ActionResult Publicacion()
        {
            PublicacionService prod = new PublicacionService();
            List<Publicacion> valor = prod.ObtenerPublicaciones();
            ViewBag.publicaciones = valor;
            return View(valor);
        }
    }
}
