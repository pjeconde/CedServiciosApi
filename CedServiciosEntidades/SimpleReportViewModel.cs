using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CedServicios.Entidades
{
    public class SimpleReportViewModel
    {
        [Required]
        public string DimensionOne { get; set; }
        public int Quantity { get; set; }
    }
}
