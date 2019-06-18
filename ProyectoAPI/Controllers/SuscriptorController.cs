using ProyectoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoAPI.Controllers
{
    public class SuscriptorController : Controller
    {

        private todaviasirveDBEntities db = new todaviasirveDBEntities();

        // GET: Suscriptor
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Suscriptor suscr)
        {
            if (ModelState.IsValid)
            {
                db.Suscriptor.Add(suscr);
                db.SaveChanges();
                return RedirectToAction("Publicacion");
            }

            return View(suscr);
        }
    }

    
}