using System;
using System.Collections.Generic;
using System.Text;
using CedServicios.Entidades;

namespace CedServicios.Entidades.Response
{
    public class ValorStringResponse
    {
        private Respuesta respuesta;
        private string valor;
        public Respuesta Respuesta
        {
            set
            {
                respuesta = value;
            }
            get
            {
                return respuesta;
            }
        }
        public string Valor
        {
            set
            {
                valor = value;
            }
            get
            {
                return valor;
            }
        }
    }
}
