using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CedFacturaElectronica.Core.Entidades
{
    public class Comprobante
    {
        // si o si ID // Fecha creacion/ Fecha actualizacion / Activo
        public int Id { get; set; }
        //identificacion comprobante
        public int TipoComprobanteId { get; set; }
        public TipoComprobante TipoComprobante { get; set; }
        public int NroPuntoVenta { get; set; }
        public long Nro { get; set; }
        public long NroLote { get; set; }
        //identificacion cliente
        public Documento Documento { get; set; }
        public int IdPersona { get; set; }
        public int DesambiguacionCuitPais { get; set; }
        public string RazonSocial { get; set; }
        //datos comprobante
        public string Detalle { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaVto { get; set; }
        public DateTime FechaAct { get; set; }
        public bool Activo { get; set; }
        public string Moneda { get; set; }
        public double ImporteMoneda { get; set; }
        public double TipoCambio { get; set; }
        public double Importe { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public string IdDestinoComprobante { get; set; }
        public WF Wf { get; set; }
        public string UltActualizacion { get; set; }
        public NaturalezaComprobante NaturalezaComprobante { get; set; }
        //campos adicionales opcionales
        public string CuitRazonSocial { get; set; }
        public DateTime FechaAlta { get; set; }
        public string PeriodicidadEmision { get; set; }
        public DateTime FechaProximaEmision { get; set; }
        public int CantidadComprobantesAEmitir { get; set; }
        public int CantidadComprobantesEmitidos { get; set; }
        public int CantidadDiasFechaVto { get; set; }
        public DatosEmailAvisoComprobanteContrato DatosEmailAvisoComprobanteContrato { get; set; }
        //Stock y contabilidad
        public List<ComprobanteDetalle> Minutas { get; set; }

        public Comprobante()
        {
            TipoComprobante = new TipoComprobante();
            Documento = new Documento();
            Wf = new WF();
            NaturalezaComprobante = new NaturalezaComprobante();
            DatosEmailAvisoComprobanteContrato = new DatosEmailAvisoComprobanteContrato();
            Minutas = new List<ComprobanteDetalle>();
        }
    }
}
