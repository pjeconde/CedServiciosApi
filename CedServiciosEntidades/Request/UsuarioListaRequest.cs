using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CedServicios.Entidades.Request
{
    /// <summary>
    /// Ninguna propiedad es obligatoria.
    /// </summary>
    public class UsuarioListaRequest
    {
        private PaginacionRequest paginacion;
        private string idUsuario;
        private string nombre;
        private string email;
        private string estado;
        public UsuarioListaRequest()
        {
            paginacion = new PaginacionRequest();
        }
        public PaginacionRequest Paginacion
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
        public string IdUsuario
        {
            set
            {
                idUsuario = value;
            }
            get
            {
                return idUsuario;
            }
        }
        public string Nombre
        {
            set
            {
                nombre = value;
            }
            get
            {
                return nombre;
            }
        }
        public string Email
        {
            set
            {
                email = value;
            }
            get
            {
                return email;
            }
        }
        public string Estado
        {
            set
            {
                estado = value;
            }
            get
            {
                return estado;
            }
        }
    }
}
