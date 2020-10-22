using System;
using System.Collections.Generic;

namespace CedServicios.Entidades
{
    [Serializable]
    public class Respuesta
    {
        public Resultado Resultado { get; set; }
        public List<Resultado> Detalle { get; set; }

        public Respuesta()
        {
            Resultado = new Resultado();
            Detalle = new List<Resultado>();
        }


    }
}