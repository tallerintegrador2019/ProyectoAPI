using ProyectoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace ProyectoAPI.Services
{
    public class ProductoService
    {
        public async Task<string> ObtenerProductoPorNombre() {
            Publicacion publicacion = new Publicacion();
            var valor = "sin valores";
            HttpClient client = new HttpClient();
            var respuesta = await client.GetAsync("http://localhost:55081/Api/Publicacion/Buscar/tres");
            if (respuesta.IsSuccessStatusCode) {
                 publicacion = await respuesta.Content.ReadAsAsync<Publicacion>();
            }
            return publicacion.descripcion;
        }
    }
}