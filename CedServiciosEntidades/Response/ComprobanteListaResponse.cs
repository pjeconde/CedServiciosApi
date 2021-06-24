using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CedServicios.Entidades.Response
{
    [Serializable]
    public class ComprobanteListaResponse
    {
        private Respuesta respuesta;
        private PaginacionResponse paginacion;
        private List<CedServicios.Entidades.Comprobante> comprobantes;
        public ComprobanteListaResponse()
        {
            respuesta = new Respuesta();
            paginacion = new PaginacionResponse();
            comprobantes = new List<CedServicios.Entidades.Comprobante>();
        }
        public Respuesta Respuesta
        {
            set
            {
                respuesta = value;
            }
            get
            {
                return respuesta;
            }
        }
        public PaginacionResponse Paginacion
        {
            set
            {
                paginacion = value;
            }
            get
            {
                return paginacion;
            }
        }
        public List<CedServicios.Entidades.Comprobante> Comprobantes
        {
            set
            {
                comprobantes = value;
            }
            get
            {
                return comprobantes;
            }
        }
    }
}