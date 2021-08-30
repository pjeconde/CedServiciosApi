using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace CedServiciosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComprobanteController : ACController
    {
        public ComprobanteController(Models.ACContext context, IOptions<AppSettings> settings, IMemoryCache cache) : base(context, settings, cache)
        {
        }
        ///// <summary>
        ///// Consultar un comprobante...
        ///// </summary>
        //[HttpPost]
        //[Route("Leer")]
        //public IEnumerable<CedServicios.Entidades.Response.ComprobanteResponse> Leer([FromBody] CedServicios.Entidades.Request.ComprobanteVentaRequest ComprobanteVentaRequest)
        //{
        //    CedServicios.Entidades.Response.ComprobanteResponse comprobanteResponse = new CedServicios.Entidades.Response.ComprobanteResponse();
        //    try
        //    {
        //        CedServicios.Entidades.Sesion sesion;
        //        sesion = ObtenerSesion();
        //        CedServicios.Entidades.Comprobante comprobante = new CedServicios.Entidades.Comprobante();
        //        comprobanteResponse.Respuesta = ValidarComprobanteVentaRequest(ComprobanteVentaRequest);
        //        if (comprobanteResponse.Respuesta.Resultado.Severidad == CedServicios.Entidades.Resultado.SeveridadEnum.Ok)
        //        {
        //            comprobante = CedServicios.RN.Comprobante.Leer("Venta", ComprobanteVentaRequest.Cuit, ComprobanteVentaRequest.TipoComprobante, ComprobanteVentaRequest.NroPuntoVta, ComprobanteVentaRequest.NroComprobante, 0, "", sesion);
        //            comprobanteResponse.Comprobante = comprobante;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        comprobanteResponse.Respuesta = CedServicios.RN.Respuesta.ExceptionToRespuesta(ex);
        //    }
        //    yield return comprobanteResponse;
        //}

        ///// <summary>
        ///// Consultar una lista de comprobantes.
        ///// </summary>
        //[HttpPost]
        //[Route("Lista")]
        //public IEnumerable<CedServicios.Entidades.Response.ComprobanteListaResponse> Lista(CedServicios.Entidades.Request.ComprobanteListaRequest ComprobanteListaRequest)
        //{
        //    CedServicios.Entidades.Response.ComprobanteListaResponse comprobanteListaResponse = new CedServicios.Entidades.Response.ComprobanteListaResponse();
        //    try
        //    {
        //        CedServicios.Entidades.Sesion sesion;
        //        sesion = ObtenerSesion();
        //        List<CedServicios.Entidades.Comprobante> comprobanteLista = new List<CedServicios.Entidades.Comprobante>();
        //        //comprobanteListaResponse.Comprobantes = CedServicios.RN.Comprobante.Lista(ComprobanteListaRequest.Paginacion.Pagina, ComprobanteListaRequest.Paginacion.OrderBy, ComprobanteListaRequest.Estados, ComprobanteListaRequest.EstadosCompra, ComprobanteListaRequest.TiposComprobante, ComprobanteListaRequest.FechaDsd, ComprobanteListaRequest.FechaHst, ComprobanteListaRequest.Persona, ComprobanteListaRequest.NaturalezaComprobante, ComprobanteListaRequest.PuntoDeVenta, ComprobanteListaRequest.NumeroDeComprobante, ComprobanteListaRequest.TiposComprobante, ComprobanteListaRequest.Detalle, sesion);
        //    }
        //    catch (Exception ex)
        //    {
        //        comprobanteListaResponse.Respuesta = CedServicios.RN.Respuesta.ExceptionToRespuesta(ex);
        //    }
        //    yield return comprobanteListaResponse;
        //}

        /// <summary>
        /// Consultar una lista de comprobantes.
        /// </summary>
        [HttpPost]
        [Route("ListaPrueba")]
        public IEnumerable<CedServicios.Entidades.ComprobanteDetalle> ListaPrueba(CedServicios.Entidades.Request.ComprobanteListaRequest ComprobanteListaRequest)
        {
            CedServicios.Entidades.ComprobanteDetalle pepe = new CedServicios.Entidades.ComprobanteDetalle();
            yield return pepe;
        }

        /// <summary>
        /// Consultar una lista de tipos de comprobante.
        /// </summary>
        [HttpGet]
        [Route("ListaTiposDeComprobante")]
        public IEnumerable<CedServicios.Entidades.Response.TiposDeComprobanteResponse> TiposDeComprobante()
        {
            CedServicios.Entidades.Response.TiposDeComprobanteResponse tiposDeComprobanteResponse = new CedServicios.Entidades.Response.TiposDeComprobanteResponse();
            try
            {
                tiposDeComprobanteResponse.TiposDeComprobante = CedServicios.RN.Comprobante.TiposDeComprobante();
            }
            catch (Exception ex)
            {
                tiposDeComprobanteResponse.Respuesta = CedServicios.RN.Respuesta.ExceptionToRespuesta(ex);
            }
            yield return tiposDeComprobanteResponse;
        }

        /// <summary>
        /// Consultar una lista de naturaleza de los comprobantes.
        /// </summary>
        [HttpGet]
        [Route("NaturalezaComprobanteLista")]
        public IEnumerable<CedServicios.Entidades.Response.NaturalezaComprobanteListaResponse> NaturalezaComprobanteLista()
        {
            CedServicios.Entidades.Response.NaturalezaComprobanteListaResponse naturalezaComprobanteListaResponse = new CedServicios.Entidades.Response.NaturalezaComprobanteListaResponse();
            try
            {
                naturalezaComprobanteListaResponse.NaturalezaComprobanteLista = CedServicios.RN.Comprobante.NaturalezaComprobanteLista();
            }
            catch (Exception ex)
            {
                naturalezaComprobanteListaResponse.Respuesta = CedServicios.RN.Respuesta.ExceptionToRespuesta(ex);
            }
            yield return naturalezaComprobanteListaResponse;
        }

        #region Validaciones Request
        private static CedServicios.Entidades.Respuesta ValidarComprobanteVentaRequest(CedServicios.Entidades.Request.ComprobanteVentaRequest ComprobanteVentaRequest)
        {
            CedServicios.Entidades.Respuesta respuesta = new CedServicios.Entidades.Respuesta();
            CedServicios.Entidades.Resultado resultado = CedServicios.RN.Respuesta.ValidarNumeric(ComprobanteVentaRequest.Cuit, (nameof(ComprobanteVentaRequest.Cuit)).ToLower());
            if (resultado.Severidad == CedServicios.Entidades.Resultado.SeveridadEnum.Error) { respuesta.Detalle.Add(resultado); }
            if (respuesta.Detalle.Count > 0)
            {
                respuesta.Resultado = respuesta.Detalle[0];
            }
            return respuesta;
        }
        private static CedServicios.Entidades.Respuesta ValidarComprobanteListaRequest(CedServicios.Entidades.Request.ComprobanteListaRequest ComprobanteListaRequest)
        {
            CedServicios.Entidades.Respuesta respuesta = new CedServicios.Entidades.Respuesta();
            //CedServicios.Entidades.Resultado resultado = CedServicios.RN.Respuesta.ValidarNumeric(ComprobanteListaRequest.Cuit, (nameof(ComprobanteListaRequest.Cuit)).ToLower());
            //if (resultado.Severidad == CedServicios.Entidades.Resultado.SeveridadEnum.Error) { respuesta.Detalle.Add(resultado); }
            //resultado = CedServicios.RN.Respuesta.ValidarNumeric(ComprobanteListaRequest.PuntoDeVenta, (nameof(ComprobanteListaRequest.PuntoDeVenta)).ToLower());
            //if (resultado.Severidad == CedServicios.Entidades.Resultado.SeveridadEnum.Error) { respuesta.Detalle.Add(resultado); }
            //resultado = CedServicios.RN.Respuesta.ValidarNumeric(ComprobanteListaRequest.NumeroDeComprobant, (nameof(ComprobanteListaRequest.NumeroDeComprobante)).ToLower());
            //if (resultado.Severidad == CedServicios.Entidades.Resultado.SeveridadEnum.Error) { respuesta.Detalle.Add(resultado); }
            if (respuesta.Detalle.Count > 0)
            {
                respuesta.Resultado = respuesta.Detalle[0];
            }
            return respuesta;
        }
        #endregion 
    }
}
