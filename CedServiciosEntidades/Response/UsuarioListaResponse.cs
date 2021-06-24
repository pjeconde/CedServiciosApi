using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CedServicios.Entidades.Response
{
    [Serializable]
    public class UsuarioListaResponse
    {
        private Respuesta respuesta;
        private PaginacionResponse paginacion;
        private List<Usuario> usuarios;
        public UsuarioListaResponse()
        {
            respuesta = new Respuesta();
            paginacion = new PaginacionResponse();
            usuarios = new List<Usuario>();
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
        public PaginacionResponse Paginacion
        {
            set
            {
                paginacion = value;
            }
            get
            {
                return paginacion;
            }
        }
        public List<Usuario> Usuarios
        {
            set
            {
                usuarios = value;
            }
            get
            {
                return usuarios;
            }
        }
    }
}
