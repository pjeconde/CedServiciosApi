using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CedServicios.Entidades.Request
{
    [Serializable]
    public class PersonaCrearRequest
    {
        private string cuit;
        private Documento documento;
        private string idPersona;
        private int desambiguacionCuitPais;
        private string razonSocial;
        private Domicilio domicilio;
        private Contacto contacto;
        private DatosImpositivos datosImpositivos;
        private bool esCliente;
        private bool esProveedor;

        public PersonaCrearRequest()
        {
            documento = new Documento();
            domicilio = new Domicilio();
        }

        public string Cuit
        {
            set
            {
                cuit = value;
            }
            get
            {
                return cuit;
            }
        }
        public Documento Documento
        {
            set
            {
                documento = value;
            }
            get
            {
                return documento;
            }
        }
        public string IdPersona
        {
            set
            {
                idPersona = value;
            }
            get
            {
                return idPersona;
            }
        }
        public string RazonSocial
        {
            set
            {
                razonSocial = value;
            }
            get
            {
                return razonSocial;
            }
        }
        public Domicilio Domicilio
        {
            set
            {
                domicilio = value;
            }
            get
            {
                return domicilio;
            }
        }
        public Contacto Contacto
        {
            set
            {
                contacto = value;
            }
            get
            {
                return contacto;
            }
        }
        public DatosImpositivos DatosImpositivos
        {
            set
            {
                datosImpositivos = value;
            }
            get
            {
                return datosImpositivos;
            }
        }
        public bool EsCliente
        {
            set
            {
                esCliente = value;
            }
            get
            {
                return esCliente;
            }
        }
        public bool EsProveedor
        {
            set
            {
                esProveedor = value;
            }
            get
            {
                return esProveedor;
            }
        }
    }
}
