using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CedFacturaElectronica.Core.Entidades
{
    public class Domicilio
    {
        public int Id { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Departamento { get; set; }
        public string Sector { get; set; }
        public string Torre { get; set; }
        public string Manzana { get; set; }
        public string Localidad { get; set; }
        public string CodPost { get; set; }
        public int IdProvincia { get; set; }
        public Provincia Provincia { get; set; }

        public Domicilio()
        {
            Provincia = new Provincia();
        }
    }
}
