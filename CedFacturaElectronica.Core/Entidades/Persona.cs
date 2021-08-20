using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CedFacturaElectronica.Core.Entidades
{
    public class Persona
    {
        public int Id { get; set; }
        public string RazonSocial { get; set; }
        public int DocumentoId { get; set; }
        public Documento Documento { get; set; }
        public string Cuit { get; set; }
        public int DesambiguacionCuitPais { get; set; }
        public int ContactoId { get; set; }
        public Contacto Contacto { get; set; }
        public int DatosImpositivosId { get; set; }
        public DatosImpositivos DatosImpositivos { get; set; }
        public bool EsCliente { get; set; }
        public bool EsProveedor { get; set; }
        public int DatosIdentificatoriosId { get; set; }
        public DatosIdentificatorios DatosIdentificatorios { get; set; }
        public string EmailAvisoVisualizacion { get; set; }
        public string PasswordAvisoVisualizacion { get; set; }
        public int WfId { get; set; }
        public WF Wf { get; set; }
        public string UltActualiz { get; set; }
        public int Orden { get; set; }
        public int DatosEmailAvisoComprobantePersonaId { get; set; }
        public DatosEmailAvisoComprobantePersona DatosEmailAvisoComprobantePersona { get; set; }
        public string IdListaPrecioVenta { get; set; }
        public string IdListaPrecioCompra { get; set; }

        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public bool Activo { get; set; }
    }
}
