﻿using System;
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
using ProyectoAPI.Models.ViewModel;
using ProyectoAPI.Service;

namespace ProyectoAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PublicacionController : ApiController
    {
        private todaviasirveDBEntities db = new todaviasirveDBEntities();
        public PublicacionService service = new PublicacionService();
        // GET: api/Publicacion
        //public IQueryable<Publicacion> GetPublicacion()
        //{
        //    return db.Publicacion;
        //}
        [HttpGet]
        [ResponseType(typeof(Publicacion))]
        [Route("Api/Publicacion/")]
        public IHttpActionResult GetPublicacion()
        {
            List<Publicacion> publicacion = (from publi in db.Publicacion
                                             select publi).ToList();
            if (publicacion == null)
            {
                return NotFound();
            }
            return Ok(publicacion);
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
        public async Task<IHttpActionResult> PutPublicacion(int id)
        {

            Publicacion publi = db.Publicacion.Find(id);

            if (id != publi.id)
            {
                return BadRequest();
            }


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

                db.Entry(publi).State = EntityState.Modified;

            }

            try
            {
                db.SaveChanges();
                return Ok();
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

            //return StatusCode(HttpStatusCode.NoContent);
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
            //Feedback publiUsu = new Feedback();
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
                                //publiUsu.idUsuario = Convert.ToInt32(valor);
                                publi.idUsuario = Convert.ToInt32(valor);
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

                //publiUsu.idPublicacion = publi.id;
                //publiUsu.fecha = new DateTime().ToString();
                //db.Feedback.Add(publiUsu);
                //db.SaveChanges();
                //return Ok(HttpStatusCode.OK);
                //return Content(HttpStatusCode.OK, publi);
                return Ok(publi.id);

            }

            return CreatedAtRoute("DefaultApi", new { id = publi.id }, publi);
        }



        // DELETE: api/Publicacion/5
        //[ResponseType(typeof(Publicacion))]
        //public IHttpActionResult DeletePublicacion(int id)
        //{
        //    Publicacion publicacion = db.Publicacion.Find(id);
        //    if (publicacion == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Publicacion.Remove(publicacion);
        //    db.SaveChanges();

        //    return Ok(publicacion);
        //}


        [ResponseType(typeof(Publicacion))]
        public IHttpActionResult DeletePublicacion(int id)
        {
            Publicacion publicacion = db.Publicacion.Find(id);
            if (publicacion == null)
            {
                return NotFound();
            }

            List<Paso> pasos = (from p in db.Paso
                                where p.idPublicacion == id
                                select p).ToList();

            if (pasos != null)
            {
                db.Paso.RemoveRange(pasos);
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
            if (!String.IsNullOrEmpty(nombre))
            {
                List<Publicacion> publicaciones = (from publi in db.Publicacion
                                                   where publi.titulo.Contains(nombre)
                                                   select publi).ToList();

                if (publicaciones == null)
                {
                    return NotFound();
                }

                return Ok(publicaciones);
            }
            else {
                return NotFound();
            }
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

        [HttpGet]
        [ResponseType(typeof(int))]
        [Route("Api/Publicacion/DeletePublicacionUsuario/{id}/{idUsuario}")]
        public IHttpActionResult DeletePublicacionUsuario(int id, int idUsuario)
        {
            //Publicacion publicacion = db.Publicacion.Find(id);
            service.EliminarPublicacion(id);
            List<Publicacion> publicacion = service.ObtenerPublicacionesUsuario(idUsuario);
            if (publicacion == null)
            {
                return NotFound();
            }
            //if (publicacion == null)
            //{
            //    return NotFound();
            //}

            //db.Publicacion.Remove(publicacion);
            //db.SaveChanges();

            return Ok(publicacion);
        }

        [HttpGet]
        [Route("Api/Publicacion/seleccionarFavorito/{idPublicacion}/{idUsuario}")]
        public IHttpActionResult SeleccionarFavorito(int idPublicacion, int idUsuario)
        {
            //Publicacion publicacion = db.Publicacion.Find(id);

            //List<Publicacion> 
            var publicacion = service.SeleccionarFavoritos(idPublicacion, idUsuario);
            if (publicacion == null)
            {
                return NotFound();
            }
            //if (publicacion == null)
            //{
            //    return NotFound();
            //}

            //db.Publicacion.Remove(publicacion);
            //db.SaveChanges();

            return Ok(publicacion);
        }

        [HttpDelete]
        [Route("Api/Publicacion/eliminarFavorito/{idPublicacion}/{idUsuario}")]
        public IHttpActionResult EliminarFavorito(int idPublicacion, int idUsuario)
        {
            service.EliminarFavorito(idPublicacion, idUsuario);
            return Ok();
        }

        [HttpPost]
        [ResponseType(typeof(Publicacion))]
        [Route("Api/Publicacion/subirComentario")]
        public async Task<IHttpActionResult> SubirComentario()
        {
            var feedback = new Feedback();
            string root1 = HttpContext.Current.Server.MapPath("~/Content/Images");
            var provider = new MultipartFormDataStreamProvider(root1);
            // Read the form data.
            await Request.Content.ReadAsMultipartAsync(provider);
            foreach (var key in provider.FormData.AllKeys)
            {
                if (!key.Equals("__RequestVerificationToken"))
                {
                    switch (key)
                    {
                        case "comentario":
                            feedback.comentario = provider.FormData.GetValues(key)[0];
                            break;
                        case "idUsuario":
                            var valor = provider.FormData.GetValues(key)[0];
                            feedback.idUsuario = Convert.ToInt32(valor);
                            break;
                        case "idPublicacion":
                            var valor1 = provider.FormData.GetValues(key)[0];
                            feedback.idPublicacion = Convert.ToInt32(valor1);
                            break;
                        default:
                            break;
                    }
                }
            }
            db.Feedback.Add(feedback);
            db.SaveChanges();
            return Ok(HttpStatusCode.OK);
            //return Ok(HttpStatusCode.OK);
        }

        // controlador Obtener comentario de un publicacion
        [HttpGet]
        [ResponseType(typeof(Publicacion))]
        [Route("Api/Publicacion/obtenerComentarioPublicacion/{idPublicacion}/{idUsuario}")]
        public IHttpActionResult ObtenerComentarioPublicacion(int idPublicacion, int idUsuario)
        {
            ComentarioCantidad comentarioUsuario = service.ObtenerComentariosPublicacion(idPublicacion, idUsuario);
            if (comentarioUsuario == null)
            {
                return Ok("sin resltados");
            }

            return Ok(comentarioUsuario);
        }

        // controlador Obtener comentario de un publicacion
        [HttpGet]
        [ResponseType(typeof(Publicacion))]
        [Route("Api/Publicacion/ObtenerFavoritos/{idUsuario}")]
        public IHttpActionResult ObtenerFavoritos(int idUsuario)
        {
            List<Publicacion> favoritos = service.ObtenerFavoritos(idUsuario);
            if (favoritos == null)
            {
                return Ok("sin resltados");
            }

            return Ok(favoritos);
        }


        // cierre controller
    }
}