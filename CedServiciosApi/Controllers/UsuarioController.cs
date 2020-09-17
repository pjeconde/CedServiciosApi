using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CedServiciosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ACController
    {
        public UsuarioController() : base()
        {
        }
        [HttpGet]
        public CedServicios.Entidades.Sesion Ingresar(string Usuario, string Clave)
        {
            //Entidades.Sesion sesion = HttpContext.Session.GetObj<Entidades.Sesion>("Sesion");
            CedServicios.Entidades.Sesion sesion = new CedServicios.Entidades.Sesion();
            sesion = CrearSesion();
            return sesion;
        }

        // GET: api/Usuario/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Usuario
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Usuario/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        
        //public CedServicios.Entidades.Sesion CrearSesion()
        //{
        //    CedServicios.Entidades.Sesion s = new CedServicios.Entidades.Sesion();
        //    s.CnnStr = Microsoft.Extensions.Configuration.GetConnectionString("DefaultConnection");
        //    s.AdministradoresSiteEmail = Configuration.GetValue<string>("AppSettings:Mantenedores");
        //    s.Ambiente = Configuration.GetValue<string>("AppSettings:Ambiente");
        //    s.Opciones = CedServicios.RN.Sesion.Opciones(s);
        //    s.OpcionesHabilitadas = CedServicios.RN.Sesion.OpcionesHabilitadas(s);
        //    s.Usuario = new Entidades.Usuario();
        //    s.URLsite = HttpContext.Request.Host.Value.ToString();  //HttpContext.Request.Path.Value.ToString();
        //    HttpContext.Session.Set("UsuarioId", System.Text.UTF8Encoding.UTF8.GetBytes(Environment.UserName));
        //    HttpContext.Session.SetObj("Sesion", s);
        //    return s;
        //}
    }
}
