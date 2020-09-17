using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CedServicios.Entidades
{
    public class Medio
    {
        [Required]
        public string IdMedio { set; get; }
        public string DescrMedio { set; get; }
    }
}
