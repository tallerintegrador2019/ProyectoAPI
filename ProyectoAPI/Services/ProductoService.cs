using Newtonsoft.Json;
using ProyectoAPI.Models;
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
            //return publicacion;
            List<Publicacion> publicaciones = instanciaBd.Publicacion.ToList();
            return publicaciones;
        }

    }
}