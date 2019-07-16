using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoAPI.Models.ViewModel
{
    public class ComentarioCantidad
    {
        public List<ComentarioUsuario> comentarioUsuarios { get; set; }
        public string cantidad { get; set; }
        public bool favorito { get; set; }
        public int cantLike { get; set; }
        public bool meGusta { get; set; } 
    }
}