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
using ProyectoAPI.Service;

namespace ProyectoAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PublicacionController : ApiController
    {
        private todaviasirveDBEntities db = new todaviasirveDBEntities();
        public PublicacionService service = new PublicacionService();
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

            if (id != publicacion.id)
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
        public async Task<IHttpActionResult> PostPublicacion()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Publicacion publi = new Publicacion();
            Publicacion_Usuario publiUsu = new Publicacion_Usuario();
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
                            case "titulo":
                                publi.titulo = provider.FormData.GetValues(key)[0];
                                break;
                            case "subtitulo":
                                publi.subtitulo = provider.FormData.GetValues(key)[0];
                                break;
                            case "descripcion":
                                publi.descripcion = provider.FormData.GetValues(key)[0];
                                break;
                            case "fechaSubida":
                                publi.fechaSubida = provider.FormData.GetValues(key)[0];
                                break;
                            case "usuarioPublicacion":
                                var valor = provider.FormData.GetValues(key)[0];
                                 publiUsu.idUsuario = Convert.ToInt32(valor); 
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
                    publi.imagenPortada = imagen.FileName;
                }

                db.Publicacion.Add(publi);
                db.SaveChanges();

                publiUsu.idPublicacion = publi.id;
                publiUsu.fecha = new DateTime().ToString();
                db.Publicacion_Usuario.Add(publiUsu);
                db.SaveChanges();
                //return Ok(HttpStatusCode.OK);
                return Content(HttpStatusCode.OK, publi.id);

            }

            return CreatedAtRoute("DefaultApi", new { id = publi.id }, publi);
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
            return db.Publicacion.Count(e => e.id == id) > 0;
        }

        // metodo buscar
        [HttpGet]
        [ResponseType(typeof(Publicacion))]
        [Route("Api/Publicacion/Buscar/{nombre}")]
        public IHttpActionResult Buscar(string nombre)
        {
            List<Publicacion> publicacion = (from publi in db.Publicacion
                                             where publi.titulo.Contains(nombre)
                                             select publi).ToList();

            if (publicacion == null)
            {
                return NotFound();
            }

            return Ok(publicacion);
        }

        // metodo Obtener publicaciones de un usuario
        [HttpGet]
        [ResponseType(typeof(Publicacion))]
        [Route("Api/Publicacion/PublicacionesUsuario/{idUsuario}")]
        public IHttpActionResult PublicacionesUsuario(int idUsuario)
        {
           List<Publicacion> publicacion = service.ObtenerPublicacionesUsuario(idUsuario);
           
            if (publicacion == null)
            {
                return NotFound();
            }

            return Ok(publicacion);
        }

    } // cierre controller
}