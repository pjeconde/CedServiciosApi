using System;
using System.Collections.Generic;

namespace CedServicios.Entidades
{
    [Serializable]
    public class Respuesta
    {
        private Resultado resultado;
        private List<Resultado> detalle;

        public Respuesta()
        {
            resultado = new Resultado();
            detalle = new List<Resultado>();
        }
        public Resultado Resultado
        {
            get
            {
                return resultado;
            }
            set
            {
                resultado = value;
            }
        }
        public List<Resultado> Detalle
        {
            get
            {
                return detalle;
            }
            set
            {
                detalle = value;
            }
        }
    }
}