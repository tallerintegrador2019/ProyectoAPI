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
        public ActionResult Login() {
            return View();
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
