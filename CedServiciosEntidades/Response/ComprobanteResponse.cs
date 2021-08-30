using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;


namespace CedServicios.Entidades.Response
{
    [Serializable]
    public class ComprobanteResponse
    {
        private Respuesta respuesta;
        private Comprobante comprobante;
        public ComprobanteResponse()
        {
            respuesta = new Respuesta();
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
        public Comprobante Comprobante
        {
            set
            {
                comprobante = value;
            }
            get
            {
                return comprobante;
            }
        }
    }
}