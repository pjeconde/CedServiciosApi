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
        public UsuarioController(Models.ACContext context, IOptions<AppSettings> settings, IMemoryCache cache) : base(context, settings, cache)
        {
        }

        /// <summary>
        /// Ingresar utilizando el Id. usuario y clave.
        /// </summary>
        /// <param name="IdUsuario">Identificacion del usuario</param>
        /// <param name="Clave">Clave de acceso</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Ingresar")]
        public IEnumerable<CedServicios.Entidades.Response.ValorBoolResponse> Ingresar(string IdUsuario, string Clave)
        {
            CedServicios.Entidades.Response.ValorBoolResponse valorBoolResponse = new CedServicios.Entidades.Response.ValorBoolResponse();
            try
            {
                CedServicios.Entidades.Sesion sesion;
                sesion = ObtenerSesion();
                valorBoolResponse = CedServicios.RN.Usuario.Login(IdUsuario, Clave, sesion);
            }
            catch (Exception ex)
            {
                valorBoolResponse.Respuesta = CedServicios.RN.Respuesta.ExceptionToRespuesta(ex);
            }
            yield return valorBoolResponse;
        }

        /// <summary>
        /// Ingresar utilizando el Id. usuario y clave.
        /// </summary>
        /// <param name="Usuario">json de entidad UsuarioDatosBasicos</param> 
        [HttpPost]
        [Route("Registrar")]
        public IEnumerable<CedServicios.Entidades.Response.ValorBoolResponse> Registrar([FromBody] CedServicios.Entidades.UsuarioDatosBasicos Usuario)
        {
            CedServicios.Entidades.Response.ValorBoolResponse valorBoolResponse = new CedServicios.Entidades.Response.ValorBoolResponse();
            try
            {
                CedServicios.Entidades.Sesion sesion;
                sesion = ObtenerSesion();
                valorBoolResponse = CedServicios.RN.Usuario.Registrar(Usuario, sesion);
            }
            catch (Exception ex)
            {
                valorBoolResponse.Respuesta = CedServicios.RN.Respuesta.ExceptionToRespuesta(ex);
            }
            yield return valorBoolResponse;
        }

        /// <summary>
        /// Consultar los datos del usuario.
        /// </summary>
        /// <param name="Id">Identificacion del usuario</param>
        [HttpGet]
        [Route("Leer")]
        public ActionResult<CedServicios.Entidades.Response.UsuarioResponse> Leer(string Id)
        {
            CedServicios.Entidades.Response.UsuarioResponse usuarioResponse = new CedServicios.Entidades.Response.UsuarioResponse();

            try
            {
                CedServicios.Entidades.Sesion sesion;
                sesion = ObtenerSesion();
                CedServicios.Entidades.Usuario usuario = new CedServicios.Entidades.Usuario();
                usuario = CedServicios.RN.Usuario.Leer(Id, sesion);
                usuarioResponse.Usuario = usuario;
            }
            catch (Exception ex)
            {
                usuarioResponse.Respuesta = CedServicios.RN.Respuesta.ExceptionToRespuesta(ex);
            }
            return usuarioResponse;
        }

        /// <summary>
        /// Consultar si el Id. del usuario está disponible.
        /// </summary>
        /// <param name="Id">Identificacion del usuario (nick)</param>
        [HttpGet]
        [Route("ConsultarIdDisponible")]
        public IEnumerable<CedServicios.Entidades.Response.ValorBoolResponse> ConsultarIdDisponible(string Id)
        {
            CedServicios.Entidades.Response.ValorBoolResponse valorBoolResponse = new CedServicios.Entidades.Response.ValorBoolResponse();
            try
            {
                CedServicios.Entidades.Sesion sesion;
                sesion = ObtenerSesion();
                valorBoolResponse.Valor = CedServicios.RN.Usuario.IdCuentaDisponible(Id, sesion);
            }
            catch (Exception ex)
            {
                valorBoolResponse.Respuesta = CedServicios.RN.Respuesta.ExceptionToRespuesta(ex);
            }
            yield return valorBoolResponse;
        }

        /// <summary>
        /// Cambiar la clave actual.
        /// </summary>
        /// <param name="Id">Identificacion del usuario (nick)</param>
        /// <param name="Clave">Clave actual</param>
        /// <param name="ClaveNueva">Clave nueva</param>
        /// <param name="ClaveNuevaConfirmacion">Confirmación de la Clave nueva</param>
        [HttpGet]
        [Route("CambiarClave")]
        public IEnumerable<CedServicios.Entidades.Response.ValorBoolResponse> CambiarClave(string Id, string Clave, string ClaveNueva, string ClaveNuevaConfirmacion)
        {
            CedServicios.Entidades.Response.ValorBoolResponse valorBoolResponse = new CedServicios.Entidades.Response.ValorBoolResponse();
            try
            {
                CedServicios.Entidades.Sesion sesion;
                sesion = ObtenerSesion();
                valorBoolResponse.Valor = CedServicios.RN.Usuario.CambiarClave(Id, Clave, ClaveNueva, ClaveNuevaConfirmacion, sesion);
            }
            catch (Exception ex)
            {
                valorBoolResponse.Respuesta = CedServicios.RN.Respuesta.ExceptionToRespuesta(ex);
            }
            yield return valorBoolResponse;
        }

        /// <summary>
        /// Consultar el Id.Usuario registrado para un email.
        /// </summary>
        /// <param name="Email">Email del usuario</param>
        [HttpGet]
        [Route("ConsultarIdUsuarioPorEmail")]
        public IEnumerable<CedServicios.Entidades.Response.ValorBoolResponse> ConsultarIdUsuarioPorEmail(string Email)
        {
            CedServicios.Entidades.Response.ValorBoolResponse valorBoolResponse = new CedServicios.Entidades.Response.ValorBoolResponse();
            try
            {
                CedServicios.Entidades.Sesion sesion;
                sesion = ObtenerSesion();
                CedServicios.RN.EnvioCorreo.ReporteIdUsuarios(Email, sesion);
                valorBoolResponse.Valor = true;
            }
            catch (Exception ex)
            {
                valorBoolResponse.Respuesta = CedServicios.RN.Respuesta.ExceptionToRespuesta(ex);
            }
            yield return valorBoolResponse;
        }

        /// <summary>
        /// Consultar una lista de usuarios.
        /// </summary>
        /// <param name="UsuarioListaRequest">json de entidad UsuarioListaRequest</param> 
        [HttpPost]
        [Route("Lista")]
        public IEnumerable<CedServicios.Entidades.Response.UsuarioListaResponse> Lista([FromBody] CedServicios.Entidades.Request.UsuarioListaRequest UsuarioListaRequest)
        {
            CedServicios.Entidades.Response.UsuarioListaResponse usuarioListaResponse = new CedServicios.Entidades.Response.UsuarioListaResponse();
            try
            {
                CedServicios.Entidades.Sesion sesion;
                sesion = ObtenerSesion();
                usuarioListaResponse.Respuesta = ValidarUsuarioListaRequest(UsuarioListaRequest);
                if (usuarioListaResponse.Respuesta.Resultado.Severidad == CedServicios.Entidades.Resultado.SeveridadEnum.Ok)
                {
                    usuarioListaResponse = CedServicios.RN.Usuario.Lista(UsuarioListaRequest.Paginacion.Pagina, UsuarioListaRequest.Paginacion.OrderBy, UsuarioListaRequest.IdUsuario, UsuarioListaRequest.Nombre, UsuarioListaRequest.Email, UsuarioListaRequest.Estado, sesion);
                }
            }
            catch (Exception ex)
            {
                usuarioListaResponse.Respuesta = CedServicios.RN.Respuesta.ExceptionToRespuesta(ex);
            }
            yield return usuarioListaResponse;
        }

        private static CedServicios.Entidades.Respuesta ValidarUsuarioListaRequest(CedServicios.Entidades.Request.UsuarioListaRequest UsuariolistaRequest)
        {
            CedServicios.Entidades.Respuesta respuesta = new CedServicios.Entidades.Respuesta();
            if (UsuariolistaRequest.Paginacion.Pagina < 1)
            {
                respuesta.Detalle.Add(new CedServicios.Entidades.Resultado(CedServicios.Entidades.Resultado.SeveridadEnum.Error, "", "La página a consultar no puedo ser inferior a 1."));
                respuesta.Resultado = respuesta.Detalle[0];
            }
            return respuesta;
        }

        /// <summary>
        /// Consultar el Id.Usuario registrado para un email.
        /// </summary>
        /// <param name="Id">Identificacion del usuario (nick)</param>
        /// <param name="Email">Email del usuario</param> 
        [HttpGet]
        [Route("ConsultarPreguntaDeSeguridad")]
        public IEnumerable<CedServicios.Entidades.Response.ValorStringResponse> ConsultarPreguntaDeSeguridad(string Id, string Email)
        {
            CedServicios.Entidades.Response.ValorStringResponse valorStringResponse = new CedServicios.Entidades.Response.ValorStringResponse();
            try
            {
                CedServicios.Entidades.Sesion sesion;
                sesion = ObtenerSesion();
                valorStringResponse = CedServicios.RN.Usuario.PreguntaSeguridad(Id, Email, sesion);
            }
            catch (Exception ex)
            {
                valorStringResponse.Respuesta = CedServicios.RN.Respuesta.ExceptionToRespuesta(ex);
            }
            yield return valorStringResponse;
        }

        /// <summary>
        /// Cambiar la clave con la pregunta de seguridad (por olvido de clave).
        /// </summary>
        [HttpPost]
        [Route("CambiarClaveConPreguntaSeg")]
        public IEnumerable<CedServicios.Entidades.Response.ValorBoolResponse> CambiarClaveConPreguntaSeg([FromBody] CedServicios.Entidades.Request.UsuarioCambiarClaveConPreguntaSegRequest UsuarioCambiarClaveConPreguntaSegRequest)
        {
            CedServicios.Entidades.Response.ValorBoolResponse valorBoolResponse = new CedServicios.Entidades.Response.ValorBoolResponse();
            try
            {
                CedServicios.Entidades.Sesion sesion;
                sesion = ObtenerSesion();
                valorBoolResponse.Valor = CedServicios.RN.Usuario.CambiarClaveConPreguntaDeSeguridad(UsuarioCambiarClaveConPreguntaSegRequest, sesion);
            }
            catch (Exception ex)
            {
                valorBoolResponse.Respuesta = CedServicios.RN.Respuesta.ExceptionToRespuesta(ex);
            }
            yield return valorBoolResponse;
        }
    }
}
