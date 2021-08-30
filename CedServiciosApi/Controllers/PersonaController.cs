using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace CedServiciosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ACController
    {
        public PersonaController(Models.ACContext context, IOptions<AppSettings> settings, IMemoryCache cache) : base(context, settings, cache)
        {
        }

        ///// <summary>
        ///// Craer una nueva persona (cliente, proveedo o ambas)
        ///// </summary>
        //[HttpPost]
        //[Route("Registrar")]
        //public IEnumerable<CedServicios.Entidades.Response.ValorBoolResponse> Registrar([FromBody] CedServicios.Entidades.Request.PersonaCrearRequest personaCrearRequest)
        //{
        //    CedServicios.Entidades.Response.ValorBoolResponse valorBoolResponse = new CedServicios.Entidades.Response.ValorBoolResponse();
        //    try
        //    {
        //        CedServicios.Entidades.Sesion sesion;
        //        sesion = ObtenerSesion();
        //        CedServicios.Entidades.Persona persona = new CedServicios.Entidades.Persona();
        //        persona.RazonSocial = personaCrearRequest.RazonSocial;
        //        valorBoolResponse = CedServicios.RN.Persona.Crear(persona, sesion);
        //    }
        //    catch (Exception ex)
        //    {
        //        valorBoolResponse.Respuesta = CedServicios.RN.Respuesta.ExceptionToRespuesta(ex);
        //    }
        //    yield return valorBoolResponse;
        //}

        /// <summary>
        /// Consultar la lista de condición de IVA.
        /// </summary>
        [HttpGet]
        [Route("CondicionIVALista")]
        public IEnumerable<List<FeaEntidades.CondicionesIVA.CondicionIVA>> CondicionIVALista()
        {
            yield return FeaEntidades.CondicionesIVA.CondicionIVA.Lista();
        }

        /// <summary>
        /// Consultar la lista de condición de Ingresos Brutos.
        /// </summary>
        [HttpGet]
        [Route("CondicionIBLista")]
        public IEnumerable<List<FeaEntidades.CondicionesIB.CondicionIB>> CondicionIBLista()
        {
            yield return FeaEntidades.CondicionesIB.CondicionIB.Lista();
        }

        /// <summary>
        /// Consultar la lista de Tipos de Documento.
        /// </summary>
        [HttpGet]
        [Route("TiposDeDocumentoLista")]
        public IEnumerable<List<FeaEntidades.Documentos.Documento>> TiposDeDocumentoLista()
        {
            yield return FeaEntidades.Documentos.Documento.Lista();
        }

        /// <summary>
        /// Consultar la lista de Provincias.
        /// </summary>
        [HttpGet]
        [Route("ProvinciasLista")]
        public IEnumerable<List<FeaEntidades.CodigosProvincia.CodigoProvincia>> ProvinciasLista()
        {
            yield return FeaEntidades.CodigosProvincia.CodigoProvincia.Lista();
        }

    }
}
