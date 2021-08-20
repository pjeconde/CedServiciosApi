using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CedFacturaElectronica.Core.Entidades
{
    public class DatosEmailAvisoComprobantePersona
    {
        public int Id { get; set; }
        public bool Activo { get; set; }
        public string Destinatario { get; set; }
        public string CopiaOculta { get; set; }
        public string Asunto { get; set; }
        public string Cuerpo { get; set; }
        public List<DestinatarioFrecuente> DestinatariosFrecuentes { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }

        public DatosEmailAvisoComprobantePersona()
        {
            DestinatariosFrecuentes = new List<DestinatarioFrecuente>();
        }

        public DatosEmailAvisoComprobantePersona(bool activo, string destinatario, string copiaOculta, string asunto, string cuerpo, List<DestinatarioFrecuente> destinatariosFrecuentes)
        {
            this.Activo = activo;
            this.Destinatario = destinatario;
            this.CopiaOculta = copiaOculta;
            this.Asunto = asunto;
            this.Cuerpo = cuerpo;
            this.DestinatariosFrecuentes = destinatariosFrecuentes;
        }
    }
}
