using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CedFacturaElectronica.Core.Entidades
{
    public class DatosImpositivos
    {
        public int Id { get; set; }
        public string DescripcionCondIVA { get; set; }
        public string NumeroIngresoBruto { get; set; }
        public int IdCondIngresoBruto { get; set; }
        public string DescCondIngresoBruto { get; set; }
        public DateTime FechaInicioActividad { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public bool Activo { get; set; }
    }
}
