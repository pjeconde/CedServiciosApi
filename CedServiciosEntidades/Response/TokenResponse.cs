using System;
using System.Collections.Generic;
using System.Text;
using CedServicios.Entidades;

namespace CedServicios.Entidades.Response
{
    public class TokenResponse
    {
        private Respuesta respuesta;
        private string token;

        public TokenResponse()
        {
            respuesta = new Respuesta();
            token = "";
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
        public string Token
        {
            set
            {
                token = value;
            }
            get
            {
                return token;
            }
        }
    }
}
