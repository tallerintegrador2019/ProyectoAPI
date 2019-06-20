﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using ProyectoAPI.Models;
using ProyectoAPI.Models.ViewModel;
using ProyectoAPI.Services;

namespace ProyectoAPI.Controllers
{
    public class PublicacionCRUDController : Controller
    {
        private todaviasirveDBEntities db = new todaviasirveDBEntities();
        PublicacionService service = new PublicacionService();
        // GET: PublicacionCRUD
        public ActionResult Index()
        {
            return View(db.Publicacion.ToList());
        }

        // GET: PublicacionCRUD/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publicacion publicacion = db.Publicacion.Find(id);
            if (publicacion == null)
            {
                return HttpNotFound();
            }
            return View(publicacion);
        }

        // GET: PublicacionCRUD/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PublicacionCRUD/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,titulo,subtitulo,descripcion,fechaSubida,imagenPortada")] Publicacion publicacion)
        {
                         
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];
                    if (file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var imagenlocal = Path.Combine(
                            Server.MapPath("~/Content/images"), fileName);
                        file.SaveAs(imagenlocal);
                        publicacion.imagenPortada = fileName;
                    }
                }
                db.Publicacion.Add(publicacion);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(publicacion);
        }

        public ActionResult ReconocerImagen() {
            HttpPostedFileBase file = Request.Files[0];
            string valor;
            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var imagenlocal = Path.Combine(
                    Server.MapPath("~/Content/images"), fileName);
                file.SaveAs(imagenlocal);
               
            }
            //Llamada a Imagga
            // Resultados en español : language=es 
            // Limite de resultados : limit=3 -- limit (default: -1 - meaning all tags)	
            //
            //HttpWebRequest request =
            //WebRequest.Create("https://api.imagga.com/v2/tags?image_url=https://s3.amazonaws.com/imagga-demo-uploads/tagging-demo/f067ff3435241405bd8d7901e1e348c7.jpg&language=es&limit=5")
            //as HttpWebRequest;
            //request.Method = "GET";
            ////request.ContentType = "application/x-www-form-urlencoded";
            //request.Accept = "application/json; charset=utf-8";
            //request.Headers.Add("Authorization", "Basic YWNjXzBlNTgzY2YyOTllNmIxNDowODU3ZjFkOGI4OWRjYjMwYWZiYjhjNmMwMzRlYmQxNA==");
            //// Metodo modificado
            //JsonImagga resultadoImagen;
            //string resp;

            //HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //Console.WriteLine("tamaño del contenido : {0}", response.ContentLength);
            //Console.WriteLine("Content type is {0}", response.ContentType);
            //using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            ////using (Stream reader = response.GetResponseStream())
            //{
            //    resp = reader.ReadToEnd();
            //    //Console.WriteLine("Response stream received.");
            //    //Console.WriteLine(resp);

            //    //JsonSerializer js = new JsonSerializer();
            //    //JsonImagga obj = (JsonImagga)js.Deserialize(resp,typeof(JsonImagga));
            //    //var resultadoImagen = JsonConvert.DeserializeObject<JsonImagga>(reader);
            //    resultadoImagen = JsonConvert.DeserializeObject<JsonImagga>(resp);
            //    //foreach (var val in resultadoImagen)
            //    //{
            //    //    foreach (var valo in val.tags)
            //    //    {
            //    //        Console.WriteLine(valo.tag.en);
            //    //    }
            //    //}
            //    //foreach (var valo in resultadoImagen.tags)
            //    //{
            //    //           Console.WriteLine(valo.tag.en);
            //    //}
            //    //Console.WriteLine(resultadoImagen);
            //    response.Close();
            //    reader.Close();
            //}

            //Console.WriteLine(resp);
            //var resultadoImagen = JsonConvert.DeserializeObject<JsonImagga>(resp);
            //Console.WriteLine(resultadoImagen.confidence);
            //Console.Write("valor" + resp);
            List<string> listado = new List<string>();
            listado.Add("Botella");
            listado.Add("Plastico");
            ViewBag.listadoReconocido = listado;
            return View();
        }



        //public async Task<JsonImagga> LeerJson()
        //{
        //    using (var client = new HttpClient())
        //    {
        //        using (client)
        //            client.BaseAddress = new Uri("https://api.imagga.com/v2/tags?image_url=");
        //        client.DefaultRequestHeaders.Authorization
        //         = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", "YWNjXzBlNTgzY2YyOTllNmIxNDowODU3ZjFkOGI4OWRjYjMwYWZiYjhjNmMwMzRlYmQxNA==");
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        // Hace la llamada a http://url-base-del-api/api/products/<id>
        //        var response = await client.GetAsync("https://s3.amazonaws.com/imagga-demo-uploads/tagging-demo/f067ff3435241405bd8d7901e1e348c7.jpg&language=es&limit=5");

        //        if (response.IsSuccessStatusCode)
        //        {
        //            // Lee el response y lo deserializa como un Product
        //           var valor =  await response.Content.ReadAsAsync<JsonImagga>();
        //        }
        //        // Sino devuelve null
        //        return await Task.FromResult<JsonImagga>(null);
        //        //using (HttpWebResponse response = (HttpWebResponse) await request.GetResponseAsync())
        //        //using (Stream stream = response.GetResponseStream())
        //        //using (StreamReader reader = new StreamReader(stream))
        //        //{
        //        //    var valor = await reader.ReadToEndAsync();
        //        //}
        //    }
        //}

        // GET: PublicacionCRUD/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publicacion publicacion = db.Publicacion.Find(id);
            if (publicacion == null)
            {
                return HttpNotFound();
            }
            return View(publicacion);
        }

        // POST: PublicacionCRUD/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,titulo,subtitulo,descripcion,fechaSubida,imagenPortada")] Publicacion publicacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(publicacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(publicacion);
        }

        // GET: PublicacionCRUD/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publicacion publicacion = db.Publicacion.Find(id);
            if (publicacion == null)
            {
                return HttpNotFound();
            }
            return View(publicacion);
        }

        // POST: PublicacionCRUD/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Publicacion publicacion = db.Publicacion.Find(id);
            db.Publicacion.Remove(publicacion);
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
