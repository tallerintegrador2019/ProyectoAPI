using ProyectoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoAPI;
namespace ProyectoAPI.Services
{
    public class LoginService
    {
        todaviasirveDBEntities contexto = new todaviasirveDBEntities();

        public bool verificarDatos(Usuario usu)
        {
            Usuario usuarioEncontrado = contexto.Usuario.Where(u => u.email.Equals(usu.email) && u.pass.Equals(usu.pass)).FirstOrDefault();
            
            if(usuarioEncontrado != null) {
                HttpContext.Current.Session["idUsuario"] = usuarioEncontrado.id;
                HttpContext.Current.Session["email"] = usuarioEncontrado.email;
               if (usuarioEncontrado.email.Equals("admin@admin"))
                {
                    HttpContext.Current.Session["esAdmin"] = true;
                }
                return true;
            }
            return false;
        }
        public bool verificarDatosApi(Usuario usu)
        {
            Usuario usuarioEncontrado = contexto.Usuario.Where(u => u.email.Equals(usu.email) && u.pass.Equals(usu.pass)).FirstOrDefault();

            if (usuarioEncontrado != null)
            {
                  return true;
            }
            return false;
        }
        public int RegistrarUsuario(Usuario usu) {
            contexto.Usuario.Add(usu);
            contexto.SaveChanges();
            return usu.id;
        }
    }
}