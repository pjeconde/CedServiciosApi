using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace CedServiciosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ACController
    {
        public UsuarioController(Models.ACContext context,IOptions<AppSettings> settings, IMemoryCache cache) : base(context,settings, cache)
        {
        }
        
        /// <summary>
        /// Ingresar utilizando el Id. usuario y clave.
        /// </summary>
        /// <param name="id">Identificacion del usuario</param>
        /// <param name="password">Clave de acceso</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Ingresar")]
        public IEnumerable<CedServicios.Entidades.Respuesta> Ingresar(string id, string password)
        {
            CedServicios.Entidades.Respuesta respuesta = new CedServicios.Entidades.Respuesta();
            try
            {
                CedServicios.Entidades.Sesion sesion;
                sesion = ObtenerSesion();
                CedServicios.Entidades.Usuario usuario = new CedServicios.Entidades.Usuario();
                usuario.Id = id;
                usuario.Password = password;
                respuesta = CedServicios.RN.Usuario.Login(usuario, sesion);
            }
            catch (Exception ex)
            {
                CedServicios.RN.Respuesta.ExceptionToRespuesta(respuesta, ex);
            }
            yield return respuesta;
        }

        /// <summary>
        /// Ingresar utilizando el Id. usuario y clave.
        /// </summary>
        /// <param name="usuario">json de entidad usuario</param> 
        [HttpPost]
        [Route("Registrar")]
        public IEnumerable<CedServicios.Entidades.Respuesta> Registrar([FromBody] CedServicios.Entidades.Usuario usuario)
        {
            CedServicios.Entidades.Respuesta respuesta = new CedServicios.Entidades.Respuesta();
            try
            {
                CedServicios.Entidades.Sesion sesion;
                sesion = ObtenerSesion();
                respuesta = CedServicios.RN.Usuario.Registrar(usuario, sesion);
            }
            catch (Exception ex)
            {
                CedServicios.RN.Respuesta.ExceptionToRespuesta(respuesta, ex);
            }
            yield return respuesta;
        }

        /// <summary>
        /// Consultar los datos del usuario.
        /// </summary>
        /// <param name="id">Identificacion del usuario</param>
        //[HttpGet("{id}", Name = "Leer")]
        [HttpGet]
        [Route("Leer")]
        public IEnumerable<CedServicios.Entidades.Respuesta> Leer(string id)
        {
            CedServicios.Entidades.Respuesta respuesta = new CedServicios.Entidades.Respuesta();
            try
            {
                CedServicios.Entidades.Sesion sesion;
                sesion = ObtenerSesion();
                CedServicios.Entidades.Usuario usuario = new CedServicios.Entidades.Usuario();
                usuario.Id = id;
                CedServicios.RN.Usuario.Leer(usuario, sesion);
                respuesta.Severidad = CedServicios.Entidades.RespuestaDetalle.SeveridadEnum.Ok;
                respuesta.Objeto = usuario;
            }
            catch (Exception ex)
            {
                CedServicios.RN.Respuesta.ExceptionToRespuesta(respuesta, ex);
            }
            yield return respuesta;
        }

        /// <summary>
        /// Consultar si el Id. del usuario está disponible.
        /// </summary>
        /// <param name="id">Identificacion del usuario (nick)</param>
        [HttpGet]
        [Route("ConsultarIdDisponible")]
        public IEnumerable<CedServicios.Entidades.Respuesta> ConsultarIdDisponible(string id)
        {
            CedServicios.Entidades.Respuesta respuesta = new CedServicios.Entidades.Respuesta();
            try
            {
                CedServicios.Entidades.Sesion sesion;
                sesion = ObtenerSesion();
                CedServicios.Entidades.Usuario usuario = new CedServicios.Entidades.Usuario();
                usuario.Id = id;
                CedServicios.RN.Usuario.IdCuentaDisponible(usuario, sesion);
                respuesta.Severidad = CedServicios.Entidades.RespuestaDetalle.SeveridadEnum.Ok;
                respuesta.Objeto = true;
            }
            catch (Exception ex)
            {
                CedServicios.RN.Respuesta.ExceptionToRespuesta(respuesta, ex);
            }
            yield return respuesta;
        }
        /// <summary>
        /// Consultar una lista de usuarios.
        /// </summary>
        [HttpGet]
        [Route("Lista")]
        public IEnumerable<CedServicios.Entidades.Respuesta> Lista(int Pagina, string OrderBy, string IdUsuario, string Nombre, string Email, string Estado)
        {
            CedServicios.Entidades.Respuesta respuesta = new CedServicios.Entidades.Respuesta();
            try
            {
                CedServicios.Entidades.Sesion sesion;
                sesion = ObtenerSesion();
                if (OrderBy == null) OrderBy = ""; if (IdUsuario == null) IdUsuario = ""; if (Nombre == null) Nombre = ""; if (Email == null) Email = ""; if (Estado == null) Estado = "";
                CedServicios.Entidades.UsuarioLista usuarioLista = new CedServicios.Entidades.UsuarioLista();
                usuarioLista = CedServicios.RN.Usuario.ListaPaging(Pagina, OrderBy, IdUsuario, Nombre, Email, Estado, sesion);
                respuesta.Severidad = CedServicios.Entidades.RespuestaDetalle.SeveridadEnum.Ok;
                respuesta.Objeto = usuarioLista;
            }
            catch (Exception ex)
            {
                CedServicios.RN.Respuesta.ExceptionToRespuesta(respuesta, ex);
            }
            yield return respuesta;
        }

    }
}
