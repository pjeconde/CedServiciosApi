using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CedServicios.Entidades.Request
{

    /// <summary>
    /// Ninguna propiedad es obligatoria.
    /// </summary>
    public class ComprobanteListaRequest
    {
        private PaginacionRequest paginacion;
        //private List<CedServicios.Entidades.Estado> estados;
        //private List<CedServicios.Entidades.Estado> estadosCompra;
        //private string fechaDsd;
        //private string fechaHst;
        //private CedServicios.Entidades.NaturalezaComprobante naturalezaComprobante;
        //private string cuit;
        //private List<CedServicios.Entidades.TipoComprobante> tiposDeComprobante;
        //private string puntoDeVenta;
        //private string numeroDeComprobante;
        //private string detalle;

        public ComprobanteListaRequest()
        {
            paginacion = new PaginacionRequest();
            //estados = new List<CedServicios.Entidades.Estado>();
            //estadosCompra = new List<CedServicios.Entidades.Estado>();
            //naturalezaComprobante = new CedServicios.Entidades.NaturalezaComprobante();
            //tiposDeComprobante = new List<CedServicios.Entidades.TipoComprobante>();
        }

        public PaginacionRequest Paginacion
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
        ///// <summary>
        ///// No es obligatorio informar la lista de Estados Comprobantes Ventas.
        ///// </summary>
        //public List<CedServicios.Entidades.Estado> Estados
        //{
        //    set
        //    {
        //        estados = value;
        //    }
        //    get
        //    {
        //        return estados;
        //    }
        //}
        ///// <summary>
        ///// No es obligatorio informar la lista de Estados Comprobantes Compras.
        ///// </summary>
        //public List<CedServicios.Entidades.Estado> EstadosCompra
        //{
        //    set
        //    {
        //        estadosCompra = value;
        //    }
        //    get
        //    {
        //        return estadosCompra;
        //    }
        //}
        ///// <summary>
        ///// No es obligatorio informar la lista de Tipos de Comprobantes.
        ///// </summary>
        //public List<CedServicios.Entidades.TipoComprobante> TiposDeComprobante
        //{
        //    set
        //    {
        //        tiposDeComprobante = value;
        //    }
        //    get
        //    {
        //        return tiposDeComprobante;
        //    }
        //}
        ///// <summary>
        ///// No es obligatorio informar la lista de Personas.
        ///// </summary>
        //public string Cuit
        //{
        //    set
        //    {
        //        cuit = value;
        //    }
        //    get
        //    {
        //        return cuit;
        //    }
        //}
        //public CedServicios.Entidades.NaturalezaComprobante NaturalezaComprobante
        //{
        //    set
        //    {
        //        naturalezaComprobante = value;
        //    }
        //    get
        //    {
        //        return naturalezaComprobante;
        //    }
        //}
        //public string FechaDsd
        //{
        //    set
        //    {
        //        fechaDsd = value;
        //    }
        //    get
        //    {
        //        return fechaDsd;
        //    }
        //}
        //public string FechaHst
        //{
        //    set
        //    {
        //        fechaHst = value;
        //    }
        //    get
        //    {
        //        return fechaHst;
        //    }
        //}
        //public string PuntoDeVenta
        //{
        //    set
        //    {
        //        puntoDeVenta = value;
        //    }
        //    get
        //    {
        //        return puntoDeVenta;
        //    }
        //}
        //public string NumeroDeComprobante
        //{
        //    set
        //    {
        //        numeroDeComprobante = value;
        //    }
        //    get
        //    {
        //        return numeroDeComprobante;
        //    }
        //}
        //public string Detalle
        //{
        //    set
        //    {
        //        detalle = value;
        //    }
        //    get
        //    {
        //        return detalle;
        //    }
        //}
    }
}
