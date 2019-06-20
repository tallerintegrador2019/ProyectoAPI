using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoAPI.Models;

namespace ProyectoAPI.Controllers
{
    public class UsuarioABMController : Controller
    {
        private todaviasirveDBEntities db = new todaviasirveDBEntities();

        // GET: UsuarioABM
        public ActionResult Index()
        {
            var usuario = db.Usuario.Include(u => u.Rango);
            return View(usuario.ToList());
        }

        // GET: UsuarioABM/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: UsuarioABM/Create
        public ActionResult Create()
        {
            ViewBag.idRango = new SelectList(db.Rango, "id", "descripcion");
            return View();
        }

        // POST: UsuarioABM/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,username,pass,nombre,apellido,email,imagen,idRango")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {

                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];

                    if (file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var imagenlocal = Path.Combine( Server.MapPath("~/Content/Images"), fileName );
                        file.SaveAs(imagenlocal);
                        usuario.imagen = fileName;
                    }
                }

                db.Usuario.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idRango = new SelectList(db.Rango, "id", "descripcion", usuario.idRango);
            return View(usuario);
        }

        // GET: UsuarioABM/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.idRango = new SelectList(db.Rango, "id", "descripcion", usuario.idRango);
            return View(usuario);
        }

        // POST: UsuarioABM/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,username,pass,nombre,apellido,email,imagen,idRango")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idRango = new SelectList(db.Rango, "id", "descripcion", usuario.idRango);
            return View(usuario);
        }

        // GET: UsuarioABM/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: UsuarioABM/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuario.Find(id);
            db.Usuario.Remove(usuario);
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
