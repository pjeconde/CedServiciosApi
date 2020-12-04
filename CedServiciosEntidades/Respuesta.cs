using System;
using System.Collections.Generic;

namespace CedServicios.Entidades
{
    [Serializable]
    public class Respuesta
    {
        public RespuestaDetalle.SeveridadEnum Severidad { get; set; }
        public List<RespuestaDetalle> Detalle { get; set; }
        public object Objeto { get; set; }

        public Respuesta()
        {
            Detalle = new List<RespuestaDetalle>();
        }

    }
}