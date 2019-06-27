using Newtonsoft.Json;
using ProyectoAPI.Models;
using ProyectoAPI.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace ProyectoAPI.Services
{
    public class PublicacionService
    {
        todaviasirveDBEntities instanciaBd = new todaviasirveDBEntities();

        public List<Publicacion> ObtenerPublicaciones() {
            Publicacion publicacion = new Publicacion();
            //HttpClient client = new HttpClient();
            //var respuesta = client.GetAsync("http://localhost:55081/Api/Publicacion/Buscar/tres");
            // publicacion = respuesta.Content.ReadAsAsync<Publicacion>();
            //publicacion =await respuesta.Result.Content.ReadAsAsync<Publicacion>();
            //
            List<Publicacion> publicaciones = instanciaBd.Publicacion.ToList();
            return publicaciones;
        }

        public List<Publicacion> ObtenerPublicacionesUsuario() {
            int idUsuario = (int)HttpContext.Current.Session["idUsuario"];
            var listaPublicacionUsuario = instanciaBd.Publicacion_Usuario.Where(usuPubli => usuPubli.idUsuario == idUsuario).ToList();
            var listaId = new List<int>();
            var publicaciones = new List<Publicacion>();
            foreach (var item in listaPublicacionUsuario) {
                listaId.Add((int)item.idPublicacion);
            }
            foreach (var item2 in listaId) {
                publicaciones.Add(instanciaBd.Publicacion.Find(item2));
            }
            return publicaciones;
        }

        public void EliminarPublicacion(int idPublicacion) {
            // Trae las invitaciones que tiene un pedido
            var listaPasos = instanciaBd.Paso.Where(paso => paso.idPublicacion == idPublicacion).ToList();
            foreach (var item in listaPasos)
            {
                Paso pasoEliminar = instanciaBd.Paso.Find(item.id);
                if (pasoEliminar != null)
                {
                    instanciaBd.Paso.Remove(pasoEliminar);
                    instanciaBd.SaveChanges();
                }
            }
            Publicacion publicacion = instanciaBd.Publicacion.Find(idPublicacion);
            instanciaBd.Publicacion.Remove(publicacion);
            instanciaBd.SaveChanges();
        }

        public async Task<JsonImagga> LeerJson()
        {
            using (var client = new HttpClient())
            {
                using (client)
                    client.BaseAddress = new Uri("https://api.imagga.com/v2/tags?image_url=");
                client.DefaultRequestHeaders.Authorization
                 = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", "YWNjXzBlNTgzY2YyOTllNmIxNDowODU3ZjFkOGI4OWRjYjMwYWZiYjhjNmMwMzRlYmQxNA==");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // Hace la llamada a http://url-base-del-api/api/products/<id>
                var response = await client.GetAsync("https://s3.amazonaws.com/imagga-demo-uploads/tagging-demo/f067ff3435241405bd8d7901e1e348c7.jpg&language=es&limit=5");

                if (response.IsSuccessStatusCode)
                {
                    // Lee el response y lo deserializa como un Elemento de imagga
                    var valor = await response.Content.ReadAsAsync<JsonImagga>();
                }
                // Sino devuelve null
                return await Task.FromResult<JsonImagga>(null);
                //using (HttpWebResponse response = (HttpWebResponse) await request.GetResponseAsync())
                //using (Stream stream = response.GetResponseStream())
                //using (StreamReader reader = new StreamReader(stream))
                //{
                //    var valor = await reader.ReadToEndAsync();
                //}
            }
        }




    }
}