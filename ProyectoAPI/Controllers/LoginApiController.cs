using ProyectoAPI.Models;
using ProyectoAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ProyectoAPI.Controllers
{
    public class LoginApiController : ApiController
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
                    bool usuarioEncontrado = login.verificarDatosApi(usu);
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
        [HttpPost]
        [Route("Api/LoginApi/RegistrarUsuario")]
        public IHttpActionResult RegistrarUsuario(Usuario usu) {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            try
            {
                login.RegistrarUsuario(usu);
            }
            catch {
                return BadRequest("No se pudo registrar al usuario");
                //return new HttpResponseMessage(HttpStatusCode.NotImplemented);
            }
            response.Content = new StringContent(JsonConvert.SerializeObject(usu));
            //response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return Ok(usu);
        }
    }
}
