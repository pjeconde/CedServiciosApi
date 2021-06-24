using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CedServicios.Entidades.Request
{
    /// <summary>
    /// Todas las propiedad son obligatorias.
    /// </summary>
    public class ComprobanteVentaRequest
    {
        private string cuit;
        private int tipoComprobante;
        private int nroPuntoVta;
        private int nroComprobante;

        public string Cuit
        {
            set
            {
                cuit = value;
            }
            get
            {
                return cuit;
            }
        }
        public int TipoComprobante
        {
            set
            {
                tipoComprobante = value;
            }
            get
            {
                return tipoComprobante;
            }
        }
        public int NroPuntoVta
        {
            set
            {
                nroPuntoVta = value;
            }
            get
            {
                return nroPuntoVta;
            }
        }
        public int NroComprobante
        {
            set
            {
                nroComprobante = value;
            }
            get
            {
                return nroComprobante;
            }
        }
    }
}
