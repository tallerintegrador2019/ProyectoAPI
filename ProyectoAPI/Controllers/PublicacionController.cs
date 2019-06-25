﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Newtonsoft.Json;
using ProyectoAPI.Models;

namespace ProyectoAPI.Controllers
{
    public class PublicacionController : ApiController
    {
        private todaviasirveDBEntities db = new todaviasirveDBEntities();

        /*
        // GET: api/Publicacion
        public IQueryable<Publicacion> GetPublicacion()
        {
            return db.Publicacion;
        }
        */

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

        //// POST: api/Publicacion
        //[ResponseType(typeof(Publicacion))]
        //public IHttpActionResult PostPublicacion(Publicacion publicacion)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Publicacion.Add(publicacion);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = publicacion.id }, publicacion);
        //}

        // POST: api/Publicacion                    Nuevo Post
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(Publicacion))]
        public async Task<IHttpActionResult> PostPublicacionAsync()
        {
            Publicacion publicacion = new Publicacion();
            Paso paso1 = new Paso();
            Paso paso2 = new Paso();
            Paso paso3 = new Paso();
            Paso paso4 = new Paso();
            Paso paso5 = new Paso();

            var request = HttpContext.Current.Request;
            bool imagenDePublicacion = true;

            if (Request.Content.IsMimeMultipartContent())
            {
                string root1 = HttpContext.Current.Server.MapPath("~/Content/Images");
                var provider = new MultipartFormDataStreamProvider(root1);
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);
                // This illustrates how to get the file names.
                //foreach (MultipartFileData file in provider.FileData)
                //{
                //    Trace.WriteLine(file.Headers.ContentDisposition.FileName);
                //    Trace.WriteLine("Server file path: " + file.LocalFileName);
                //}
                foreach (var key in provider.FormData.AllKeys)
                {
                    if (!key.Equals("__RequestVerificationToken"))
                    {
                        switch (key)
                        {
                            case "titulo":
                                publicacion.titulo = provider.FormData.GetValues(key)[0];
                                break;
                            case "subtitulo":
                                publicacion.subtitulo = provider.FormData.GetValues(key)[0];
                                break;
                            case "descripcion":
                                publicacion.descripcion = provider.FormData.GetValues(key)[0];
                                break;
                            case "descripcion1":
                                paso1.descripcion = provider.FormData.GetValues(key)[0];
                                break;
                            case "descripcion2":
                                paso2.descripcion = provider.FormData.GetValues(key)[0];
                                break;
                            case "descripcion3":
                                paso3.descripcion = provider.FormData.GetValues(key)[0];
                                break;
                            case "descripcion4":
                                paso4.descripcion = provider.FormData.GetValues(key)[0];
                                break;
                            case "descripcion5":
                                paso5.descripcion = provider.FormData.GetValues(key)[0];
                                break;
                            default:
                                break;
                        }

                    }
                }
                for (var j = 0; j < request.Files.Count; j++)
                {
                    var archivo = request.Files[j];
                    var cantidadArchivos = request.Files.Count;
                    var fileName = "";
                    var imagenlocal = "";
                    switch (j + 1)
                    {
                        case 1:
                            var nombre = Path.GetFileName(archivo.FileName);
                            string root = Path.Combine(root1, nombre);
                            archivo.SaveAs(root);
                            publicacion.imagenPortada = nombre;
                            db.Publicacion.Add(publicacion);
                            db.SaveChanges();
                            break;
                        case 2:
                            fileName = Path.GetFileName(archivo.FileName);
                            imagenlocal = Path.Combine(root1, fileName);
                            archivo.SaveAs(imagenlocal);
                            paso1.idPublicacion = publicacion.id;
                            paso1.imagen = fileName;
                            db.Paso.Add(paso1);
                            db.SaveChanges();
                            break;
                        case 3:
                            fileName = Path.GetFileName(archivo.FileName);
                            imagenlocal = Path.Combine(root1, fileName);
                            archivo.SaveAs(imagenlocal);
                            paso2.idPublicacion = publicacion.id;
                            paso2.imagen = fileName;
                            db.Paso.Add(paso2);
                            db.SaveChanges();
                            break;
                        case 4:
                            fileName = Path.GetFileName(archivo.FileName);
                            imagenlocal = Path.Combine(root1, fileName);
                            archivo.SaveAs(imagenlocal);
                            paso3.idPublicacion = publicacion.id;
                            paso3.imagen = fileName;
                            db.Paso.Add(paso3);
                            db.SaveChanges();
                            break;
                        case 5:
                            fileName = Path.GetFileName(archivo.FileName);
                            imagenlocal = Path.Combine(root1, fileName);
                            archivo.SaveAs(imagenlocal);
                            paso4.idPublicacion = publicacion.id;
                            paso4.imagen = fileName;
                            db.Paso.Add(paso4);
                            db.SaveChanges();
                            break;
                        case 6:
                            fileName = Path.GetFileName(archivo.FileName);
                            imagenlocal = Path.Combine(root1, fileName);
                            archivo.SaveAs(imagenlocal);
                            paso5.idPublicacion = publicacion.id;
                            paso5.imagen = fileName;
                            db.Paso.Add(paso5);
                            db.SaveChanges();
                            break;
                        default:
                            break;
                    }
                }


                //if (request.Files.Count > 0)
                //{
                //    var imagen = request.Files[0];
                //    var postedFile = request.Files.Get("file");
                //    string root = Path.Combine(HttpContext.Current.Server.MapPath("~/Content/Images"), imagen.FileName);
                //    //root = root + "/" + imagen.FileName;
                //    imagen.SaveAs(root);
                //    publicacion.imagen = imagen.FileName;


                //}

                return Ok(HttpStatusCode.OK);

            }
            else
            {
                return BadRequest();
            }

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

        // OBSOLETO
        //// METODO BUSCAR  
        //[HttpGet]
        //[Route("Api/Publicacion/Buscar/{nombre}")]
        //public HttpResponseMessage Buscar(string nombre)
        //{
        //    try
        //    {
        //        List<Publicacion> publicacion = (from publi in db.Publicacion
        //                                         where publi.titulo.Contains(nombre)
        //                                         select publi).ToList();

        //        var response = new HttpResponseMessage(HttpStatusCode.OK);
        //        response.Content = new StringContent(JsonConvert.SerializeObject(publicacion));
        //        response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //        return response;
        //    }
        //    catch
        //    {
        //        return new HttpResponseMessage(HttpStatusCode.BadRequest);
        //    }
        //}

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



    } // cierre clase
} // cierre controller
                      