using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using ProyectoAPI.Models;

namespace ProyectoAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PasoController : ApiController
    {
        private todaviasirveDBEntities db = new todaviasirveDBEntities();

        // GET: api/Paso
        public IQueryable<Paso> GetPaso()
        {
            return db.Paso;
        }

        // GET: api/Paso/5
        [ResponseType(typeof(Paso))]
        public IHttpActionResult GetPaso(int id)
        {
            Paso paso = db.Paso.Find(id);
            if (paso == null)
            {
                return NotFound();
            }

            return Ok(paso);
        }

        // PUT: api/Paso/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPaso(int id, Paso paso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != paso.id)
            {
                return BadRequest();
            }

            db.Entry(paso).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PasoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Paso
        [ResponseType(typeof(Paso))]
        public async Task<IHttpActionResult> PostPaso()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Paso paso = new Paso();
            var request = HttpContext.Current.Request;

            if (Request.Content.IsMimeMultipartContent())
            {
                string root1 = HttpContext.Current.Server.MapPath("~/Content/Images");
                var provider = new MultipartFormDataStreamProvider(root1);
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);
                // This illustrates how to get the file names.
                foreach (MultipartFileData file in provider.FileData)
                {
                    Trace.WriteLine(file.Headers.ContentDisposition.FileName);
                    Trace.WriteLine("Server file path: " + file.LocalFileName);

                }
                foreach (var key in provider.FormData.AllKeys)
                {
                    if (!key.Equals("__RequestVerificationToken"))
                    {
                        switch (key)
                        {
                            case "numero":
                                paso.numero = Int32.Parse(provider.FormData.GetValues(key)[0]);
                                break;
                            case "descripcion":
                                paso.descripcion = provider.FormData.GetValues(key)[0];
                                break;
                            case "idPublicacion":
                                paso.idPublicacion = Int32.Parse(provider.FormData.GetValues(key)[0]);
                                break;
                            default:
                                break;
                        }

                    }

                }

                if (request.Files.Count > 0)
                {
                    var imagen = request.Files[0];
                    var postedFile = request.Files.Get("file");
                    string root = Path.Combine(HttpContext.Current.Server.MapPath("~/Content/Images"), imagen.FileName);
                    //root = root + "/" + imagen.FileName;
                    imagen.SaveAs(root);
                    paso.imagen = imagen.FileName;
                }

                db.Paso.Add(paso);
                db.SaveChanges();
            }

            return CreatedAtRoute("DefaultApi", new { id = paso.id }, paso);
        }

        // DELETE: api/Paso/5
        [ResponseType(typeof(Paso))]
        public IHttpActionResult DeletePaso(int id)
        {
            Paso paso = db.Paso.Find(id);
            if (paso == null)
            {
                return NotFound();
            }

            db.Paso.Remove(paso);
            db.SaveChanges();

            return Ok(paso);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PasoExists(int id)
        {
            return db.Paso.Count(e => e.id == id) > 0;
        }
    }
}