using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoAPI.Models;

namespace ProyectoAPI.Controllers
{
    public class PasoABMController : Controller
    {
        private todaviasirveDBEntities db = new todaviasirveDBEntities();

        // GET: PasoABM
        public ActionResult Index()
        {
            var paso = db.Paso.Include(p => p.Publicacion);
            return View(paso.ToList());
        }

        // GET: PasoABM/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paso paso = db.Paso.Find(id);
            if (paso == null)
            {
                return HttpNotFound();
            }
            return View(paso);
        }

        // GET: PasoABM/Create
        public ActionResult Create()
        {
            ViewBag.idPublicacion = new SelectList(db.Publicacion, "id", "titulo");
            return View();
        }

        // POST: PasoABM/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,numero,descripcion,imagen,idPublicacion")] Paso paso)
        {
            if (ModelState.IsValid)
            {
                db.Paso.Add(paso);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idPublicacion = new SelectList(db.Publicacion, "id", "titulo", paso.idPublicacion);
            return View(paso);
        }

        // GET: PasoABM/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paso paso = db.Paso.Find(id);
            if (paso == null)
            {
                return HttpNotFound();
            }
            ViewBag.idPublicacion = new SelectList(db.Publicacion, "id", "titulo", paso.idPublicacion);
            return View(paso);
        }

        // POST: PasoABM/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,numero,descripcion,imagen,idPublicacion")] Paso paso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paso).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idPublicacion = new SelectList(db.Publicacion, "id", "titulo", paso.idPublicacion);
            return View(paso);
        }

        // GET: PasoABM/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paso paso = db.Paso.Find(id);
            if (paso == null)
            {
                return HttpNotFound();
            }
            return View(paso);
        }

        // POST: PasoABM/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Paso paso = db.Paso.Find(id);
            db.Paso.Remove(paso);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
