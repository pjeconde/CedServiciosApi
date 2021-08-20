using System;

namespace CedFacturaElectronica.Core.Entidades
{
    public class TipoDocumento
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public bool Activo { get; set; }
    }
}