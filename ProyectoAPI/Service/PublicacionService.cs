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
            var obtener = instanciaBd.Publicacion.Where(publi => publi.idUsuario == idUsuario).ToList();
            //var listaPublicacionUsuario = instanciaBd.Feedback.Where(usuPubli => usuPubli.idUsuario == idUsuario).ToList();
            //var listaId = new List<int>();
            //var publicaciones = new List<Publicacion>();
            //foreach (var item in listaPublicacionUsuario)
            //{
            //    listaId.Add((int)item.idPublicacion);
            //}
            //foreach (var item2 in listaId)
            //{
            //    publicaciones.Add(instanciaBd.Publicacion.Find(item2));
            //}
            return obtener;
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
        public ComentarioCantidad ObtenerComentariosPublicacion(int idPublicacion, int idUsuario)
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
            var favorito = instanciaBd.Favorito.Where(usuFavorito => usuFavorito.idPublicacion == idPublicacion && usuFavorito.idUsuario==idUsuario).FirstOrDefault();
            if (favorito != null)
            {
                comentarioCantidad.favorito = true;
            }else {
                comentarioCantidad.favorito = false;
             }
            //var algo = (from fav in instanciaBd.Favorito
            //            where fav.idPublicacion = idPublicacion && fav.idUsuario = idUsuario
            //             select fav);

            return (comentarioCantidad);
        }

          //Subir favoritos
        public string SeleccionarFavoritos(int idPublicacion, int idUsuario)
        {
            Favorito favorito = new Favorito
            {
                idPublicacion = idPublicacion,
                idUsuario = idUsuario
            };
            instanciaBd.Favorito.Add(favorito);
            instanciaBd.SaveChanges();
            string ok = "OK";
            return (ok);
        }

        internal void EliminarFavorito(int idPublicacion, int idUsuario)
        {
            var favorito = instanciaBd.Favorito.Where(fav => fav.idPublicacion == idPublicacion && fav.idUsuario ==idUsuario).ToList();
            foreach (var item in favorito) {
                var result = instanciaBd.Favorito.Find(item.id);
                instanciaBd.Favorito.Remove(item);
                instanciaBd.SaveChanges();
            }
            throw new NotImplementedException();
        }
    }
}