using System;
using System.Collections.Generic;
using System.Text;

namespace CedServicios.Entidades.Response
{
    public class TiposDeComprobanteResponse
    {
        private Respuesta respuesta;
        private List<TipoComprobante> tiposDeComprobante;
        public TiposDeComprobanteResponse()
        {
            respuesta = new Respuesta();
            tiposDeComprobante = new List<TipoComprobante>();
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
        public List<TipoComprobante> TiposDeComprobante
        {
            set
            {
                tiposDeComprobante = value;
            }
            get
            {
                return tiposDeComprobante;
            }
        }
    }
}
