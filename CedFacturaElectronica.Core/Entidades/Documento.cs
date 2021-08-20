using System;

namespace CedFacturaElectronica.Core.Entidades
{
     public class Documento
     {
        public int Id { get; set; }
        public int Numero { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public bool Activo { get; set; }
        public int TipoDocumentoId { get; set; }

        public TipoDocumento TipoDocumento { get; set; }

        public Documento()
        {
            TipoDocumento = new TipoDocumento();
        }
     }
    
}