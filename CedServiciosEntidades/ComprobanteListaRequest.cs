using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CedServicios.Entidades
{
    /// <summary>
    /// Ninguna propiedad es obligatoria.
    /// </summary>
    public class ComprobanteListaRequest
    {
        private int pagina;
        private string orderBy;

        private List<CedServicios.Entidades.Estado> estados;
        private List<CedServicios.Entidades.Estado> estadosCompra;
        private List<FeaEntidades.TiposDeComprobantes.TipoComprobante> tiposComprobante;
        private List<CedServicios.Entidades.PersonaRequest> personas;
        private CedServicios.Entidades.NaturalezaComprobante naturalezaComprobante;
        private string fechaDsd;
        private string fechaHst;
        private string puntoDeVenta;
        private string numeroDeComprobante;
        private string detalle;

        public ComprobanteListaRequest()
        {
            estados = new List<CedServicios.Entidades.Estado>();
            estadosCompra = new List<CedServicios.Entidades.Estado>();
            tiposComprobante = new List<FeaEntidades.TiposDeComprobantes.TipoComprobante>();
            personas = new List<CedServicios.Entidades.PersonaRequest>();
            naturalezaComprobante = new CedServicios.Entidades.NaturalezaComprobante(); 
        }

        public int Pagina
        {
            set
            {
                pagina = value;
            }
            get
            {
                return pagina;
            }
        }
        public string OrderBy
        {
            set
            {
                orderBy = value;
            }
            get
            {
                return orderBy;
            }
        }
        /// <summary>
        /// No es obligatorio informar la lista de Estados Comprobantes Ventas.
        /// </summary>
        public List<CedServicios.Entidades.Estado> Estados
        {
            set
            {
                estados = value;
            }
            get
            {
                return estados;
            }
        }
        /// <summary>
        /// No es obligatorio informar la lista de Estados Comprobantes Compras.
        /// </summary>
        public List<CedServicios.Entidades.Estado> EstadosCompra
        {
            set
            {
                estadosCompra = value;
            }
            get
            {
                return estadosCompra;
            }
        }
        /// <summary>
        /// No es obligatorio informar la lista de Tipos de Comprobantes.
        /// </summary>
        public List<FeaEntidades.TiposDeComprobantes.TipoComprobante> TiposComprobante
        {
            set
            {
                tiposComprobante = value;
            }
            get
            {
                return tiposComprobante;
            }
        }
        /// <summary>
        /// No es obligatorio informar la lista de Personas.
        /// </summary>
        public List<CedServicios.Entidades.PersonaRequest> Personas
        {
            set
            {
                personas = value;
            }
            get
            {
                return personas;
            }
        }
        public CedServicios.Entidades.NaturalezaComprobante NaturalezaComprobante
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
        public string FechaDsd
        {
            set
            {
                fechaDsd = value;
            }
            get
            {
                return fechaDsd;
            }
        }
        public string FechaHst
        {
            set
            {
                fechaHst = value;
            }
            get
            {
                return fechaHst;
            }
        }
        public string PuntoDeVenta
        {
            set
            {
                puntoDeVenta = value;
            }
            get
            {
                return puntoDeVenta;
            }
        }
        public string NumeroDeComprobante
        {
            set
            {
                numeroDeComprobante = value;
            }
            get
            {
                return numeroDeComprobante;
            }
        }
        public string Detalle
        {
            set
            {
                detalle = value;
            }
            get
            {
                return detalle;
            }
        }
    }
}
