using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CedServicios.Entidades
{
    public class StackedViewModel
    {
        [Required]
        public string StackedDimensionOne { get; set; }
        public List<SimpleReportViewModel> LstData { get; set; }
    }
}
