﻿using System;
using System.Collections.Generic;
using CedServicios.Entidades;

namespace CedServicios.RN
{
    public class Persona
    {
        public static List<Entidades.Persona> ListaPorCuit(bool SoloVigentes, CedServicios.Entidades.Enum.TipoPersona TipoPersona, Entidades.Sesion Sesion)
        {
            DB.Persona db = new DB.Persona(Sesion);
            return db.ListaPorCuit(SoloVigentes, TipoPersona);
        }
        public static List<Entidades.Persona> ListaPorCuit(bool SoloVigentes, bool ParaCombo, CedServicios.Entidades.Enum.TipoPersona TipoPersona, Entidades.Sesion Sesion)
        {
            DB.Persona db = new DB.Persona(Sesion);
            return db.ListaPorCuit(SoloVigentes, ParaCombo, TipoPersona);
        }
        public static List<Entidades.Persona> ListaPorCuitContrato(bool DeBaja, bool ParaCombo, CedServicios.Entidades.Enum.TipoPersona TipoPersona, Entidades.Sesion Sesion)
        {
            DB.Persona db = new DB.Persona(Sesion);
            return db.ListaPorCuitContrato(DeBaja, ParaCombo, TipoPersona);
        }
        public static List<Entidades.Persona> ListaExportacion(Entidades.Usuario Cuenta, Entidades.Sesion Sesion, bool ConSeleccionarComprador)
        {
            DB.Persona comprador = new DB.Persona(Sesion);
            List<Entidades.Persona> lista = comprador.ListaPorCuit(true, CedServicios.Entidades.Enum.TipoPersona.Cliente);
            lista = lista.FindAll(delegate(Entidades.Persona c)
            {
                return c.Documento.Tipo.Id.Equals("70") || c.RazonSocial.Equals("Seleccionar cliente");
            });
            return lista;
        }
        public static List<Entidades.Persona> ListaSinExportacion(Entidades.Usuario Cuenta, Entidades.Sesion Sesion, bool ConSeleccionarComprador)
        {
            DB.Persona comprador = new DB.Persona(Sesion);
            List<Entidades.Persona> lista = comprador.ListaPorCuit(true, CedServicios.Entidades.Enum.TipoPersona.Cliente);
            lista = lista.FindAll(delegate(Entidades.Persona c)
            {
                return !c.Documento.Tipo.Id.Equals("70") || c.RazonSocial.Equals("Seleccionar cliente");
            });
            return lista;
        }
        public static List<Entidades.Persona> ListaTurismo(Entidades.Sesion Sesion)
        {
            DB.Persona comprador = new DB.Persona(Sesion);
            List<Entidades.Persona> lista = comprador.ListaPorCuit(true, CedServicios.Entidades.Enum.TipoPersona.Cliente);
            return lista;
        }
        public static List<Entidades.Persona> ListaPorCuityTipoyNroDoc(string Cuit, Entidades.Documento Documento, CedServicios.Entidades.Enum.TipoPersona TipoPersona, Entidades.Sesion Sesion)
        {
            DB.Persona db = new DB.Persona(Sesion);
            return db.ListaPorCuityTipoyNroDoc(Cuit, Documento, TipoPersona);
        }
        public static List<Entidades.Persona> ListaPorCuityRazonSocial(string Cuit, string Razonsocial, CedServicios.Entidades.Enum.TipoPersona TipoPersona, Entidades.Sesion Sesion)
        {
            DB.Persona db = new DB.Persona(Sesion);
            return db.ListaPorCuityRazonSocial(Cuit, Razonsocial, TipoPersona);
        }
        public static List<Entidades.Persona> ListaPorCuityIdPersona(string Cuit, string IdPersona, CedServicios.Entidades.Enum.TipoPersona TipoPersona, Entidades.Sesion Sesion)
        {
            DB.Persona db = new DB.Persona(Sesion);
            return db.ListaPorCuityIdPersona(Cuit, IdPersona, TipoPersona);
        }
        public static void Leer(Entidades.Persona Persona, Entidades.Sesion Sesion)
        {
            DB.Persona persona = new DB.Persona(Sesion);
            persona.Leer(Persona);
        }
        public static void LeerPorClavePrimaria(Entidades.Persona Persona, Entidades.Sesion Sesion)
        {
            DB.Persona persona = new DB.Persona(Sesion);
            persona.LeerPorClavePrimaria(Persona);
        }
        public static string LeerYSerializar(int IdWF, Entidades.Sesion Sesion)
        {
            DB.Persona comprador = new DB.Persona(Sesion);
            Entidades.Persona persona = new Entidades.Persona();
            persona = comprador.Leer(IdWF);
            return DB.Funciones.ObjetoSerializado(persona);
        }
        public static void Validar(Entidades.Persona Persona)
        {
            if (Persona.DatosEmailAvisoComprobantePersona.Activo)
            {
                if (Persona.DatosEmailAvisoComprobantePersona.De.Equals(string.Empty)) throw new Exception("Dirección de email, en campo 'De', sin informar");
                try
                {
                    RN.Funciones.ValidarListaDeMails(Persona.DatosEmailAvisoComprobantePersona.De);
                }
                catch
                {
                    throw new Exception("Dirección de email, en campo 'De', con formato inválido");
                }
                if (Persona.DatosEmailAvisoComprobantePersona.De.Split(',').Length != 1) throw new Exception("en campo 'De' debe consignarse una, y solo una, dirección de email, en campo 'De', con formato inválido");
                if (Persona.DatosEmailAvisoComprobantePersona.DestinatariosFrecuentes.Count == 0) throw new Exception("Destinatarios frecuentes no informados");
                if (!Persona.DatosEmailAvisoComprobantePersona.Cco.Equals(string.Empty))
                {
                    try
                    {
                        RN.Funciones.ValidarListaDeMails(Persona.DatosEmailAvisoComprobantePersona.Cco);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message + "º dirección de email, en campo 'Cco', con formato inválido");
                    }
                }
                if (Persona.DatosEmailAvisoComprobantePersona.Asunto.Equals(string.Empty)) throw new Exception("Asunto no informado");
                if (Persona.DatosEmailAvisoComprobantePersona.Cuerpo.Equals(string.Empty)) throw new Exception("Cuerpo no informado");
            }
        }
        public static Entidades.Response.ValorBoolResponse Crear(Entidades.Persona Persona, Entidades.Sesion Sesion)
        {
            Entidades.Response.ValorBoolResponse valorBoolResponse = new Entidades.Response.ValorBoolResponse();
            try
            {
                valorBoolResponse.Respuesta = ValidarCrear(Persona);
                if (valorBoolResponse.Respuesta.Resultado.Severidad == Resultado.SeveridadEnum.Ok)
                {
                    DB.Persona db = new DB.Persona(Sesion);
                    db.Crear(Persona);
                }
            }
            catch (Exception ex)
            {
                valorBoolResponse.Respuesta = Respuesta.ExceptionToRespuesta(ex);
            }
            return valorBoolResponse;
           
        }
        private static CedServicios.Entidades.Respuesta ValidarCrear(CedServicios.Entidades.Persona persona)
        {
            CedServicios.Entidades.Respuesta respuesta = new CedServicios.Entidades.Respuesta();
            Entidades.Resultado resultado = Respuesta.ValidarRequeridoString(persona.Cuit, (nameof(persona.Cuit)).ToLower());
            if (resultado.Severidad == Resultado.SeveridadEnum.Error) { respuesta.Detalle.Add(resultado); }
            resultado = Respuesta.ValidarRequeridoString(persona.Documento.Tipo.Id, (nameof(persona.Documento.Tipo.Id)).ToLower());
            if (resultado.Severidad == Resultado.SeveridadEnum.Error) { respuesta.Detalle.Add(resultado); }
            resultado = Respuesta.ValidarRequeridoString(persona.Documento.Nro, (nameof(persona.Documento.Nro)).ToLower());
            if (resultado.Severidad == Resultado.SeveridadEnum.Error) { respuesta.Detalle.Add(resultado); }

            resultado = Respuesta.ValidarRequeridoString(persona.RazonSocial, (nameof(persona.RazonSocial)).ToLower());
            if (resultado.Severidad == Resultado.SeveridadEnum.Error) { respuesta.Detalle.Add(resultado); }
            resultado = Respuesta.ValidarRequeridoString(persona.Domicilio.Calle, (nameof(persona.Domicilio.Calle)).ToLower());
            if (resultado.Severidad == Resultado.SeveridadEnum.Error) { respuesta.Detalle.Add(resultado); }
            resultado = Respuesta.ValidarRequeridoString(persona.Domicilio.Nro, (nameof(persona.Domicilio.Nro)).ToLower());
            if (resultado.Severidad == Resultado.SeveridadEnum.Error) { respuesta.Detalle.Add(resultado); }

            if (!FeaEntidades.CondicionesIVA.CondicionIVA.ValidarCondicionIvaListaInf(persona.DatosImpositivos.IdCondIVA.ToString()))
            {
                resultado = new Resultado(Resultado.SeveridadEnum.Error, "", "Condición de IVA: valor inválido");
                if (resultado.Severidad == Resultado.SeveridadEnum.Error) { respuesta.Detalle.Add(resultado); }
            }

            if (respuesta.Detalle.Count > 0)
            {
                respuesta.Resultado = respuesta.Detalle[0];
            }
            return respuesta;
        }

