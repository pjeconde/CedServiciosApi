using System;
using System.Collections.Generic;
using System.Text;
using CedServicios.Entidades;

namespace CedServicios.Entidades.Response
{
    public class UsuarioResponse
    {
        private Respuesta respuesta;
        private Usuario usuario;
        
        public UsuarioResponse()
        {
            respuesta = new Respuesta();
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
        public Usuario Usuario
        {
            set
            {
                usuario = value;
            }
            get
            {
                return usuario;
            }
        }
    }
}
