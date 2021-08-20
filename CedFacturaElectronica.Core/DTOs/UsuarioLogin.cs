using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CedFacturaElectronica.Core.DTOs
{
    public class UsuarioLogin
    {
        public UsuarioInfoDTO UsuarioInfoDTO { get; set; }
        public UsuarioTokenDTO UsuarioTokenDTO { get; set; }
    }
}
