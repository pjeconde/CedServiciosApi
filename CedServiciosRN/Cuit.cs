﻿using System;
using System.Collections.Generic;
using CedServicios;

namespace CedServicios.RN
{
    public class Cuit
    {
        public static List<Entidades.Cuit> LeerListaCuitsPorUsuario(Entidades.Sesion Sesion)
        {
            CedServicios.DB.Cuit db = new DB.Cuit(Sesion);
            return db.LeerListaCuitsPorUsuario();
        }
        public static void Leer(Entidades.Cuit Cuit, Entidades.Sesion Sesion)
        {
            CedServicios.DB.Cuit db = new DB.Cuit(Sesion);
            db.Leer(Cuit);
        }
        public static void CompletarUNsYPuntosVta(List<Entidades.Cuit> Cuits, Entidades.Sesion Sesion)
        {
            CedServicios.DB.Cuit db = new DB.Cuit(Sesion);
            db.CompletarUNsYPuntosVta(Cuits);
        }
        public static void Crear(Entidades.Cuit Cuit, List<string> Servicios, Entidades.Sesion Sesion)
        {
            //Validar
            if (Servicios.Count == 0) throw new CedServicios.EX.Cuit.NingunServicioSeleccionado();
            string servicio = Servicios.Find(delegate(string s) {return s == "eFact";});
            if (servicio != null && !Cuit.DestinoComprobanteAFIP && !Cuit.DestinoComprobanteITF)
            {
                throw new CedServicios.EX.Cuit.NingunDestinoComprobanteSeleccionado();
            }
            //Crear
            string permisoAdminCUITParaUsuarioAprobadoHandler = RN.Permiso.PermisoAdminCUITParaUsuarioAprobadoHandler(Cuit, Sesion);
            string servxCUITAprobadoHandler = String.Empty;
            for (int i = 0; i < Servicios.Count; i++)
            {
                servxCUITAprobadoHandler += RN.Permiso.ServxCUITAprobadoHandler(Cuit, new Entidades.TipoPermiso(Servicios[i]), new DateTime(2062, 12, 31), Sesion);
            }
            DB.UN dbUN = new DB.UN(Sesion);
            Entidades.UN uN = new Entidades.UN();
            uN.Cuit = Cuit.Nro;
            uN.Id = 1;
            uN.Descr = "Predefinida";
            uN.WF.Estado = "Vigente";
            string crearUNHandler = dbUN.CrearHandler(uN, true);
            string permisoUsoCUITxUNAprobadoHandler = RN.Permiso.PermisoUsoCUITxUNAprobadoHandler(uN, Sesion);
            string permisoOperServUNParaUsuarioAprobadoHandler = String.Empty;
            for (int i = 0; i < Servicios.Count; i++)
            {
                permisoOperServUNParaUsuarioAprobadoHandler += RN.Permiso.PermisoOperServUNParaUsuarioAprobadoHandler(uN, new Entidades.TipoPermiso(Servicios[i]), new DateTime(2062, 12, 31), Sesion);
            }
            DB.Cuit db = new DB.Cuit(Sesion);
            Cuit.WF.Estado = "Vigente";
           // db.Crear(Cuit, permisoAdminCUITParaUsuarioAprobadoHandler, servxCUITAprobadoHandler, crearUNHandler, permisoUsoCUITxUNAprobadoHandler, permisoAdminUNParaUsuarioAprobadoHandler, permisoOperServUNParaUsuarioAprobadoHandler);
        }
        public static void Modificar(Entidades.Cuit Cuit, Entidades.Sesion Sesion)
        {
            DB.Cuit db = new DB.Cuit(Sesion);
            db.Modificar(Sesion.Cuit, Cuit);
            Sesion.Cuit = Cuit;
        }
        public static void CambiarEstado(Entidades.Cuit Cuit, string Estado, Entidades.Sesion Sesion)
        {
            DB.Cuit db = new DB.Cuit(Sesion);
            db.CambiarEstado(Cuit, Estado);
            Cuit.WF.Estado = Estado;
        }
        public static Entidades.Cuit ObtenerCopia(Entidades.Cuit Desde)
        {
            Entidades.Cuit hasta = new Entidades.Cuit();
            hasta.Contacto.Nombre = Desde.Contacto.Nombre;
            hasta.Contacto.Telefono = Desde.Contacto.Telefono;
            hasta.Contacto.Email = Desde.Contacto.Email;
            hasta.DatosIdentificatorios.GLN = Desde.DatosIdentificatorios.GLN;
            hasta.DatosIdentificatorios.CodigoInterno = Desde.DatosIdentificatorios.CodigoInterno;
            hasta.DatosImpositivos.DescrCondIngBrutos = Desde.DatosImpositivos.DescrCondIngBrutos;
            hasta.DatosImpositivos.DescrCondIVA = Desde.DatosImpositivos.DescrCondIVA;
            hasta.DatosImpositivos.FechaInicioActividades = Desde.DatosImpositivos.FechaInicioActividades;
            hasta.DatosImpositivos.IdCondIngBrutos = Desde.DatosImpositivos.IdCondIngBrutos;
            hasta.DatosImpositivos.IdCondIVA = Desde.DatosImpositivos.IdCondIVA;
            hasta.DatosImpositivos.NroIngBrutos = Desde.DatosImpositivos.NroIngBrutos;
            hasta.Domicilio.Calle = Desde.Domicilio.Calle;
            hasta.Domicilio.CodPost = Desde.Domicilio.CodPost;
            hasta.Domicilio.Depto = Desde.Domicilio.Depto;
            hasta.Domicilio.Localidad = Desde.Domicilio.Localidad;
            hasta.Domicilio.Manzana = Desde.Domicilio.Manzana;
            hasta.Domicilio.Nro = Desde.Domicilio.Nro;
            hasta.Domicilio.Piso = Desde.Domicilio.Piso;
            hasta.Domicilio.Provincia.Id = Desde.Domicilio.Provincia.Id;
            hasta.Domicilio.Provincia.Descr = Desde.Domicilio.Provincia.Descr;
            hasta.Domicilio.Sector = Desde.Domicilio.Sector;
            hasta.Domicilio.Torre = Desde.Domicilio.Torre;
            hasta.Medio.IdMedio = Desde.Medio.IdMedio;
            hasta.Medio.DescrMedio = Desde.Medio.DescrMedio;
            hasta.Nro = Desde.Nro;
            hasta.DestinoComprobanteAFIP = Desde.DestinoComprobanteAFIP;
            hasta.UsaCertificadoAFIPPropio = Desde.UsaCertificadoAFIPPropio;
            hasta.DestinoComprobanteITF = Desde.DestinoComprobanteITF;
            hasta.NroSerieCertifITF = Desde.NroSerieCertifITF;
            hasta.RazonSocial = Desde.RazonSocial;
            hasta.UltActualiz = Desde.UltActualiz;
            for (int i = 0; i < Desde.UNs.Count; i++)
            {
                Entidades.UN uN = new Entidades.UN();
                uN.Cuit = Desde.UNs[i].Cuit;
                uN.Descr = Desde.UNs[i].Descr;
                uN.Id = Desde.UNs[i].Id;
                for (int j = 0; j < Desde.UNs[i].PuntosVta.Count; j++)
                {
                    Entidades.PuntoVta puntoVta = new Entidades.PuntoVta();
                    puntoVta.Contacto.Nombre = Desde.UNs[i].PuntosVta[j].Contacto.Nombre;
                    puntoVta.Contacto.Telefono = Desde.UNs[i].PuntosVta[j].Contacto.Telefono;
                    puntoVta.Contacto.Email = Desde.UNs[i].PuntosVta[j].Contacto.Email;
                    puntoVta.Cuit = Desde.UNs[i].PuntosVta[j].Cuit;
                    puntoVta.DatosIdentificatorios.GLN = Desde.UNs[i].PuntosVta[j].DatosIdentificatorios.GLN;
                    puntoVta.DatosIdentificatorios.CodigoInterno = Desde.UNs[i].PuntosVta[j].DatosIdentificatorios.CodigoInterno;
                    puntoVta.DatosImpositivos.DescrCondIngBrutos = Desde.UNs[i].PuntosVta[j].DatosImpositivos.DescrCondIngBrutos;
                    puntoVta.DatosImpositivos.DescrCondIVA = Desde.UNs[i].PuntosVta[j].DatosImpositivos.DescrCondIVA;
                    puntoVta.DatosImpositivos.FechaInicioActividades = Desde.UNs[i].PuntosVta[j].DatosImpositivos.FechaInicioActividades;
                    puntoVta.DatosImpositivos.IdCondIngBrutos = Desde.UNs[i].PuntosVta[j].DatosImpositivos.IdCondIngBrutos;
                    puntoVta.DatosImpositivos.IdCondIVA = Desde.UNs[i].PuntosVta[j].DatosImpositivos.IdCondIVA;
                    puntoVta.DatosImpositivos.NroIngBrutos = Desde.UNs[i].PuntosVta[j].DatosImpositivos.NroIngBrutos;
                    puntoVta.Domicilio.Calle = Desde.UNs[i].PuntosVta[j].Domicilio.Calle;
                    puntoVta.Domicilio.CodPost = Desde.UNs[i].PuntosVta[j].Domicilio.CodPost;
                    puntoVta.Domicilio.Depto = Desde.UNs[i].PuntosVta[j].Domicilio.Depto;
                    puntoVta.Domicilio.Localidad = Desde.UNs[i].PuntosVta[j].Domicilio.Localidad;
                    puntoVta.Domicilio.Manzana = Desde.UNs[i].PuntosVta[j].Domicilio.Manzana;
                    puntoVta.Domicilio.Nro = Desde.UNs[i].PuntosVta[j].Domicilio.Nro;
                    puntoVta.Domicilio.Piso = Desde.UNs[i].PuntosVta[j].Domicilio.Piso;
                    puntoVta.Domicilio.Provincia.Id = Desde.UNs[i].PuntosVta[j].Domicilio.Provincia.Id;
                    puntoVta.Domicilio.Provincia.Descr = Desde.UNs[i].PuntosVta[j].Domicilio.Provincia.Descr;
                    puntoVta.Domicilio.Sector = Desde.UNs[i].PuntosVta[j].Domicilio.Sector;
                    puntoVta.Domicilio.Torre = Desde.UNs[i].PuntosVta[j].Domicilio.Torre;
                    puntoVta.IdMetodoGeneracionNumeracionLote = Desde.UNs[i].PuntosVta[j].IdMetodoGeneracionNumeracionLote;
                    puntoVta.IdTipoPuntoVta = Desde.UNs[i].PuntosVta[j].IdTipoPuntoVta;
                    puntoVta.IdUN = Desde.UNs[i].PuntosVta[j].IdUN;
                    puntoVta.Nro = Desde.UNs[i].PuntosVta[j].Nro;
                    puntoVta.UltActualiz = Desde.UNs[i].PuntosVta[j].UltActualiz;
                    puntoVta.UltNroLote = Desde.UNs[i].PuntosVta[j].UltNroLote;
                    puntoVta.UsaSetPropioDeDatosCuit = Desde.UNs[i].PuntosVta[j].UsaSetPropioDeDatosCuit;
                    puntoVta.WF.Id = Desde.UNs[i].PuntosVta[j].WF.Id;
                    puntoVta.WF.Estado = Desde.UNs[i].PuntosVta[j].WF.Estado;
                    uN.PuntosVta.Add(puntoVta);
                }
                uN.UltActualiz = Desde.UNs[i].UltActualiz;
                uN.WF.Id = Desde.UNs[i].WF.Id;
                uN.WF.Estado = Desde.UNs[i].WF.Estado;
                hasta.UNs.Add(uN);
            }
            hasta.WF.Id = Desde.WF.Id;
            hasta.WF.Estado = hasta.WF.Estado;
            return hasta;
        }
        public static List<Entidades.Cuit> ListaSegunFiltros(string Cuit, string RazonSocial, string Localidad, string Estado, Entidades.Sesion Sesion)
        {
            DB.Cuit cuit = new DB.Cuit(Sesion);
            return cuit.ListaSegunFiltros(Cuit, RazonSocial, Localidad, Estado);
        }
        public static List<Entidades.Cuit> ListaPaging(out int CantidadFilas, int IndicePagina, string OrderBy, string Cuit, string RazonSocial, string Localidad, string Estado, string SessionID, Entidades.Sesion Sesion)
        {
            List<Entidades.Cuit> listaCuit = new List<Entidades.Cuit>();
            DB.Cuit db = new DB.Cuit(Sesion);
            if (OrderBy.Equals(String.Empty))
            {
                OrderBy = "Cuit asc";
            }
            listaCuit = db.ListaSegunFiltros(Cuit, RazonSocial, Localidad, Estado);
            int cantidadFilas = listaCuit.Count;
            CantidadFilas = cantidadFilas;
            return db.ListaPaging(IndicePagina, OrderBy, SessionID, listaCuit);
        }
    }
}