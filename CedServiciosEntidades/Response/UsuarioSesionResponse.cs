using System;
using System.Collections.Generic;
using System.Text;
using CedServicios.Entidades;

namespace CedServicios.Entidades.Response
{
    public class UsuarioSesionResponse : UsuarioResponse
    {
        private Sesion sesion;
        
        public UsuarioSesionResponse()
        {
            sesion = new Sesion();
        }
        public Sesion Sesion
        {
            set
            {
                sesion = value;
            }
            get
            {
                return sesion;
            }
        }
    }
}
