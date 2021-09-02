using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CedFacturaElectronica.Core.Entidades
{
    public class UsuarioAplicacion : IdentityUser
    {
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int CantidadEnviosMail { get; set; }
        public DateTime FechaUltimoReenvioMail { get; set; }
        public string Pregunta { get; set; }
        public string Respuesta { get; set; }
        public int IdWF { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public bool Activo { get; set; }


        public UsuarioAplicacion()
        {
            FechaCreacion = DateTime.Now;
            FechaModificacion = DateTime.Now;
            Activo = true;
        }
    }
}
