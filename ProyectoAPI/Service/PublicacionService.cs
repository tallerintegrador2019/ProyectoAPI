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
            var listaPublicacionUsuario = instanciaBd.Feedback.Where(usuPubli => usuPubli.idUsuario == idUsuario).ToList();
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
        public void EliminarPublicacion(int idPublicacion)
        {
            // Trae los pasos que tiene una publicacion 
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
            //Trae la asociacion de publicacion a usuario y la elimina
            var publicacionUsuario = instanciaBd.Feedback.Where(publiUsu => publiUsu.idPublicacion == idPublicacion).FirstOrDefault();
            if (publicacionUsuario != null)
            {
                instanciaBd.Feedback.Remove(publicacionUsuario);
                instanciaBd.SaveChanges();
            }

            Publicacion publicacion = instanciaBd.Publicacion.Find(idPublicacion);
            instanciaBd.Publicacion.Remove(publicacion);
            instanciaBd.SaveChanges();
        }




    }
}