        public static void DesambiguarPersonaNacional(Entidades.Persona Persona, Entidades.Sesion Sesion)
        {
            DB.Persona db = new DB.Persona(Sesion);
            db.DesambiguarPersonaNacional(Persona);
        }
        public static void Modificar(Entidades.Persona Desde, Entidades.Persona Hasta, Entidades.Sesion Sesion)
        {
            DB.Persona db = new DB.Persona(Sesion);
            db.Modificar(Desde, Hasta);
        }
        public static void CambiarEstado(Entidades.Persona Persona, string Estado, Entidades.Sesion Sesion)
        {
            DB.Persona db = new DB.Persona(Sesion);
            db.CambiarEstado(Persona, Estado);
        }
        public static Entidades.Persona ObternerCopia(Entidades.Persona Desde)
        {
            Entidades.Persona hasta = new Entidades.Persona();
            hasta.Contacto.Nombre = Desde.Contacto.Nombre;
            hasta.Contacto.Telefono = Desde.Contacto.Telefono;
            hasta.Contacto.Email = Desde.Contacto.Email;
            hasta.Cuit = Desde.Cuit;
            hasta.IdPersona = Desde.IdPersona;
            hasta.DesambiguacionCuitPais = Desde.DesambiguacionCuitPais;
            hasta.Documento.Tipo.Id = Desde.Documento.Tipo.Id;
            hasta.Documento.Tipo.Descr = Desde.Documento.Tipo.Descr;
            hasta.Documento.Nro = Desde.Documento.Nro;
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
            hasta.EmailAvisoVisualizacion = Desde.EmailAvisoVisualizacion;
            hasta.PasswordAvisoVisualizacion = Desde.PasswordAvisoVisualizacion;
            hasta.RazonSocial = Desde.RazonSocial;
            hasta.UltActualiz = Desde.UltActualiz;
            hasta.WF.Id = Desde.WF.Id;
            hasta.WF.Estado = Desde.WF.Estado;
            return hasta;
        }
        public static List<Entidades.Persona> ListaSegunFiltros(string Cuit, string NroDoc, string RazSoc, string Estado, CedServicios.Entidades.Enum.TipoPersona TipoPersona, Entidades.Sesion Sesion)
        {
            DB.Persona cliente = new DB.Persona(Sesion);
            return cliente.ListaSegunFiltros(Cuit, NroDoc, RazSoc, Estado, TipoPersona);
        }
        public static List<Entidades.Persona> ListaPaging(out int CantidadFilas, int IndicePagina, string OrderBy, string Cuit, string NroDoc, string RazSoc, string Estado, string SessionID, Entidades.Sesion Sesion)
        {
            List<Entidades.Persona> listaPersona = new List<Entidades.Persona>();
            DB.Persona db = new DB.Persona(Sesion);
            if (OrderBy.Equals(String.Empty))
            {
                OrderBy = "Cuit desc, RazonSocial asc ";
            }
            listaPersona = db.ListaSegunFiltros(Cuit, NroDoc, RazSoc, Estado, Entidades.Enum.TipoPersona.Ambos);
            int cantidadFilas = listaPersona.Count;
            CantidadFilas = cantidadFilas;
            return db.ListaPaging(IndicePagina, OrderBy, SessionID, listaPersona);
        }
        public static void LeerDestinatariosFrecuentes(Entidades.Persona Persona, bool IncluirVacio, Entidades.Sesion Sesion)
        {
            DB.Persona persona = new DB.Persona(Sesion);
            persona.LeerDestinatariosFrecuentes(Persona, IncluirVacio);
        }
    }
}