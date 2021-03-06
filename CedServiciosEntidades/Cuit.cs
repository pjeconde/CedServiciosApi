﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CedServicios.Entidades
{
    [Serializable]
    public class Cuit
    {
        private string nro;
        private string razonSocial;
        private Domicilio domicilio;
        private Contacto contacto;
        private DatosImpositivos datosImpositivos;
        private DatosIdentificatorios datosIdentificatorios;
        private Medio medio;
        private WF wF;
        private string ultActualiz;
        private bool destinoComprobanteAFIP;
        private bool usaCertificadoAFIPPropio;
        private bool destinoComprobanteITF;
        private string nroSerieCertifITF;
        private List<UN> uNs;

        public Cuit()
        {
            domicilio = new Domicilio();
            contacto = new Contacto();
            datosImpositivos = new DatosImpositivos();
            datosIdentificatorios = new DatosIdentificatorios();
            medio = new Medio();
            wF = new WF();
            uNs = new List<UN>();
        }

        public string Nro
        {
            set
            {
                nro = value;
            }
            get
            {
                return nro;
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
        public DatosIdentificatorios DatosIdentificatorios
        {
            set
            {
                datosIdentificatorios = value;
            }
            get
            {
                return datosIdentificatorios;
            }
        }
        public Medio Medio
        {
            set
            {
                medio = value;
            }
            get
            {
                return medio;
            }
        }
        public WF WF
        {
            set
            {
                wF = value;
            }
            get
            {
                return wF;
            }
        }
        public List<UN> UNs
        {
            set
            {
                uNs = value;
            }
            get
            {
                return uNs;
            }
        }
        public string UltActualiz
        {
            set
            {
                ultActualiz = value;
            }
            get
            {
                return ultActualiz;
            }
        }
        public bool DestinoComprobanteAFIP
        {
            set
            {
                destinoComprobanteAFIP = value;
            }
            get
            {
                return destinoComprobanteAFIP;
            }
        }
        public bool UsaCertificadoAFIPPropio
        {
            set
            {
                usaCertificadoAFIPPropio = value;
            }
            get
            {
                return usaCertificadoAFIPPropio;
            }
        }
        public bool DestinoComprobanteITF
        {
            set
            {
                destinoComprobanteITF = value;
            }
            get
            {
                return destinoComprobanteITF;
            }
        }
        public string NroSerieCertifITF
        {
            set
            {
                nroSerieCertifITF = value;
            }
            get
            {
                return nroSerieCertifITF;
            }
        }

        public UN TraerUN(int IdUN)
        {
            return uNs.Find(delegate(Entidades.UN p) { return p.Id == IdUN; });
        }
        public List<Entidades.DestinoComprobante> DestinosComprobante()
        {
            List<Entidades.DestinoComprobante> destinosComprobante = new List<DestinoComprobante>();
            if (destinoComprobanteAFIP)
            {
                destinosComprobante.Add(new DestinoComprobante("AFIP", "AFIP"));
            }
            if (destinoComprobanteITF)
            {
                destinosComprobante.Add(new DestinoComprobante("ITF", "Interbanking (Interfacturas)"));
            }
            return destinosComprobante;
        }
        #region Propiedades redundantes
        public string Estado
        {
            get
            {
                return wF.Estado;
            }
        }
        public string DomicilioCalle
        {
            get
            {
                return domicilio.Calle;
            }
        }
        public string DomicilioNro
        {
            get
            {
                return domicilio.Nro;
            }
        }
        public string DomicilioPiso
        {
            get
            {
                return domicilio.Piso;
            }
        }
        public string DomicilioDepto
        {
            get
            {
                return domicilio.Depto;
            }
        }
        public string DomicilioLocalidad
        {
            get
            {
                return domicilio.Localidad;
            }
        }
        public string DomicilioDescrProvincia
        {
            get
            {
                return domicilio.Provincia.Descr;
            }
        }
        public string DatosImpositivosDescrCondIVA
        {
            get
            {
                return datosImpositivos.DescrCondIVA;
            }
        }
        public string DatosImpositivosDescrCondIngBrutos
        {
            get
            {
                return datosImpositivos.DescrCondIngBrutos;
            }
        }

        public string NroYRazSoc
        {
            get
            {
                return nro + "-" + razonSocial;
            }
        }
        public string NroYRazSocCorta
        {
            get
            {
                if (razonSocial == null || razonSocial.Length <= 20)
                {
                    return nro + "-" + razonSocial;
                }
                else
                {
                    return nro + "-" + razonSocial.Substring(0, 20);
                }
            }
        }
        #endregion

    }
}