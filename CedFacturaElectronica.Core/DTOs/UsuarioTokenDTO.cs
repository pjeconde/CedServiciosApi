using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CedFacturaElectronica.Core.DTOs
{
    public class UsuarioTokenDTO
    {
        public string Token { get; set; }
        public DateTime FechaExpiracion { get; set; }
    }
}
