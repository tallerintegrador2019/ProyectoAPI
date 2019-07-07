using ProyectoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoAPI.Service
{
    public class PublicacionService
    {

        private todaviasirveDBEntities instanciaBd = new todaviasirveDBEntities();

        public List<Publicacion> ObtenerPublicacionesUsuario(int idUsuario)
        {
            var listaPublicacionUsuario = instanciaBd.Publicacion_Usuario.Where(usuPubli => usuPubli.idUsuario == idUsuario).ToList();
            var listaId = new List<int>();
            var publicaciones = new List<Publicacion>();
            foreach (var item in listaPublicacionUsuario)
            {
                listaId.Add((int)item.idPublicacion);
            }
            foreach (var item2 in listaId)
            {
                publicaciones.Add(instanciaBd.Publicacion.Find(item2));
            }
            return publicaciones;
        }
    }
}