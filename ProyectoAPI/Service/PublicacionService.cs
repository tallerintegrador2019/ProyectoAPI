using ProyectoAPI.Models;
using ProyectoAPI.Models.ViewModel;
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

        //Obtener comentarios de una publicacion
        public ComentarioCantidad ObtenerComentariosPublicacion(int idPublicacion)
        {
            var publicaciones = instanciaBd.Feedback.Where(usuPubli => usuPubli.idPublicacion == idPublicacion).ToList();
            var cantidad = publicaciones.Count();
            var comentarios = new List<string>();
            var listadoComentarioUsuario = new List<ComentarioUsuario>();
            var comentarioCantidad = new ComentarioCantidad();
            comentarioCantidad.cantidad = cantidad.ToString();
            foreach (var item in publicaciones)
            {
                var comentarioUsuario = new ComentarioUsuario();
                var imagenUsuario = instanciaBd.Usuario.Where(usuImagen => usuImagen.id == item.idUsuario).FirstOrDefault();
                comentarioUsuario.comentario = item.comentario;
                comentarioUsuario.imagen = imagenUsuario.imagen;
                listadoComentarioUsuario.Add(comentarioUsuario);
            }
            comentarioCantidad.comentarioUsuarios = listadoComentarioUsuario;


            return (comentarioCantidad);
        }



    }
}