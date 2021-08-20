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
        //public string NombreCuenta { get; set; }
        //public int WfId { get; set; }
        //public WF Wf { get; set; }
        public string UltimaActualizacion { get; set; }
        public List<Permiso> Permisos { get; set; }
        public string CuitPredef { get; set; }
        public int IdUNPredef { get; set; }
        public string FechaOKeFactTyc { get; set; }
        private int cantidadFilasxPagina = 10;
        public bool MostrarAyudaComoPaginaDefault { get; set; }
        public int CantidadEnviosMail { get; set; }
        public DateTime FechaUltimoReenvioMail { get; set; }
        public string Pregunta { get; set; }
        public string Respuesta { get; set; }
        public string EmailSMS { get; set; }

        public int CantidadFilasXPagina
        {
            set
            {
                cantidadFilasxPagina = value;
            }
            get
            {
                return cantidadFilasxPagina;
            }
        }
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
