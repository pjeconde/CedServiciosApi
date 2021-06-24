using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CedServicios.Entidades.Request
{
    /// <summary>
    /// Ninguna propiedad es obligatoria.
    /// </summary>
    public class UsuarioCambiarClaveConPreguntaSegRequest
    {
        private string idUsuario;
        private string email;
        private string pregunta;
        private string respuesta;
        private string claveActual;
        private string claveNueva;
        private string claveNuevaConfirmacion;
        public UsuarioCambiarClaveConPreguntaSegRequest()
        {
           
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
        public string Pregunta
        {
            set
            {
                pregunta = value;
            }
            get
            {
                return pregunta;
            }
        }
        public string Respuesta
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
        public string ClaveActual
        {
            set
            {
                claveActual = value;
            }
            get
            {
                return claveActual;
            }
        }
        public string ClaveNueva
        {
            set
            {
                claveNueva = value;
            }
            get
            {
                return claveNueva;
            }
        }
        public string ClaveNuevaConfirmacion
        {
            set
            {
                claveNuevaConfirmacion = value;
            }
            get
            {
                return claveNuevaConfirmacion;
            }
        }
    }
}
