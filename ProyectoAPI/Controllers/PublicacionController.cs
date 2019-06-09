using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Description;
using Newtonsoft.Json;
using ProyectoAPI.Models;

namespace ProyectoAPI.Controllers
{
    public class PublicacionController : ApiController
    {
        private todaviasirveDBEntities db = new todaviasirveDBEntities();

        // GET: api/Publicacion
        public IQueryable<Publicacion> GetPublicacion()
        {
            return db.Publicacion;
        }

        // GET: api/Publicacion/5
        [ResponseType(typeof(Publicacion))]
        public IHttpActionResult GetPublicacion(int id)
        {
            Publicacion publicacion = db.Publicacion.Find(id);
            if (publicacion == null)
            {
                return NotFound();
            }

            return Ok(publicacion);
        }

        // PUT: api/Publicacion/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPublicacion(int id, Publicacion publicacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != publicacion.Id)
            {
                return BadRequest();
            }

            db.Entry(publicacion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PublicacionExists(id))
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

        // POST: api/Publicacion
        [ResponseType(typeof(Publicacion))]
        public IHttpActionResult PostPublicacion(Publicacion publicacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Publicacion.Add(publicacion);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = publicacion.Id }, publicacion);
        }

        // DELETE: api/Publicacion/5
        [ResponseType(typeof(Publicacion))]
        public IHttpActionResult DeletePublicacion(int id)
        {
            Publicacion publicacion = db.Publicacion.Find(id);
            if (publicacion == null)
            {
                return NotFound();
            }

            db.Publicacion.Remove(publicacion);
            db.SaveChanges();

            return Ok(publicacion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PublicacionExists(int id)
        {
            return db.Publicacion.Count(e => e.Id == id) > 0;
        }


        // METODO BUSCAR
        [HttpGet]
        [Route("Api/Publicacion/Buscar/{nombre}")]
        public HttpResponseMessage Buscar(string nombre)
        {
            try
            {
                List<Publicacion> publicacion = (from publi in db.Publicacion
                                                 where publi.titulo.Contains(nombre)
                                                 select publi).ToList();

                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(publicacion));
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return response;
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
    } // cierre clase
} // cierre controller