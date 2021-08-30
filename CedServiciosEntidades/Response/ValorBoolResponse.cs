using System;
using System.Collections.Generic;
using System.Text;
using CedServicios.Entidades;

namespace CedServicios.Entidades.Response
{
    public class ValorBoolResponse
    {
        private Respuesta respuesta;
        private bool valor;

        public ValorBoolResponse()
        {
            respuesta = new Respuesta();
            valor = false;
        }
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
        public bool Valor
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
