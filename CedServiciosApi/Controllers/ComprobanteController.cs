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
        public ComprobanteController(Models.ACContext context,IOptions<AppSettings> settings, IMemoryCache cache) : base(context,settings, cache)
        {
        }
        /// <summary>
        /// Consultar el comprobante.
        /// </summary>
        /// <param name="tipoComprobante">Tipo de Comprobante</param>
        /// <param name="nroPuntoVta">Número de Punto de Venta</param>
        /// <param name="nroComprobante">Número de Comprobante</param>
        [HttpGet]
        [Route("Leer")]
        public IEnumerable<CedServicios.Entidades.Respuesta> Leer(int tipoComprobante, int nroPuntoVta, int nroComprobante)
        {
            CedServicios.Entidades.Respuesta respuesta = new CedServicios.Entidades.Respuesta();
            try
            {
                CedServicios.Entidades.Sesion sesion;
                sesion = ObtenerSesion();
                CedServicios.Entidades.Comprobante comprobante = new CedServicios.Entidades.Comprobante();
                comprobante.TipoComprobante.Id = tipoComprobante;
                comprobante.NroPuntoVta = nroPuntoVta;
                comprobante.Nro = nroComprobante;
                CedServicios.RN.Comprobante.Leer(comprobante, sesion);
                respuesta.Severidad = CedServicios.Entidades.RespuestaDetalle.SeveridadEnum.Ok;
                respuesta.Objeto = comprobante;
            }
            catch (Exception ex)
            {
                CedServicios.RN.Respuesta.ExceptionToRespuesta(respuesta, ex);
            }
            yield return respuesta;
        }

        /// <summary>
        /// Consultar una lista de comprobantes.
        /// </summary>
        [HttpPost]
        [Route("Lista")]
        public IEnumerable<CedServicios.Entidades.Respuesta> Lista(CedServicios.Entidades.ComprobanteListaRequest ComprobanteListaRequest)
        {
            CedServicios.Entidades.Respuesta respuesta = new CedServicios.Entidades.Respuesta();
            try
            {
                CedServicios.Entidades.Sesion sesion;
                sesion = ObtenerSesion();
                //if (OrderBy == null) OrderBy = ""; if (NaturalezaComprobante == null) new CedServicios.Entidades.NaturalezaComprobante(); if (NumeroDeComprobante == null) NumeroDeComprobante = ""; if (PuntoDeVenta == null) PuntoDeVenta = ""; if (Detalle == null) Detalle = "";
                List<CedServicios.Entidades.Comprobante> comprobanteLista = new List<CedServicios.Entidades.Comprobante>();
                comprobanteLista = CedServicios.RN.Comprobante.ListaPaging(ComprobanteListaRequest, sesion);
                respuesta.Severidad = CedServicios.Entidades.RespuestaDetalle.SeveridadEnum.Ok;
                respuesta.Objeto = comprobanteLista;
            }
            catch (Exception ex)
            {
                CedServicios.RN.Respuesta.ExceptionToRespuesta(respuesta, ex);
            }
            yield return respuesta;
        }
    }
}
