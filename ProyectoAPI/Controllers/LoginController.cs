using ProyectoAPI.Models;
using ProyectoAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProyectoAPI.Controllers
{
    public class LoginController : ApiController
    {
        LoginService login = new LoginService();
        // METODO Login
        [HttpPost]
        public HttpResponseMessage Login(Usuario usu)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            try
            {
                if (ModelState.IsValid)
                {
                    bool usuarioEncontrado = login.verificarDatos(usu);
                    if (usuarioEncontrado)
                    {
                        return response;
                    }
                    else
                    {
                        return new HttpResponseMessage(HttpStatusCode.BadRequest);
                        // return BadRequest("usuario no encontrado");
                    }
                }
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
                //return BadRequest("usuario no encontrado");
                //return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            catch
            {
                //return BadRequest("usuario no encontrado");
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
    }
}
