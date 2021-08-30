using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CedServicios.Entidades
{
    [Serializable]
    public class UsuarioDatosBasicos
    {
        private string id;
        private string password;
        private string nombre;
        private string telefono;
        private string email;
        private string pregunta;
        private string respuesta;
        private string emailSMS;

        public UsuarioDatosBasicos()
        {
        }
        public string Id
        {
            set
            {
                id = value;
            }
            get
            {
                return id;
            }
        }
        public string Password
        {
            set
            {
                password = value;
            }
            get
            {
                return password;
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
        public string Telefono
        {
            set
            {
                telefono = value;
            }
            get
            {
                return telefono;
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
        public string EmailSMS
        {
            set
            {
                emailSMS = value;
            }
            get
            {
                return emailSMS;
            }
        }
    }
}
