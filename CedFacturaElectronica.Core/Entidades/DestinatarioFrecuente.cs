using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CedFacturaElectronica.Core.Entidades
{
    public class DestinatarioFrecuente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Cc { get; set; }

        public DestinatarioFrecuente()
        {
        }

        public DestinatarioFrecuente(int id, string nombre, string cc)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Cc = cc;
        }
    }
}
