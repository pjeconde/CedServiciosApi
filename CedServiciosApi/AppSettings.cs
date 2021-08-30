using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CedServiciosApi
{
    public class AppSettings
    {
        public string Ambiente { get; set; }
        public string CnnStr { get; set; }
        public string DBUsuario { get; set; }
        public string DBPassword { get; set; }
    }
}
