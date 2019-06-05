using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using ProyectoAPI.Services;
namespace ProyectoAPI.Controllers
{
    public class HomeController : Controller
    {
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
            ProductoService prod = new ProductoService();
            var valor  =  prod.ObtenerProductoPorNombre();
            //HttpClient client = new HttpClient();
            ViewBag.Title1 = "titulo";
            ViewBag.Title = valor;
            if (valor.Equals("tres")) {
                var variable = "es TRes";
            }

            return View();
        }
    }
}
