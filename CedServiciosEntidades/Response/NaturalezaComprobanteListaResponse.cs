using System;
using System.Collections.Generic;
using System.Text;

namespace CedServicios.Entidades.Response
{
    public class NaturalezaComprobanteListaResponse
    {
        private Respuesta respuesta;
        private List<NaturalezaComprobante> naturalezaComprobante;
        public NaturalezaComprobanteListaResponse()
        {
            respuesta = new Respuesta();
            naturalezaComprobante = new List<NaturalezaComprobante>();
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
        public List<NaturalezaComprobante> NaturalezaComprobanteLista
        {
            set
            {
                naturalezaComprobante = value;
            }
            get
            {
                return naturalezaComprobante;
            }
        }
    }
}
