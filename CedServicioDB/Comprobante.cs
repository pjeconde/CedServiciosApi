﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace CedServicios.DB
{
    public class Comprobante : db
    {
        public Comprobante(Entidades.Sesion Sesion) : base(Sesion)
        {
        }
        public List<Entidades.Comprobante> ListaContratosFiltrada(List<Entidades.Estado> Estados, string FechaEmision, Entidades.Persona Persona, string Moneda)
        {
            List<Entidades.Comprobante> lista = new List<Entidades.Comprobante>();
            if (sesion.Cuit.Nro != null)
            {
                System.Text.StringBuilder a = new StringBuilder();
                a.Append("select ");
                a.Append("Comprobante.Cuit, Comprobante.IdTipoComprobante, Comprobante.DescrTipoComprobante, Comprobante.NroPuntoVta, Comprobante.NroComprobante, Comprobante.NroLote, Comprobante.IdTipoDoc, Comprobante.NroDoc, Comprobante.IdPersona, Comprobante.DesambiguacionCuitPais, Comprobante.RazonSocial, Comprobante.Detalle, Comprobante.Fecha, Comprobante.FechaVto, Comprobante.Moneda, Comprobante.ImporteMoneda, Comprobante.TipoCambio, Comprobante.Importe, Comprobante.Request, Comprobante.Response, Comprobante.IdDestinoComprobante, Comprobante.IdWF, Comprobante.Estado, Comprobante.UltActualiz, Comprobante.IdNaturalezaComprobante, NaturalezaComprobante.DescrNaturalezaComprobante, Comprobante.PeriodicidadEmision, Comprobante.FechaProximaEmision, Comprobante.CantidadComprobantesAEmitir, Comprobante.CantidadComprobantesEmitidos, Comprobante.CantidadDiasFechaVto, Comprobante.EmailAvisoComprobanteActivo, Comprobante.IdDestinatarioFrecuente, Comprobante.EmailAvisoComprobanteAsunto, Comprobante.EmailAvisoComprobanteCuerpo ");
                a.Append("from Comprobante, NaturalezaComprobante ");
                a.Append("where Comprobante.Cuit='" + sesion.Cuit.Nro + "' ");
                a.Append("and Comprobante.IdNaturalezaComprobante=NaturalezaComprobante.IdNaturalezaComprobante ");
                string estados = String.Empty;
                for (int i = 0; i < Estados.Count; i++)
                {
                    estados += "'" + Estados[i].Id + "'";
                    if (i != (Estados.Count - 1)) estados += ", ";
                }
                if (estados != String.Empty)
                {
                    a.Append("and Comprobante.Estado in (" + estados + ") ");
                }
                if (FechaEmision != String.Empty)
                {
                    a.Append("and Comprobante.FechaProximaEmision<='" + FechaEmision + "' ");
                }
                if (Persona.Orden != 0)
                {
                    a.Append("and Comprobante.IdTipoDoc=" + Persona.Documento.Tipo.Id + " ");
                    a.Append("and Comprobante.NroDoc='" + Persona.Documento.Nro.ToString() + "' ");
                    a.Append("and Comprobante.IdPersona='" + Persona.IdPersona + "' ");
                    a.Append("and Comprobante.DesambiguacionCuitPais=" + Persona.DesambiguacionCuitPais.ToString() + " ");
                }
                a.Append("and Comprobante.IdNaturalezaComprobante='VentaContrato' ");
                a.Append("and Comprobante.Moneda='" + Moneda + "' ");
                a.Append("order by Comprobante.DescrTipoComprobante desc, Comprobante.NroPuntoVta desc, Comprobante.NroComprobante desc ");
                DataTable dt = (DataTable)Ejecutar(a.ToString(), TipoRetorno.TB, Transaccion.NoAcepta, sesion.CnnStr);
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Entidades.Comprobante elem = new Entidades.Comprobante();
                        Copiar(dt.Rows[i], elem);
                        lista.Add(elem);
                    }
                }
            }
            return lista;
        }
        public List<Entidades.Comprobante> ListaFiltradaIvaVentas(List<Entidades.Estado> Estados, string FechaDesde, string FechaHasta, Entidades.Persona Persona, Entidades.NaturalezaComprobante NaturalezaComprobante, bool IncluirContratos, string Detalle)
        {
            List<Entidades.Comprobante> lista = new List<Entidades.Comprobante>();
            if (sesion.Cuit.Nro != null)
            {
                System.Text.StringBuilder a = new StringBuilder();
                a.Append("select ");
                a.Append("Comprobante.Cuit, Comprobante.IdTipoComprobante, Comprobante.DescrTipoComprobante, Comprobante.NroPuntoVta, Comprobante.NroComprobante, Comprobante.NroLote, Comprobante.IdTipoDoc, Comprobante.NroDoc, Comprobante.IdPersona, Comprobante.DesambiguacionCuitPais, Comprobante.RazonSocial, Comprobante.Detalle, Comprobante.Fecha, Comprobante.FechaVto, Comprobante.Moneda, Comprobante.ImporteMoneda, Comprobante.TipoCambio, Comprobante.Importe, Comprobante.Request, Comprobante.Response, Comprobante.IdDestinoComprobante, Comprobante.IdWF, Comprobante.Estado, Comprobante.UltActualiz, Comprobante.IdNaturalezaComprobante, NaturalezaComprobante.DescrNaturalezaComprobante, Comprobante.PeriodicidadEmision, Comprobante.FechaProximaEmision, Comprobante.CantidadComprobantesAEmitir, Comprobante.CantidadComprobantesEmitidos, Comprobante.CantidadDiasFechaVto, Comprobante.EmailAvisoComprobanteActivo, Comprobante.IdDestinatarioFrecuente, Comprobante.EmailAvisoComprobanteAsunto, Comprobante.EmailAvisoComprobanteCuerpo ");
                a.Append("from Comprobante, NaturalezaComprobante ");
                a.Append("where Comprobante.Cuit='" + sesion.Cuit.Nro + "' ");
                a.Append("and Comprobante.IdNaturalezaComprobante=NaturalezaComprobante.IdNaturalezaComprobante ");
                string estados = String.Empty;
                for (int i = 0; i < Estados.Count; i++)
                {
                    estados += "'" + Estados[i].Id + "'";
                    if (i != (Estados.Count - 1)) estados += ", ";
                }
                if (estados != String.Empty)
                {
                    a.Append("and Comprobante.Estado in (" + estados + ") ");
                }
                if (FechaDesde != String.Empty)
                {
                    a.Append("and Comprobante.Fecha>='" + FechaDesde + "' ");
                }
                if (FechaHasta != String.Empty)
                {
                    a.Append("and Comprobante.Fecha<='" + FechaHasta + "' ");
                }
                if (Persona.Orden != 0)
                {
                    a.Append("and Comprobante.IdTipoDoc=" + Persona.Documento.Tipo.Id + " ");
                    a.Append("and Comprobante.NroDoc='" + Persona.Documento.Nro.ToString() + "' ");
                    a.Append("and Comprobante.IdPersona='" + Persona.IdPersona + "' ");
                    a.Append("and Comprobante.DesambiguacionCuitPais=" + Persona.DesambiguacionCuitPais.ToString() + " ");
                }
                if (NaturalezaComprobante.Id != String.Empty)
                {
                    a.Append("and Comprobante.IdNaturalezaComprobante='" + NaturalezaComprobante.Id + "' ");
                }
                else if (!IncluirContratos)
                {
                    a.Append("and Comprobante.IdNaturalezaComprobante<>'VentaContrato' ");
                }
                if (Detalle != string.Empty)
                {
                    a.Append("and Comprobante.Detalle like '%" + Detalle + "%' ");
                }
                a.Append("order by Comprobante.Fecha asc, Comprobante.DescrTipoComprobante asc, Comprobante.NroPuntoVta asc, ABS(Comprobante.NroComprobante) asc ");
                DataTable dt = (DataTable)Ejecutar(a.ToString(), TipoRetorno.TB, Transaccion.NoAcepta, sesion.CnnStr);
                if (dt.Rows.Count != 0)
                {
                    if (NaturalezaComprobante.Id != "Compra")
                    {
                        List<Entidades.UN> listaUN = sesion.Cuit.UNs.FindAll(delegate(Entidades.UN un)
                        {
                            return un.Id == sesion.UN.Id;
                        });
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Entidades.Comprobante elem = new Entidades.Comprobante();
                            Copiar(dt.Rows[i], elem);
                            List<Entidades.PuntoVta> listaPV = listaUN[0].PuntosVta.FindAll(delegate(Entidades.PuntoVta pv)
                            {
                                return pv.Nro == elem.NroPuntoVta;
                            });
                            if (listaPV.Count > 0)
                            {
                                lista.Add(elem);
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Entidades.Comprobante elem = new Entidades.Comprobante();
                            Copiar(dt.Rows[i], elem);
                            lista.Add(elem);
                        }
                    }
                }
            }
            return lista;
        }

        //API
        public Entidades.Comprobante Leer(string NaturalezaComprobante, string Cuit, int ComprobanteTipo, int NroPuntoVta, int ComprobanteNro, int DocumentoTipo, string DocumentoNro)
        {
            System.Text.StringBuilder a = new StringBuilder();
            Entidades.Comprobante comprobante = null;
            if (NaturalezaComprobante == "Compra")
            {
                a.Append("select ");
                a.Append("Comprobante.Cuit, Comprobante.IdTipoComprobante, Comprobante.DescrTipoComprobante, Comprobante.NroPuntoVta, Comprobante.NroComprobante, Comprobante.NroLote, Comprobante.IdTipoDoc, Comprobante.NroDoc, Comprobante.IdPersona, Comprobante.DesambiguacionCuitPais, Comprobante.RazonSocial, Comprobante.Detalle, Comprobante.Fecha, Comprobante.FechaVto, Comprobante.Moneda, Comprobante.ImporteMoneda, Comprobante.TipoCambio, Comprobante.Importe, Comprobante.Request, Comprobante.Response, Comprobante.IdDestinoComprobante, Comprobante.IdWF, Comprobante.Estado, Comprobante.UltActualiz, Comprobante.IdNaturalezaComprobante, NaturalezaComprobante.DescrNaturalezaComprobante, Comprobante.PeriodicidadEmision, Comprobante.FechaProximaEmision, Comprobante.CantidadComprobantesAEmitir, Comprobante.CantidadComprobantesEmitidos, Comprobante.CantidadDiasFechaVto, Comprobante.EmailAvisoComprobanteActivo, Comprobante.IdDestinatarioFrecuente, Comprobante.EmailAvisoComprobanteAsunto, Comprobante.EmailAvisoComprobanteCuerpo ");
                a.Append("from Comprobante, NaturalezaComprobante  ");
                a.Append("where Comprobante.Cuit='" + Cuit + "' and Comprobante.IdTipoComprobante=" + ComprobanteTipo + " and Comprobante.NroPuntoVta=" + NroPuntoVta + " and Comprobante.NroComprobante=" + ComprobanteNro + " and Comprobante.IdTipoDoc=" + DocumentoTipo + " and Comprobante.NroDoc='" + DocumentoNro + "' ");
                a.Append("and Comprobante.IdNaturalezaComprobante=NaturalezaComprobante.IdNaturalezaComprobante and Comprobante.IdNaturalezaComprobante = '" + NaturalezaComprobante + "'");
            }
            else
            {
                a.Append("select ");
                a.Append("Comprobante.Cuit, Comprobante.IdTipoComprobante, Comprobante.DescrTipoComprobante, Comprobante.NroPuntoVta, Comprobante.NroComprobante, Comprobante.NroLote, Comprobante.IdTipoDoc, Comprobante.NroDoc, Comprobante.IdPersona, Comprobante.DesambiguacionCuitPais, Comprobante.RazonSocial, Comprobante.Detalle, Comprobante.Fecha, Comprobante.FechaVto, Comprobante.Moneda, Comprobante.ImporteMoneda, Comprobante.TipoCambio, Comprobante.Importe, Comprobante.Request, Comprobante.Response, Comprobante.IdDestinoComprobante, Comprobante.IdWF, Comprobante.Estado, Comprobante.UltActualiz, Comprobante.IdNaturalezaComprobante, NaturalezaComprobante.DescrNaturalezaComprobante, Comprobante.PeriodicidadEmision, Comprobante.FechaProximaEmision, Comprobante.CantidadComprobantesAEmitir, Comprobante.CantidadComprobantesEmitidos, Comprobante.CantidadDiasFechaVto, Comprobante.EmailAvisoComprobanteActivo, Comprobante.IdDestinatarioFrecuente, Comprobante.EmailAvisoComprobanteAsunto, Comprobante.EmailAvisoComprobanteCuerpo ");
                a.Append("from Comprobante, NaturalezaComprobante  ");
                a.Append("where Comprobante.Cuit='" + Cuit + "' and Comprobante.IdTipoComprobante=" + ComprobanteTipo + " and Comprobante.NroPuntoVta=" + NroPuntoVta + " and Comprobante.NroComprobante=" + (NaturalezaComprobante == "VentaContrato" ? -ComprobanteNro : ComprobanteNro).ToString() + " ");
                a.Append("and Comprobante.IdNaturalezaComprobante=NaturalezaComprobante.IdNaturalezaComprobante and Comprobante.IdNaturalezaComprobante = '" + NaturalezaComprobante + "'");
            }
            DataTable dt = (DataTable)Ejecutar(a.ToString(), TipoRetorno.TB, Transaccion.NoAcepta, sesion.CnnStr);
            if (dt.Rows.Count != 0)
            {
                comprobante = new Entidades.Comprobante();
                Copiar(dt.Rows[0], comprobante);
            }
            return comprobante;
        }
        public void Leer(Entidades.Comprobante Comprobante)
        {
            System.Text.StringBuilder a = new StringBuilder();
            if (Comprobante.NaturalezaComprobante.Id == "Compra")
            {
                a.Append("select ");
                a.Append("Comprobante.Cuit, Comprobante.IdTipoComprobante, Comprobante.DescrTipoComprobante, Comprobante.NroPuntoVta, Comprobante.NroComprobante, Comprobante.NroLote, Comprobante.IdTipoDoc, Comprobante.NroDoc, Comprobante.IdPersona, Comprobante.DesambiguacionCuitPais, Comprobante.RazonSocial, Comprobante.Detalle, Comprobante.Fecha, Comprobante.FechaVto, Comprobante.Moneda, Comprobante.ImporteMoneda, Comprobante.TipoCambio, Comprobante.Importe, Comprobante.Request, Comprobante.Response, Comprobante.IdDestinoComprobante, Comprobante.IdWF, Comprobante.Estado, Comprobante.UltActualiz, Comprobante.IdNaturalezaComprobante, NaturalezaComprobante.DescrNaturalezaComprobante, Comprobante.PeriodicidadEmision, Comprobante.FechaProximaEmision, Comprobante.CantidadComprobantesAEmitir, Comprobante.CantidadComprobantesEmitidos, Comprobante.CantidadDiasFechaVto, Comprobante.EmailAvisoComprobanteActivo, Comprobante.IdDestinatarioFrecuente, Comprobante.EmailAvisoComprobanteAsunto, Comprobante.EmailAvisoComprobanteCuerpo ");
                a.Append("from Comprobante, NaturalezaComprobante  ");
                a.Append("where Comprobante.Cuit='" + sesion.Cuit.Nro + "' and Comprobante.IdTipoComprobante=" + Comprobante.TipoComprobante.Id.ToString() + " and Comprobante.NroPuntoVta=" + Comprobante.NroPuntoVta.ToString() + " and Comprobante.NroComprobante=" + Comprobante.Nro.ToString() + " and Comprobante.IdTipoDoc=" + Comprobante.Documento.Tipo.Id + " and Comprobante.NroDoc='" + Comprobante.Documento.Nro + "' ");
                a.Append("and Comprobante.IdNaturalezaComprobante=NaturalezaComprobante.IdNaturalezaComprobante and Comprobante.IdNaturalezaComprobante = '" + Comprobante.NaturalezaComprobante.Id + "'");
            }
            else
            {
                a.Append("select ");
                a.Append("Comprobante.Cuit, Comprobante.IdTipoComprobante, Comprobante.DescrTipoComprobante, Comprobante.NroPuntoVta, Comprobante.NroComprobante, Comprobante.NroLote, Comprobante.IdTipoDoc, Comprobante.NroDoc, Comprobante.IdPersona, Comprobante.DesambiguacionCuitPais, Comprobante.RazonSocial, Comprobante.Detalle, Comprobante.Fecha, Comprobante.FechaVto, Comprobante.Moneda, Comprobante.ImporteMoneda, Comprobante.TipoCambio, Comprobante.Importe, Comprobante.Request, Comprobante.Response, Comprobante.IdDestinoComprobante, Comprobante.IdWF, Comprobante.Estado, Comprobante.UltActualiz, Comprobante.IdNaturalezaComprobante, NaturalezaComprobante.DescrNaturalezaComprobante, Comprobante.PeriodicidadEmision, Comprobante.FechaProximaEmision, Comprobante.CantidadComprobantesAEmitir, Comprobante.CantidadComprobantesEmitidos, Comprobante.CantidadDiasFechaVto, Comprobante.EmailAvisoComprobanteActivo, Comprobante.IdDestinatarioFrecuente, Comprobante.EmailAvisoComprobanteAsunto, Comprobante.EmailAvisoComprobanteCuerpo ");
                a.Append("from Comprobante, NaturalezaComprobante  ");
                a.Append("where Comprobante.Cuit='" + sesion.Cuit.Nro + "' and Comprobante.IdTipoComprobante=" + Comprobante.TipoComprobante.Id.ToString() + " and Comprobante.NroPuntoVta=" + Comprobante.NroPuntoVta.ToString() + " and Comprobante.NroComprobante=" + (Comprobante.NaturalezaComprobante.Id == "VentaContrato" ? -Comprobante.Nro : Comprobante.Nro).ToString() + " ");
                a.Append("and Comprobante.IdNaturalezaComprobante=NaturalezaComprobante.IdNaturalezaComprobante and Comprobante.IdNaturalezaComprobante = '" + Comprobante.NaturalezaComprobante.Id + "'");
            }
            DataTable dt = (DataTable)Ejecutar(a.ToString(), TipoRetorno.TB, Transaccion.NoAcepta, sesion.CnnStr);
            if (dt.Rows.Count != 0)
            {
                Copiar(dt.Rows[0], Comprobante);
            }
        }
        public string LeerEstado(Entidades.Comprobante Comprobante)
        {
            string estado = "";
            System.Text.StringBuilder a = new StringBuilder();
            a.Append("select ");
            a.Append("Comprobante.Cuit, Comprobante.IdTipoComprobante, Comprobante.DescrTipoComprobante, Comprobante.NroPuntoVta, Comprobante.NroComprobante, Comprobante.NroLote, Comprobante.IdTipoDoc, Comprobante.NroDoc, Comprobante.IdPersona, Comprobante.DesambiguacionCuitPais, Comprobante.RazonSocial, Comprobante.Detalle, Comprobante.Fecha, Comprobante.FechaVto, Comprobante.Moneda, Comprobante.ImporteMoneda, Comprobante.TipoCambio, Comprobante.Importe, Comprobante.Request, Comprobante.Response, Comprobante.IdDestinoComprobante, Comprobante.IdWF, Comprobante.Estado, Comprobante.UltActualiz, Comprobante.IdNaturalezaComprobante, NaturalezaComprobante.DescrNaturalezaComprobante, Comprobante.PeriodicidadEmision, Comprobante.FechaProximaEmision, Comprobante.CantidadComprobantesAEmitir, Comprobante.CantidadComprobantesEmitidos, Comprobante.CantidadDiasFechaVto, Comprobante.EmailAvisoComprobanteActivo, Comprobante.IdDestinatarioFrecuente, Comprobante.EmailAvisoComprobanteAsunto, Comprobante.EmailAvisoComprobanteCuerpo ");
            a.Append("from Comprobante, NaturalezaComprobante  ");
            a.Append("where Comprobante.Cuit='" + sesion.Cuit.Nro + "' and Comprobante.IdTipoComprobante=" + Comprobante.TipoComprobante.Id.ToString() + " and Comprobante.NroPuntoVta=" + Comprobante.NroPuntoVta.ToString() + " and Comprobante.NroComprobante=" + (Comprobante.NaturalezaComprobante.Id == "VentaContrato" ? -Comprobante.Nro : Comprobante.Nro).ToString() + " ");
            a.Append("and Comprobante.IdNaturalezaComprobante=NaturalezaComprobante.IdNaturalezaComprobante ");
            DataTable dt = (DataTable)Ejecutar(a.ToString(), TipoRetorno.TB, Transaccion.NoAcepta, sesion.CnnStr);
            if (dt.Rows.Count != 0)
            {
                estado = dt.Rows[0]["Estado"].ToString();
            }
            return estado;
        }
        public void LeerMinutas(Entidades.Comprobante Comprobante)
        {
            System.Text.StringBuilder a = new StringBuilder();
            a.Append("select ComprobanteDetalle.IdWF, ComprobanteDetalle.IdTipoItem, ComprobanteDetalle.NroItem, ComprobanteDetalle.IdArticulo, ComprobanteDetalle.IdRubro, ComprobanteDetalle.Cantidad, ComprobanteDetalle.PrecioUnitario, ComprobanteDetalle.Importe, ComprobanteDetalle.IdUbicacion, ComprobanteDetalle.IndicadorExentoGravado, ComprobanteDetalle.Detalle, Rubro.DescrRubro from ComprobanteDetalle, Rubro where IdWF=" + Comprobante.WF.Id.ToString() + " and ComprobanteDetalle.IdRubro=Rubro.IdRubro ");
            DataTable dt = (DataTable)Ejecutar(a.ToString(), TipoRetorno.TB, Transaccion.NoAcepta, sesion.CnnStr);
            if (dt.Rows.Count != 0)
            {
                Comprobante.Minutas.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Entidades.ComprobanteDetalle minuta = new Entidades.ComprobanteDetalle();
                    minuta.Item.IdTipo = Convert.ToString(dt.Rows[i]["IdTipoItem"]);
                    minuta.Item.Nro = Convert.ToInt32(dt.Rows[i]["NroItem"]);
                    minuta.Articulo.Id = Convert.ToString(dt.Rows[i]["IdArticulo"]);
                    minuta.Rubro.Id = Convert.ToString(dt.Rows[i]["IdRubro"]);
                    minuta.Cantidad = Convert.ToDouble(dt.Rows[i]["Cantidad"]);
                    minuta.PrecioUnitario = Convert.ToDouble(dt.Rows[i]["PrecioUnitario"]);
                    minuta.Importe = Convert.ToDouble(dt.Rows[i]["Importe"]);
                    minuta.IdUbicacion = Convert.ToString(dt.Rows[i]["IdUbicacion"]);
                    minuta.IndicadorExentoGravado = Convert.ToString(dt.Rows[i]["IndicadorExentoGravado"]);
                    minuta.Detalle = Convert.ToString(dt.Rows[i]["Detalle"]);
                    minuta.Rubro.Descr = Convert.ToString(dt.Rows[i]["DescrRubro"]);
                    Comprobante.Minutas.Add(minuta);
                }
            }
        }
        public int Crear(Entidades.Comprobante Comprobante)
        {
            StringBuilder a = new StringBuilder(string.Empty);
            if (Comprobante.NaturalezaComprobante.Id != "Compra")
            {
                a.AppendLine("if not exists (select * from Comprobante where Cuit = '" + Comprobante.Cuit + "' and IdTipoComprobante=" + Comprobante.TipoComprobante.Id.ToString() + " and NroPuntoVta=" + Comprobante.NroPuntoVta.ToString() + " and Nrocomprobante = " + (Comprobante.NaturalezaComprobante.Id == "VentaContrato" ? -Comprobante.Nro : Comprobante.Nro).ToString() + " and IdNaturalezaComprobante = '" + Comprobante.NaturalezaComprobante.Id + "')");
                a.AppendLine("BEGIN ");
                a.AppendLine(CrearScript(Comprobante));
                a.AppendLine("END ");
                a.AppendLine("else ");
                a.AppendLine("BEGIN ");
                a.AppendLine("select @@rowcount ");
                a.AppendLine("END ");
            }
            else
            {
                a.AppendLine(CrearScript(Comprobante));
            }
            return (Int32)Ejecutar(a.ToString(), TipoRetorno.CantReg, Transaccion.Usa, sesion.CnnStr);
        }

        private string CrearScript(Entidades.Comprobante Comprobante)
        {
            StringBuilder a = new StringBuilder(string.Empty);
            a.AppendLine("declare @idWF varchar(256) ");
            a.AppendLine("update Configuracion set @idWF=Valor=convert(varchar(256), convert(int, Valor)+1) where IdItemConfig='UltimoIdWF' ");
            a.AppendLine("Insert Comprobante (Comprobante.Cuit, Comprobante.IdTipoComprobante, Comprobante.DescrTipoComprobante, Comprobante.NroPuntoVta, Comprobante.NroComprobante, Comprobante.NroLote, Comprobante.IdTipoDoc, Comprobante.NroDoc, Comprobante.IdPersona, Comprobante.DesambiguacionCuitPais, Comprobante.RazonSocial, Comprobante.Detalle, Comprobante.Fecha, Comprobante.FechaVto, Comprobante.Moneda, Comprobante.ImporteMoneda, Comprobante.TipoCambio, Comprobante.Importe, Comprobante.Request, Comprobante.Response, Comprobante.IdDestinoComprobante, Comprobante.IdWF, Comprobante.Estado, Comprobante.IdNaturalezaComprobante, Comprobante.PeriodicidadEmision, Comprobante.FechaProximaEmision, Comprobante.CantidadComprobantesAEmitir, Comprobante.CantidadComprobantesEmitidos, Comprobante.CantidadDiasFechaVto, Comprobante.EmailAvisoComprobanteActivo, Comprobante.IdDestinatarioFrecuente, Comprobante.EmailAvisoComprobanteAsunto, Comprobante.EmailAvisoComprobanteCuerpo) ");
            a.Append("values (");
            a.Append("'" + Comprobante.Cuit + "', ");
            a.Append(Comprobante.TipoComprobante.Id.ToString() + ", ");
            a.Append("'" + Comprobante.TipoComprobante.Descr + "', ");
            a.Append(Comprobante.NroPuntoVta.ToString() + ", ");
            a.Append((Comprobante.NaturalezaComprobante.Id == "VentaContrato" ? -Comprobante.Nro : Comprobante.Nro).ToString() + ", ");
            a.Append(Comprobante.NroLote.ToString() + ", ");
            a.Append(Comprobante.Documento.Tipo.Id + ", '");
            a.Append(Comprobante.Documento.Nro.ToString() + "', ");
            if (Comprobante.IdPersona == null)
            {
                a.Append("'', ");
            }
            else
            {
                a.Append("'" + Comprobante.IdPersona + "', ");
            }
            a.Append(Comprobante.DesambiguacionCuitPais.ToString() + ", ");
            a.Append("'" + Comprobante.RazonSocial + "', ");
            if (Comprobante.Detalle == null)
            {
                a.Append("'', ");
            }
            else
            {
                a.Append("'" + Comprobante.Detalle + "', ");
            }
            a.Append("'" + Comprobante.Fecha.ToString("yyyyMMdd") + "', ");
            a.Append("'" + Comprobante.FechaVto.ToString("yyyyMMdd") + "', ");
            a.Append("'" + Comprobante.Moneda + "', ");
            a.Append(Comprobante.ImporteMoneda.ToString("0000000000000.00", System.Globalization.CultureInfo.InvariantCulture) + ", ");
            a.Append(Comprobante.TipoCambio.ToString("0000.000000", System.Globalization.CultureInfo.InvariantCulture) + ", ");
            a.Append(Comprobante.Importe.ToString("0000000000000.00", System.Globalization.CultureInfo.InvariantCulture) + ", ");
            a.Append("'" + Comprobante.Request + "', ");
            if (Comprobante.Response == null)
            {
                a.Append("'', ");
            }
            else
            {
                a.Append("'" + Comprobante.Response + "', ");
            }
            a.Append("'" + Comprobante.IdDestinoComprobante + "', ");
            a.Append("@idWF, ");
            a.Append("'" + Comprobante.WF.Estado + "', ");
            a.Append("'" + Comprobante.NaturalezaComprobante.Id + "', ");
            a.Append("'" + Comprobante.PeriodicidadEmision + "', ");
            a.Append("'" + Comprobante.FechaProximaEmision.ToString("yyyyMMdd") + "', ");
            a.Append(Comprobante.CantidadComprobantesAEmitir + ", ");
            a.Append(Comprobante.CantidadComprobantesEmitidos + ", ");
            a.Append(Comprobante.CantidadDiasFechaVto + ", ");
            a.Append(Convert.ToInt32(Comprobante.DatosEmailAvisoComprobanteContrato.Activo).ToString() + ", ");
            a.Append("'" + Comprobante.DatosEmailAvisoComprobanteContrato.DestinatarioFrecuente.Id + "', ");
            a.Append("'" + Comprobante.DatosEmailAvisoComprobanteContrato.Asunto + "', ");
            a.Append("'" + Comprobante.DatosEmailAvisoComprobanteContrato.Cuerpo + "' ");
            a.AppendLine(") ");
            a.AppendLine("insert Log values (@idWF, getdate(), '" + sesion.Usuario.Id + "', 'Comprobante', 'Alta', '" + Comprobante.WF.Estado + "', '') ");
            //Alta ComprobanteDetalle (minutas)
            for (int i = 0; i < Comprobante.Minutas.Count; i++)
            {
                a.AppendLine("insert ComprobanteDetalle (IdWF, IdTipoItem, NroItem, IdArticulo, IdRubro, Cantidad, PrecioUnitario, Importe, IdUbicacion, IndicadorExentoGravado, Detalle) ");
                a.AppendLine("values (@idWF, '" + Comprobante.Minutas[i].Item.IdTipo + "', " + Comprobante.Minutas[i].Item.Nro.ToString() + ", '" + Comprobante.Minutas[i].Articulo.Id + "', '" + Comprobante.Minutas[i].Rubro.Id + "', " + Comprobante.Minutas[i].Cantidad.ToString("0000000000000.00", System.Globalization.CultureInfo.InvariantCulture) + ", " + Comprobante.Minutas[i].PrecioUnitario.ToString("0000000000000.00", System.Globalization.CultureInfo.InvariantCulture) + ", " + Comprobante.Minutas[i].Importe.ToString("0000000000000.00", System.Globalization.CultureInfo.InvariantCulture) + ", '" + Comprobante.Minutas[i].IdUbicacion + "', '" + Comprobante.Minutas[i].IndicadorExentoGravado + "', '" + Comprobante.Minutas[i].Detalle + "') ");
            }
            a.AppendLine("select @@rowcount ");
            return a.ToString();
        }
        public int Modificar(Entidades.Comprobante Comprobante, Entidades.Comprobante ComprobanteOrig)
        {
            StringBuilder a = new StringBuilder(string.Empty);
            if (Comprobante.NaturalezaComprobante.Id != "Compra")
            {
                a.AppendLine("if exists (select * from Comprobante where Cuit = '" + Comprobante.Cuit + "' and IdTipoComprobante=" + Comprobante.TipoComprobante.Id.ToString() + " and NroPuntoVta=" + Comprobante.NroPuntoVta.ToString() + " and Nrocomprobante = " + (Comprobante.NaturalezaComprobante.Id == "VentaContrato" ? -Comprobante.Nro : Comprobante.Nro).ToString() + " and IdNaturalezaComprobante = '" + Comprobante.NaturalezaComprobante.Id + "' and UltActualiz = " + ComprobanteOrig.UltActualiz + ") ");
                a.AppendLine("or (not exists (select * from Comprobante where Cuit = '" + Comprobante.Cuit + "' and IdTipoComprobante=" + Comprobante.TipoComprobante.Id.ToString() + " and NroPuntoVta=" + Comprobante.NroPuntoVta.ToString() + " and Nrocomprobante = " + (Comprobante.NaturalezaComprobante.Id == "VentaContrato" ? -Comprobante.Nro : Comprobante.Nro).ToString() + " and IdNaturalezaComprobante = '" + Comprobante.NaturalezaComprobante.Id + "') ");
                a.AppendLine("and exists (select * from Comprobante where Cuit='" + ComprobanteOrig.Cuit + "' and IdTipoComprobante=" + ComprobanteOrig.TipoComprobante.Id.ToString() + " and NroPuntoVta=" + ComprobanteOrig.NroPuntoVta.ToString() + " and Nrocomprobante=" + (Comprobante.NaturalezaComprobante.Id == "VentaContrato" ? -ComprobanteOrig.Nro : ComprobanteOrig.Nro).ToString() + " and IdNaturalezaComprobante = '" + Comprobante.NaturalezaComprobante.Id + "' and UltActualiz = " + ComprobanteOrig.UltActualiz + ")) ");
                a.AppendLine("BEGIN ");
                a.AppendLine(ModificarScript(Comprobante, ComprobanteOrig));
                a.AppendLine("END ");
                a.AppendLine("else ");
                a.AppendLine("BEGIN ");
                a.AppendLine("select @@rowcount ");
                a.AppendLine("END ");
            }
            else
            {
                a.AppendLine(ModificarScript(Comprobante, ComprobanteOrig));
            }
            return (Int32)Ejecutar(a.ToString(), TipoRetorno.CantReg, Transaccion.Usa, sesion.CnnStr);
        }
        private string ModificarScript(Entidades.Comprobante Comprobante, Entidades.Comprobante ComprobanteOrig)
        {
            StringBuilder a = new StringBuilder(string.Empty);
            a.AppendLine("declare @idWF varchar(256) ");
            a.AppendLine("set @IdWF=" + ComprobanteOrig.WF.Id.ToString() + " ");
            a.Append("update Comprobante set ");
            //----- Si es necesario se modifica la clave principal -----
            a.Append("Comprobante.Cuit='" + Comprobante.Cuit + "', ");
            a.Append("Comprobante.IdTipoComprobante=" + Comprobante.TipoComprobante.Id + ", ");
            a.Append("Comprobante.NroPuntoVta=" + Comprobante.NroPuntoVta + ", ");
            a.Append("Comprobante.NroComprobante=" + (Comprobante.NaturalezaComprobante.Id == "VentaContrato" ? -Comprobante.Nro : Comprobante.Nro).ToString() + ", ");
            //----------------------------------------------------------
            a.Append("Comprobante.DescrTipoComprobante='" + Comprobante.TipoComprobante.Descr + "', ");
            a.Append("Comprobante.NroLote=" + Comprobante.NroLote.ToString() + ", ");
            a.Append("Comprobante.IdTipoDoc=" + Comprobante.Documento.Tipo.Id + ", ");
            a.Append("Comprobante.NroDoc='" + Comprobante.Documento.Nro.ToString() + "', ");
            if (Comprobante.IdPersona == null)
            {
                a.Append("Comprobante.IdPersona='', ");
            }
            else
            {
                a.Append("Comprobante.IdPersona='" + Comprobante.IdPersona + "', ");
            }
            a.Append("Comprobante.DesambiguacionCuitPais=" + Comprobante.DesambiguacionCuitPais.ToString() + ", ");
            a.Append("Comprobante.RazonSocial='" + Comprobante.RazonSocial + "', ");
            if (Comprobante.Detalle == null)
            {
                a.Append("Comprobante.Detalle='', ");
            }
            else
            {
                a.Append("Comprobante.Detalle='" + Comprobante.Detalle + "', ");
            }
            a.Append("Comprobante.Fecha='" + Comprobante.Fecha.ToString("yyyyMMdd") + "', ");
            a.Append("Comprobante.FechaVto='" + Comprobante.FechaVto.ToString("yyyyMMdd") + "', ");
            a.Append("Comprobante.Moneda='" + Comprobante.Moneda + "', ");
            a.Append("Comprobante.ImporteMoneda=" + Comprobante.ImporteMoneda.ToString("0000000000000.00", System.Globalization.CultureInfo.InvariantCulture) + ", ");
            a.Append("Comprobante.TipoCambio=" + Comprobante.TipoCambio.ToString("0000.000000", System.Globalization.CultureInfo.InvariantCulture) + ", ");
            a.Append("Comprobante.Importe=" + Comprobante.Importe.ToString("0000000000000.00", System.Globalization.CultureInfo.InvariantCulture) + ", ");
            a.Append("Comprobante.Request='" + Comprobante.Request + "', ");
            if (Comprobante.Response == null)
            {
                a.Append("Comprobante.Response='', ");
            }
            else
            {
                a.Append("Comprobante.Response='" + Comprobante.Response + "', ");
            }
            a.Append("Comprobante.IdDestinoComprobante='" + Comprobante.IdDestinoComprobante + "', ");
            a.Append("Comprobante.Estado='" + Comprobante.WF.Estado + "', ");
            a.Append("Comprobante.IdNaturalezaComprobante='" + Comprobante.NaturalezaComprobante.Id + "', ");
            a.Append("Comprobante.PeriodicidadEmision='" + Comprobante.PeriodicidadEmision + "', ");
            a.Append("Comprobante.FechaProximaEmision='" + Comprobante.FechaProximaEmision.ToString("yyyyMMdd") + "', ");
            a.Append("Comprobante.CantidadComprobantesAEmitir=" + Comprobante.CantidadComprobantesAEmitir + ", ");
            a.Append("Comprobante.CantidadComprobantesEmitidos=" + Comprobante.CantidadComprobantesEmitidos + ", ");
            a.Append("Comprobante.CantidadDiasFechaVto=" + Comprobante.CantidadDiasFechaVto + ", ");
            a.Append("Comprobante.EmailAvisoComprobanteActivo=" + Convert.ToInt32(Comprobante.DatosEmailAvisoComprobanteContrato.Activo).ToString() + ", ");
            a.Append("Comprobante.IdDestinatarioFrecuente='" + Comprobante.DatosEmailAvisoComprobanteContrato.DestinatarioFrecuente.Id + "', ");
            a.Append("Comprobante.EmailAvisoComprobanteAsunto='" + Comprobante.DatosEmailAvisoComprobanteContrato.Asunto + "', ");
            a.Append("Comprobante.EmailAvisoComprobanteCuerpo='" + Comprobante.DatosEmailAvisoComprobanteContrato.Cuerpo + "' ");
            a.AppendLine("where Cuit='" + ComprobanteOrig.Cuit + "' and IdTipoComprobante=" + ComprobanteOrig.TipoComprobante.Id.ToString() + " and NroPuntoVta=" + ComprobanteOrig.NroPuntoVta.ToString() + " and Nrocomprobante=" + (Comprobante.NaturalezaComprobante.Id == "VentaContrato" ? -ComprobanteOrig.Nro : ComprobanteOrig.Nro).ToString() + " and IdNaturalezaComprobante = '" + ComprobanteOrig.NaturalezaComprobante.Id + "' and UltActualiz = " + ComprobanteOrig.UltActualiz + " ");
            a.AppendLine("insert Log values (@idWF, getdate(), '" + sesion.Usuario.Id + "', 'Comprobante', 'Modif', '" + Comprobante.WF.Estado + "', '') ");
            a.AppendLine("declare @idLog int ");
            a.AppendLine("select @idLog=@@Identity ");
            a.AppendLine("insert LogDetalle (IdLog, TipoDetalle, Detalle) values (@idLog, 'Desde', '" + Funciones.ObjetoSerializado(ComprobanteOrig) + "')");
            a.AppendLine("insert LogDetalle (IdLog, TipoDetalle, Detalle) values (@idLog, 'Hasta', '" + Funciones.ObjetoSerializado(Comprobante) + "') ");
            //Eliminar ComprobanteDetalle (minutas)
            a.AppendLine("delete ComprobanteDetalle where IdWF=@idWF ");
            //Alta ComprobanteDetalle (minutas)
            for (int i = 0; i < Comprobante.Minutas.Count; i++)
            {
                a.AppendLine("insert ComprobanteDetalle (IdWF, IdTipoItem, NroItem, IdArticulo, IdRubro, Cantidad, PrecioUnitario, Importe, IdUbicacion, IndicadorExentoGravado, Detalle) ");
                a.AppendLine("values (@idWF, '" + Comprobante.Minutas[i].Item.IdTipo + "', " + Comprobante.Minutas[i].Item.Nro.ToString() + ", '" + Comprobante.Minutas[i].Articulo.Id + "', '" + Comprobante.Minutas[i].Rubro.Id + "', " + Comprobante.Minutas[i].Cantidad.ToString("0000000000000.00", System.Globalization.CultureInfo.InvariantCulture) + ", " + Comprobante.Minutas[i].PrecioUnitario.ToString("0000000000000.00", System.Globalization.CultureInfo.InvariantCulture) + ", " + Comprobante.Minutas[i].Importe.ToString("0000000000000.00", System.Globalization.CultureInfo.InvariantCulture) + ", '" + Comprobante.Minutas[i].IdUbicacion + "', '" + Comprobante.Minutas[i].IndicadorExentoGravado + "', '" + Comprobante.Minutas[i].Detalle + "') ");
            }
            return a.ToString();
        }
        public void Registrar(Entidades.Comprobante Comprobante)
        {
            Entidades.Comprobante comprobanteDesde = new Entidades.Comprobante();
            comprobanteDesde.Cuit = Comprobante.Cuit;
            comprobanteDesde.TipoComprobante = Comprobante.TipoComprobante;
            comprobanteDesde.NroPuntoVta = Comprobante.NroPuntoVta;
            comprobanteDesde.Nro = Comprobante.NaturalezaComprobante.Id == "VentaContrato" ? -Comprobante.Nro : Comprobante.Nro;
            comprobanteDesde.NaturalezaComprobante.Id = Comprobante.NaturalezaComprobante.Id;
            Leer(comprobanteDesde);
            StringBuilder a = new StringBuilder(string.Empty);
            a.AppendLine("declare @idWF varchar(256) ");
            if (comprobanteDesde.Documento.Tipo.Id == null)
            {
                a.AppendLine("update Configuracion set @idWF=Valor=convert(varchar(256), convert(int, Valor)+1) where IdItemConfig='UltimoIdWF' ");
                a.AppendLine("Insert Comprobante (Comprobante.Cuit, Comprobante.IdTipoComprobante, Comprobante.DescrTipoComprobante, Comprobante.NroPuntoVta, Comprobante.NroComprobante, Comprobante.NroLote, Comprobante.IdTipoDoc, Comprobante.NroDoc, Comprobante.IdPersona, Comprobante.DesambiguacionCuitPais, Comprobante.RazonSocial, Comprobante.Detalle, Comprobante.Fecha, Comprobante.FechaVto, Comprobante.Moneda, Comprobante.ImporteMoneda, Comprobante.TipoCambio, Comprobante.Importe, Comprobante.Request, Comprobante.Response, Comprobante.IdDestinoComprobante, Comprobante.IdWF, Comprobante.Estado, Comprobante.IdNaturalezaComprobante, Comprobante.PeriodicidadEmision, Comprobante.FechaProximaEmision, Comprobante.CantidadComprobantesAEmitir, Comprobante.CantidadComprobantesEmitidos, Comprobante.CantidadDiasFechaVto, Comprobante.EmailAvisoComprobanteActivo, Comprobante.IdDestinatarioFrecuente, Comprobante.EmailAvisoComprobanteAsunto, Comprobante.EmailAvisoComprobanteCuerpo) ");
                a.Append("values (");
                a.Append("'" + Comprobante.Cuit + "', ");
                a.Append(Comprobante.TipoComprobante.Id.ToString() + ", ");
                a.Append("'" + Comprobante.TipoComprobante.Descr + "', ");
                a.Append(Comprobante.NroPuntoVta.ToString() + ", ");
                a.Append((Comprobante.NaturalezaComprobante.Id == "VentaContrato" ? -Comprobante.Nro : Comprobante.Nro).ToString() + ", ");
                a.Append(Comprobante.NroLote.ToString() + ", ");
                a.Append(Comprobante.Documento.Tipo.Id + ", '");
                a.Append(Comprobante.Documento.Nro.ToString() + "', ");
                if (Comprobante.IdPersona == null)
                {
                    a.Append("'', ");
                }
                else
                {
                    a.Append("'" + Comprobante.IdPersona + "', ");
                }
                a.Append(Comprobante.DesambiguacionCuitPais.ToString() + ", ");
                a.Append("'" + Comprobante.RazonSocial + "', ");
                if (Comprobante.Detalle == null)
                {
                    a.Append("'', ");
                }
                else
                {
                    a.Append("'" + Comprobante.Detalle + "', ");
                }
                a.Append("'" + Comprobante.Fecha.ToString("yyyyMMdd") + "', ");
                a.Append("'" + Comprobante.FechaVto.ToString("yyyyMMdd") + "', ");
                a.Append("'" + Comprobante.Moneda + "', ");
                a.Append(Comprobante.ImporteMoneda.ToString("0000000000000.00", System.Globalization.CultureInfo.InvariantCulture) + ", ");
                a.Append(Comprobante.TipoCambio.ToString("0000.000000", System.Globalization.CultureInfo.InvariantCulture) + ", ");
                a.Append(Comprobante.Importe.ToString("0000000000000.00", System.Globalization.CultureInfo.InvariantCulture) + ", ");
                a.Append("'" + Comprobante.Request + "', ");
                if (Comprobante.Response == null)
                {
                    a.Append("'', ");
                }
                else
                {
                    a.Append("'" + Comprobante.Response + "', ");
                }
                a.Append("'" + Comprobante.IdDestinoComprobante + "', ");
                a.Append("@idWF, ");
                a.Append("'" + Comprobante.WF.Estado + "', ");
                a.Append("'" + Comprobante.NaturalezaComprobante.Id + "', ");
                a.Append("'" + Comprobante.PeriodicidadEmision + "', ");
                a.Append("'" + Comprobante.FechaProximaEmision.ToString("yyyyMMdd") + "', ");
                a.Append(Comprobante.CantidadComprobantesAEmitir + ", ");
                a.Append(Comprobante.CantidadComprobantesEmitidos + ", ");
                a.Append(Comprobante.CantidadDiasFechaVto + ", ");
                a.Append(Convert.ToInt32(Comprobante.DatosEmailAvisoComprobanteContrato.Activo).ToString() + ", ");
                a.Append("'" + Comprobante.DatosEmailAvisoComprobanteContrato.DestinatarioFrecuente.Id + "', ");
                a.Append("'" + Comprobante.DatosEmailAvisoComprobanteContrato.Asunto + "', ");
                a.Append("'" + Comprobante.DatosEmailAvisoComprobanteContrato.Cuerpo + "' ");
                a.AppendLine(") ");
                a.AppendLine("insert Log values (@idWF, getdate(), '" + sesion.Usuario.Id + "', 'Comprobante', 'Alta', '" + Comprobante.WF.Estado + "', '') ");
            }
            else
            {
                a.AppendLine("set @IdWF=" + comprobanteDesde.WF.Id.ToString() + " ");
                a.Append("update Comprobante set ");
                a.Append("Comprobante.DescrTipoComprobante='" + Comprobante.TipoComprobante.Descr + "', ");
                a.Append("Comprobante.NroLote=" + Comprobante.NroLote.ToString() + ", ");
                a.Append("Comprobante.IdTipoDoc=" + Comprobante.Documento.Tipo.Id + ", ");
                a.Append("Comprobante.NroDoc='" + Comprobante.Documento.Nro.ToString() + "', ");
                if (Comprobante.IdPersona == null)
                {
                    a.Append("Comprobante.IdPersona='', ");
                }
                else
                {
                    a.Append("Comprobante.IdPersona='" + Comprobante.IdPersona + "', ");
                }
                a.Append("Comprobante.DesambiguacionCuitPais=" + Comprobante.DesambiguacionCuitPais.ToString() + ", ");
                a.Append("Comprobante.RazonSocial='" + Comprobante.RazonSocial + "', ");
                if (Comprobante.Detalle == null)
                {
                    a.Append("Comprobante.Detalle='', ");
                }
                else
                {
                    a.Append("Comprobante.Detalle='" + Comprobante.Detalle + "', ");
                }    
                a.Append("Comprobante.Fecha='" + Comprobante.Fecha.ToString("yyyyMMdd") + "', ");
                a.Append("Comprobante.FechaVto='" + Comprobante.FechaVto.ToString("yyyyMMdd") + "', ");
                a.Append("Comprobante.Moneda='" + Comprobante.Moneda + "', ");
                a.Append("Comprobante.ImporteMoneda=" + Comprobante.ImporteMoneda.ToString("0000000000000.00", System.Globalization.CultureInfo.InvariantCulture) + ", ");
                a.Append("Comprobante.TipoCambio=" + Comprobante.TipoCambio.ToString("0000.000000", System.Globalization.CultureInfo.InvariantCulture) + ", ");
                a.Append("Comprobante.Importe=" + Comprobante.Importe.ToString("0000000000000.00", System.Globalization.CultureInfo.InvariantCulture) + ", ");
                a.Append("Comprobante.Request='" + Comprobante.Request + "', ");
                if (Comprobante.Response == null)
                {
                    a.Append("Comprobante.Response='', ");
                }
                else
                {
                    a.Append("Comprobante.Response='" + Comprobante.Response + "', ");
                }    
                a.Append("Comprobante.IdDestinoComprobante='" + Comprobante.IdDestinoComprobante + "', ");
                a.Append("Comprobante.Estado='" + Comprobante.WF.Estado + "', ");
                a.Append("Comprobante.IdNaturalezaComprobante='" + Comprobante.NaturalezaComprobante.Id + "', ");
                a.Append("Comprobante.PeriodicidadEmision='" + Comprobante.PeriodicidadEmision + "', ");
                a.Append("Comprobante.FechaProximaEmision='" + Comprobante.FechaProximaEmision.ToString("yyyyMMdd") + "', ");
                a.Append("Comprobante.CantidadComprobantesAEmitir=" + Comprobante.CantidadComprobantesAEmitir + ", ");
                a.Append("Comprobante.CantidadComprobantesEmitidos=" + Comprobante.CantidadComprobantesEmitidos + ", ");
                a.Append("Comprobante.CantidadDiasFechaVto=" + Comprobante.CantidadDiasFechaVto + ", ");
                a.Append("Comprobante.EmailAvisoComprobanteActivo=" + Convert.ToInt32(Comprobante.DatosEmailAvisoComprobanteContrato.Activo).ToString() + ", ");
                a.Append("Comprobante.IdDestinatarioFrecuente='" + Comprobante.DatosEmailAvisoComprobanteContrato.DestinatarioFrecuente.Id + "', ");
                a.Append("Comprobante.EmailAvisoComprobanteAsunto='" + Comprobante.DatosEmailAvisoComprobanteContrato.Asunto + "', ");
                a.Append("Comprobante.EmailAvisoComprobanteCuerpo='" + Comprobante.DatosEmailAvisoComprobanteContrato.Cuerpo + "' ");
                a.AppendLine("where Cuit='" + Comprobante.Cuit + "' and IdTipoComprobante=" + Comprobante.TipoComprobante.Id.ToString() + " and NroPuntoVta=" + Comprobante.NroPuntoVta.ToString() + " and Nrocomprobante=" + (Comprobante.NaturalezaComprobante.Id == "VentaContrato" ? -Comprobante.Nro : Comprobante.Nro).ToString()  + " and IdNaturalezaComprobante = '" + Comprobante.NaturalezaComprobante.Id + "' ");
                a.AppendLine("insert Log values (@idWF, getdate(), '" + sesion.Usuario.Id + "', 'Comprobante', 'Modif', '" + Comprobante.WF.Estado + "', '') ");
                a.AppendLine("declare @idLog int ");
                a.AppendLine("select @idLog=@@Identity ");
                a.AppendLine("insert LogDetalle (IdLog, TipoDetalle, Detalle) values (@idLog, 'Desde', '" + Funciones.ObjetoSerializado(comprobanteDesde) + "')");
                a.AppendLine("insert LogDetalle (IdLog, TipoDetalle, Detalle) values (@idLog, 'Hasta', '" + Funciones.ObjetoSerializado(Comprobante) + "') ");
                //Eliminar ComprobanteDetalle (minutas)
                a.AppendLine("delete ComprobanteDetalle where IdWF=@idWF ");
            }
            //Alta ComprobanteDetalle (minutas)
            for (int i = 0; i < Comprobante.Minutas.Count; i++)
            {
                a.AppendLine("insert ComprobanteDetalle (IdWF, IdTipoItem, NroItem, IdArticulo, IdRubro, Cantidad, PrecioUnitario, Importe, IdUbicacion, IndicadorExentoGravado, Detalle) ");
                a.AppendLine("values (@idWF, '" + Comprobante.Minutas[i].Item.IdTipo + "', " + Comprobante.Minutas[i].Item.Nro.ToString() + ", '" + Comprobante.Minutas[i].Articulo.Id + "', '" + Comprobante.Minutas[i].Rubro.Id + "', " + Comprobante.Minutas[i].Cantidad.ToString("0000000000000.00", System.Globalization.CultureInfo.InvariantCulture) + ", " + Comprobante.Minutas[i].PrecioUnitario.ToString("0000000000000.00", System.Globalization.CultureInfo.InvariantCulture) + ", " + Comprobante.Minutas[i].Importe.ToString("0000000000000.00", System.Globalization.CultureInfo.InvariantCulture) + ", '" + Comprobante.Minutas[i].IdUbicacion + "', '" + Comprobante.Minutas[i].IndicadorExentoGravado + "', '" + Comprobante.Minutas[i].Detalle + "') ");
            }
            Ejecutar(a.ToString(), TipoRetorno.None, Transaccion.Usa, sesion.CnnStr);
        }
        public int DarDeBaja(Entidades.Comprobante Comprobante)
        {
            StringBuilder a = new StringBuilder(string.Empty);
            a.AppendLine("if exists (select * from Comprobante where Cuit = '" + Comprobante.Cuit + "' and IdTipoComprobante=" + Comprobante.TipoComprobante.Id.ToString() + " and NroPuntoVta=" + Comprobante.NroPuntoVta.ToString() + " and Nrocomprobante = " + (Comprobante.NaturalezaComprobante.Id == "VentaContrato" ? -Comprobante.Nro : Comprobante.Nro).ToString()  + " and IdNaturalezaComprobante = '" + Comprobante.NaturalezaComprobante.Id + "' and UltActualiz = " + Comprobante.UltActualiz + ") ");
            a.AppendLine("BEGIN ");
            a.AppendLine("declare @idWF varchar(256) ");
            a.AppendLine("set @IdWF=" + Comprobante.WF.Id.ToString() + " ");
            a.Append("update Comprobante set ");
            Comprobante.WF.Estado = "DeBaja";
            a.Append("Comprobante.Estado='" + Comprobante.WF.Estado + "' ");
            a.AppendLine("where Cuit='" + Comprobante.Cuit + "' and IdTipoComprobante=" + Comprobante.TipoComprobante.Id.ToString() + " and NroPuntoVta=" + Comprobante.NroPuntoVta.ToString() + " and Nrocomprobante=" + (Comprobante.NaturalezaComprobante.Id == "VentaContrato" ? -Comprobante.Nro : Comprobante.Nro).ToString() + " and IdNaturalezaComprobante = '" + Comprobante.NaturalezaComprobante.Id + "' and UltActualiz = " + Comprobante.UltActualiz + " ");
            a.AppendLine("insert Log values (@idWF, getdate(), '" + sesion.Usuario.Id + "', 'Comprobante', 'Baja', '" + Comprobante.WF.Estado + "', '') ");
            a.AppendLine("END ");
            a.AppendLine("else ");
            a.AppendLine("BEGIN ");
            a.AppendLine("select @@rowcount ");
            a.AppendLine("END ");
            return (int)Ejecutar(a.ToString(), TipoRetorno.CantReg, Transaccion.Usa, sesion.CnnStr);
        }
        public int DarDeBajaFisica(Entidades.Comprobante Comprobante)
        {
            StringBuilder a = new StringBuilder(string.Empty);
            a.AppendLine("if exists (select * from Comprobante where Cuit = '" + Comprobante.Cuit + "' and IdTipoComprobante=" + Comprobante.TipoComprobante.Id.ToString() + " and NroPuntoVta=" + Comprobante.NroPuntoVta.ToString() + " and Nrocomprobante = " + (Comprobante.NaturalezaComprobante.Id == "VentaContrato" ? -Comprobante.Nro : Comprobante.Nro).ToString() + " and IdNaturalezaComprobante = '" + Comprobante.NaturalezaComprobante.Id + "' and UltActualiz = " + Comprobante.UltActualiz + ") ");
            a.AppendLine("BEGIN ");
            a.AppendLine("delete LogDetalle where IdLog in (select IdLog from Log where IdWF in (select IdWF from Comprobante where Cuit = '" + Comprobante.Cuit + "' and IdTipoComprobante=" + Comprobante.TipoComprobante.Id.ToString() + " and NroPuntoVta=" + Comprobante.NroPuntoVta.ToString() + " and Nrocomprobante = " + (Comprobante.NaturalezaComprobante.Id == "VentaContrato" ? -Comprobante.Nro : Comprobante.Nro).ToString() + " and IdNaturalezaComprobante = '" + Comprobante.NaturalezaComprobante.Id + "' and UltActualiz = " + Comprobante.UltActualiz + ")) ");
            a.AppendLine("delete Log where IdWF in (select IdWF from Comprobante where Cuit = '" + Comprobante.Cuit + "' and IdTipoComprobante=" + Comprobante.TipoComprobante.Id.ToString() + " and NroPuntoVta=" + Comprobante.NroPuntoVta.ToString() + " and Nrocomprobante = " + (Comprobante.NaturalezaComprobante.Id == "VentaContrato" ? -Comprobante.Nro : Comprobante.Nro).ToString() + " and IdNaturalezaComprobante = '" + Comprobante.NaturalezaComprobante.Id + "' and UltActualiz = " + Comprobante.UltActualiz + ") ");
            a.AppendLine("delete ComprobanteDetalle where IdWF in (select IdWF from Comprobante where Cuit = '" + Comprobante.Cuit + "' and IdTipoComprobante=" + Comprobante.TipoComprobante.Id.ToString() + " and NroPuntoVta=" + Comprobante.NroPuntoVta.ToString() + " and Nrocomprobante = " + (Comprobante.NaturalezaComprobante.Id == "VentaContrato" ? -Comprobante.Nro : Comprobante.Nro).ToString() + " and IdNaturalezaComprobante = '" + Comprobante.NaturalezaComprobante.Id + "' and UltActualiz = " + Comprobante.UltActualiz + ") ");
            a.AppendLine("delete Comprobante where IdWF in (select IdWF from Comprobante where Cuit = '" + Comprobante.Cuit + "' and IdTipoComprobante=" + Comprobante.TipoComprobante.Id.ToString() + " and NroPuntoVta=" + Comprobante.NroPuntoVta.ToString() + " and Nrocomprobante = " + (Comprobante.NaturalezaComprobante.Id == "VentaContrato" ? -Comprobante.Nro : Comprobante.Nro).ToString()  + " and IdNaturalezaComprobante = '" + Comprobante.NaturalezaComprobante.Id + "' and UltActualiz = " + Comprobante.UltActualiz + ") ");
            a.AppendLine("END ");
            a.AppendLine("else ");
            a.AppendLine("BEGIN ");
            a.AppendLine("select @@rowcount ");
            a.AppendLine("END ");
            return (int)Ejecutar(a.ToString(), TipoRetorno.CantReg, Transaccion.Usa, sesion.CnnStr);
        }
        public int AnularBaja(Entidades.Comprobante Comprobante)
        {
            StringBuilder a = new StringBuilder(string.Empty);
            a.AppendLine("if exists (select * from Comprobante where Cuit = '" + Comprobante.Cuit + "' and IdTipoComprobante=" + Comprobante.TipoComprobante.Id.ToString() + " and NroPuntoVta=" + Comprobante.NroPuntoVta.ToString() + " and Nrocomprobante = " + (Comprobante.NaturalezaComprobante.Id == "VentaContrato" ? -Comprobante.Nro : Comprobante.Nro).ToString() + " and IdNaturalezaComprobante = '" + Comprobante.NaturalezaComprobante.Id + "' and UltActualiz = " + Comprobante.UltActualiz + ") ");
            a.AppendLine("BEGIN ");
            a.AppendLine("declare @idWF varchar(256) ");
            a.AppendLine("set @IdWF=" + Comprobante.WF.Id.ToString() + " ");
            a.AppendLine("declare @EstadoAnterior varchar(15) ");
            a.AppendLine("select top 1 @EstadoAnterior=Estado from Log where IdWF=@IdWF and Estado<>'DeBaja' order by IdLog desc ");
            a.Append("update Comprobante set ");
            a.Append("Comprobante.Estado=@EstadoAnterior ");
            a.AppendLine("where Cuit='" + Comprobante.Cuit + "' and IdTipoComprobante=" + Comprobante.TipoComprobante.Id.ToString() + " and NroPuntoVta=" + Comprobante.NroPuntoVta.ToString() + " and Nrocomprobante=" + (Comprobante.NaturalezaComprobante.Id == "VentaContrato" ? -Comprobante.Nro : Comprobante.Nro).ToString() + " and IdNaturalezaComprobante = '" + Comprobante.NaturalezaComprobante.Id + "' and UltActualiz = " + Comprobante.UltActualiz + " ");
            a.AppendLine("insert Log values (@idWF, getdate(), '" + sesion.Usuario.Id + "', 'Comprobante', 'AnulBaja', @EstadoAnterior, '') ");
            a.AppendLine("select @@rowcount as CantReg, @EstadoAnterior as Estado ");
            a.AppendLine("END ");
            a.AppendLine("else ");
            a.AppendLine("BEGIN ");
            a.AppendLine("select @@rowcount as CantReg, '' as Estado ");
            a.AppendLine("END ");
            DataTable tb = (DataTable)Ejecutar(a.ToString(), TipoRetorno.TB, Transaccion.Usa, sesion.CnnStr);
            int CantReg = 0;
            if (tb.Rows.Count == 1)
            {
                CantReg = Convert.ToInt32(tb.Rows[0]["CantReg"].ToString());
                Comprobante.WF.Estado = tb.Rows[0]["Estado"].ToString();
            }
            return CantReg;
        }
        public List<Entidades.Comprobante> ListaGlobalFiltrada(bool SoloVigentes, bool EsFechaAlta, string FechaDesde, string FechaHasta, Entidades.Persona Persona, string CUIT, string CUITRazonSocial, string NroComprobante)
        {
            List<Entidades.Comprobante> lista = new List<Entidades.Comprobante>();
            if (sesion.Cuit.Nro != null)
            {
                System.Text.StringBuilder a = new StringBuilder();
                a.Append("select ");
                a.Append("Comprobante.Cuit, Cuit.RazonSocial as CUITRazonSocial, convert(datetime, Log.Fecha, 120) as FechaAlta, Comprobante.IdTipoComprobante, Comprobante.DescrTipoComprobante, Comprobante.NroPuntoVta, Comprobante.NroComprobante, Comprobante.NroLote, Comprobante.IdTipoDoc, Comprobante.NroDoc, Comprobante.IdPersona, Comprobante.DesambiguacionCuitPais, Comprobante.RazonSocial, Comprobante.Detalle, Comprobante.Fecha, Comprobante.FechaVto, Comprobante.Moneda, Comprobante.ImporteMoneda, Comprobante.TipoCambio, Comprobante.Importe, Comprobante.Request, Comprobante.Response, Comprobante.IdDestinoComprobante, Comprobante.IdWF, Comprobante.Estado, Comprobante.UltActualiz, Comprobante.IdNaturalezaComprobante, NaturalezaComprobante.DescrNaturalezaComprobante, Comprobante.PeriodicidadEmision, Comprobante.FechaProximaEmision, Comprobante.CantidadComprobantesAEmitir, Comprobante.CantidadComprobantesEmitidos, Comprobante.CantidadDiasFechaVto, Comprobante.EmailAvisoComprobanteActivo, Comprobante.IdDestinatarioFrecuente, Comprobante.EmailAvisoComprobanteAsunto, Comprobante.EmailAvisoComprobanteCuerpo ");
                a.Append("from Comprobante, Cuit, Log, NaturalezaComprobante ");
                a.Append("where Comprobante.Cuit = Cuit.Cuit and Comprobante.IdWF = Log.IdWF and Log.Evento = 'Alta' ");
                a.Append("and Comprobante.IdNaturalezaComprobante=NaturalezaComprobante.IdNaturalezaComprobante ");
                if (SoloVigentes)
                {
                    a.Append("and Comprobante.Estado='Vigente' ");
                }
                if (EsFechaAlta)
                {
                    if (FechaDesde != String.Empty)
                    {
                        a.Append("and Log.Fecha >= '" + Convert.ToDateTime(FechaDesde, new System.Globalization.CultureInfo("es-AR")).ToString("yyyyMMdd") + "' ");
                    }
                    if (FechaHasta != String.Empty)
                    {
                        string FechaAuxHasta = Convert.ToDateTime(FechaHasta, new System.Globalization.CultureInfo("es-AR")).AddDays(1).ToString("yyyyMMdd");
                        a.Append("and Log.Fecha < '" + FechaAuxHasta + "' ");
                    }
                }
                else
                {
                    if (FechaDesde != String.Empty)
                    {
                        a.Append("and Comprobante.Fecha >= '" + Convert.ToDateTime(FechaDesde, new System.Globalization.CultureInfo("es-AR")).ToString("yyyyMMdd") + "' ");
                    }
                    if (FechaHasta != String.Empty)
                    {
                        a.Append("and Comprobante.Fecha <= '" + Convert.ToDateTime(FechaHasta, new System.Globalization.CultureInfo("es-AR")).ToString("yyyyMMdd") + "' ");
                    }
                }
                if (CUIT != String.Empty)
                {
                    a.Append("and Comprobante.Cuit like '%" + CUIT + "%' ");
                }
                if (CUITRazonSocial != String.Empty)
                {
                    a.Append("and Cuit.RazonSocial like '%" + CUITRazonSocial + "%' ");
                }
                if (NroComprobante != String.Empty)
                {
                    a.Append("and Comprobante.NroComprobante = " + NroComprobante + " ");
                }
                //if (Persona.Orden != 0)
                //{
                //    a.Append("and Comprobante.IdTipoDoc=" + Persona.Documento.Tipo.Id + " ");
                //    a.Append("and Comprobante.NroDoc='" + Persona.Documento.Nro.ToString() + "' ");
                //    a.Append("and Comprobante.IdPersona='" + Persona.IdPersona + "' ");
                //    a.Append("and Comprobante.DesambiguacionCuitPais=" + Persona.DesambiguacionCuitPais.ToString() + " ");
                //}
                a.Append("order by Comprobante.DescrTipoComprobante desc, Comprobante.NroPuntoVta desc, Comprobante.NroComprobante desc ");
                DataTable dt = (DataTable)Ejecutar(a.ToString(), TipoRetorno.TB, Transaccion.NoAcepta, sesion.CnnStr);
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Entidades.Comprobante elem = new Entidades.Comprobante();
                        Copiar(dt.Rows[i], elem);
                        elem.CuitRazonSocial = Convert.ToString(dt.Rows[i]["CUITRazonSocial"]);
                        elem.FechaAlta = Convert.ToDateTime(dt.Rows[i]["FechaAlta"]);
                        lista.Add(elem);
                    }
                }
            }
            return lista;
        }
        private void Copiar(DataRow Desde, Entidades.Comprobante Hasta)
        {
            Hasta.Cuit = Convert.ToString(Desde["Cuit"]);
            Hasta.TipoComprobante.Id = Convert.ToInt32(Desde["IdTipoComprobante"]);
            Hasta.TipoComprobante.Descr = Convert.ToString(Desde["DescrTipoComprobante"]);
            Hasta.NroPuntoVta = Convert.ToInt32(Desde["NroPuntoVta"]);
            Hasta.Nro = Math.Abs(Convert.ToInt64(Desde["NroComprobante"]));
            Hasta.NroLote = Convert.ToInt64(Desde["NroLote"]);
            Hasta.Documento.Tipo.Id = Convert.ToString(Desde["IdTipoDoc"]);
            FeaEntidades.Documentos.Documento tipoDocumento = FeaEntidades.Documentos.Documento.Lista().Find(delegate(FeaEntidades.Documentos.Documento d)
            {
                return Hasta.Documento.Tipo.Id == d.Codigo.ToString();
            });
            if (tipoDocumento != null)
            {
                Hasta.Documento.Tipo.Descr = tipoDocumento.Descr;
            }
            else
            {
                Hasta.Documento.Tipo.Descr = "Desconocido";
            }
            Hasta.Documento.Nro = Convert.ToString(Desde["NroDoc"]);
            Hasta.IdPersona = Convert.ToString(Desde["IdPersona"]);
            Hasta.DesambiguacionCuitPais = Convert.ToInt32(Desde["DesambiguacionCuitPais"]);
            Hasta.RazonSocial = Convert.ToString(Desde["RazonSocial"]);
            Hasta.Detalle = Convert.ToString(Desde["Detalle"]);
            Hasta.Fecha = Convert.ToDateTime(Desde["Fecha"]);
            Hasta.FechaVto = Convert.ToDateTime(Desde["FechaVto"]);
            Hasta.Moneda = Convert.ToString(Desde["Moneda"]);
            Hasta.ImporteMoneda = Convert.ToDouble(Desde["ImporteMoneda"]);
            Hasta.TipoCambio = Convert.ToDouble(Desde["TipoCambio"]);
            Hasta.Importe = Convert.ToDouble(Desde["Importe"]);
            Hasta.Request = Convert.ToString(Desde["Request"]);
            Hasta.Response = Convert.ToString(Desde["Response"]);
            Hasta.IdDestinoComprobante = Convert.ToString(Desde["IdDestinoComprobante"]);
            Hasta.WF.Id = Convert.ToInt32(Desde["IdWF"]);
            Hasta.WF.Estado = Convert.ToString(Desde["Estado"]);
            Hasta.UltActualiz = ByteArray2TimeStamp((byte[])Desde["UltActualiz"]);
            Hasta.NaturalezaComprobante.Id = Convert.ToString(Desde["IdNaturalezaComprobante"]);
            Hasta.NaturalezaComprobante.Descr = Convert.ToString(Desde["DescrNaturalezaComprobante"]);
            switch (Hasta.NaturalezaComprobante.Id)
            {
                case "Compra":
                    Hasta.NaturalezaComprobante.Descr = "Compra";
                    break;
                case "Venta":
                    Hasta.NaturalezaComprobante.Descr = "Venta";
                    break;
                case "VentaTradic":
                    Hasta.NaturalezaComprobante.Descr = "Vta.Tradic ";
                    break;
                case "VentaContrato":
                    Hasta.NaturalezaComprobante.Descr = "Contrato";
                    break;
            }
            Hasta.PeriodicidadEmision = Convert.ToString(Desde["PeriodicidadEmision"]);
            Hasta.FechaProximaEmision = Convert.ToDateTime(Desde["FechaProximaEmision"]);
            Hasta.CantidadComprobantesAEmitir = Convert.ToInt32(Desde["CantidadComprobantesAEmitir"]);
            Hasta.CantidadComprobantesEmitidos = Convert.ToInt32(Desde["CantidadComprobantesEmitidos"]);
            Hasta.CantidadDiasFechaVto = Convert.ToInt32(Desde["CantidadDiasFechaVto"]);
            Hasta.DatosEmailAvisoComprobanteContrato.Activo = Convert.ToBoolean(Desde["EmailAvisoComprobanteActivo"]);
            Hasta.DatosEmailAvisoComprobanteContrato.DestinatarioFrecuente.Id = Convert.ToString(Desde["IdDestinatarioFrecuente"]);
            Hasta.DatosEmailAvisoComprobanteContrato.Asunto = Convert.ToString(Desde["EmailAvisoComprobanteAsunto"]);
            Hasta.DatosEmailAvisoComprobanteContrato.Cuerpo = Convert.ToString(Desde["EmailAvisoComprobanteCuerpo"]);
        }
        public void Actualizar(Entidades.Comprobante Comprobante)
        {
            System.Text.StringBuilder a = new StringBuilder();
            a.Append("insert Log values (" + Comprobante.WF.Id + ", getdate(), '" + sesion.Usuario.Id + "', 'Comprobante', 'CambioEstado', '" + Comprobante.Estado + "', '') ");
            a.Append("update Comprobante set Response = '" + Comprobante.Response + "', Estado = '" + Comprobante.Estado + "', ");
            a.Append("Comprobante.IdTipoDoc='" + Comprobante.Documento.Tipo.Id + "', ");
            a.Append("Comprobante.NroDoc='" + Comprobante.Documento.Nro.ToString() + "', ");
            //a.Append("Comprobante.Fecha='" + Comprobante.Fecha.ToString("yyyyMMdd") + "', ");
            //a.Append("Comprobante.FechaVto='" + Comprobante.FechaVto.ToString("yyyyMMdd") + "', ");
            a.Append("Comprobante.Moneda='" + Comprobante.Moneda + "', ");
            a.Append("Comprobante.ImporteMoneda=" + Comprobante.ImporteMoneda.ToString("0000000000000.00", System.Globalization.CultureInfo.InvariantCulture) + ", ");
            a.Append("Comprobante.TipoCambio=" + Comprobante.TipoCambio.ToString("0000.000000", System.Globalization.CultureInfo.InvariantCulture) + ", ");
            a.Append("Comprobante.Importe=" + Comprobante.Importe.ToString("0000000000000.00", System.Globalization.CultureInfo.InvariantCulture) + " ");
            a.Append("where Comprobante.Cuit='" + sesion.Cuit.Nro + "' and Comprobante.IdTipoComprobante=" + Comprobante.TipoComprobante.Id.ToString() + " and Comprobante.NroPuntoVta=" + Comprobante.NroPuntoVta.ToString() + " and Comprobante.NroComprobante=" + Comprobante.Nro.ToString() + " and Comprobante.IdNaturalezaComprobante = '" + Comprobante.NaturalezaComprobante.Id + "'");
            int Cantidad = (int)Ejecutar(a.ToString(), TipoRetorno.CantReg, Transaccion.NoAcepta, sesion.CnnStr);
        }
        public void LeerDestinatarioFrecuente(Entidades.Persona Persona, Entidades.Comprobante Contrato)
        {
            System.Text.StringBuilder a = new StringBuilder();
            a.Append("select DestinatarioFrecuente.Para, DestinatarioFrecuente.Cc ");
            a.Append("from DestinatarioFrecuente ");
            a.Append("where DestinatarioFrecuente.Cuit='" + Persona.Cuit + "' and DestinatarioFrecuente.IdTipoDoc = " + Persona.Documento.Tipo.Id + " and DestinatarioFrecuente.NroDoc = '" + Persona.Documento.Nro.ToString() + "' and DestinatarioFrecuente.IdPersona = '" + Persona.IdPersona + "' and DestinatarioFrecuente.DesambiguacionCuitPais = " + Persona.DesambiguacionCuitPais.ToString() + " and IdDestinatarioFrecuente='" + Contrato.DatosEmailAvisoComprobanteContrato.DestinatarioFrecuente.Id + "' ");
            DataTable dt = (DataTable)Ejecutar(a.ToString(), TipoRetorno.TB, Transaccion.NoAcepta, sesion.CnnStr);
            if (dt.Rows.Count != 0)
            {
                Contrato.DatosEmailAvisoComprobanteContrato.DestinatarioFrecuente.Para = dt.Rows[0]["Para"].ToString();
                Contrato.DatosEmailAvisoComprobanteContrato.DestinatarioFrecuente.Cc = dt.Rows[0]["Cc"].ToString();
            }
        }
        public void LeerUltimoEmitido(Entidades.Comprobante Comprobante)
        {
            System.Text.StringBuilder a = new StringBuilder();
            a.Append("select top 1");
            a.Append("Comprobante.Cuit, Comprobante.IdTipoComprobante, Comprobante.DescrTipoComprobante, Comprobante.NroPuntoVta, Comprobante.NroComprobante, Comprobante.NroLote, Comprobante.IdTipoDoc, Comprobante.NroDoc, Comprobante.IdPersona, Comprobante.DesambiguacionCuitPais, Comprobante.RazonSocial, Comprobante.Detalle, Comprobante.Fecha, Comprobante.FechaVto, Comprobante.Moneda, Comprobante.ImporteMoneda, Comprobante.TipoCambio, Comprobante.Importe, Comprobante.Request, Comprobante.Response, Comprobante.IdDestinoComprobante, Comprobante.IdWF, Comprobante.Estado, Comprobante.UltActualiz, Comprobante.IdNaturalezaComprobante, NaturalezaComprobante.DescrNaturalezaComprobante, Comprobante.PeriodicidadEmision, Comprobante.FechaProximaEmision, Comprobante.CantidadComprobantesAEmitir, Comprobante.CantidadComprobantesEmitidos, Comprobante.CantidadDiasFechaVto, Comprobante.EmailAvisoComprobanteActivo, Comprobante.IdDestinatarioFrecuente, Comprobante.EmailAvisoComprobanteAsunto, Comprobante.EmailAvisoComprobanteCuerpo ");
            a.Append("from Comprobante, NaturalezaComprobante  ");
            a.Append("where Comprobante.Cuit='" + sesion.Cuit.Nro + "' and Comprobante.IdTipoComprobante=" + Comprobante.TipoComprobante.Id.ToString() + " and Comprobante.NroPuntoVta=" + Comprobante.NroPuntoVta.ToString() + " and Comprobante.IdNaturalezaComprobante='" + Comprobante.NaturalezaComprobante.Id + "' ");
            a.Append("and Comprobante.IdNaturalezaComprobante=NaturalezaComprobante.IdNaturalezaComprobante and Comprobante.Estado<>'DeBaja' ");
            a.Append("order by ABS(NroComprobante) desc ");
            DataTable dt = (DataTable)Ejecutar(a.ToString(), TipoRetorno.TB, Transaccion.NoAcepta, sesion.CnnStr);
            if (dt.Rows.Count != 0)
            {
                Copiar(dt.Rows[0], Comprobante);
            }
        }
        public void ActualizarFechaProximaEmision(Entidades.Comprobante Comprobante)
        {
            StringBuilder a = new StringBuilder(string.Empty);
            a.Append("update Comprobante set ");
            a.Append("Comprobante.FechaProximaEmision='" + Comprobante.FechaProximaEmision.ToString("yyyyMMdd") + "' ");
            a.AppendLine("where Cuit='" + Comprobante.Cuit + "' and IdTipoComprobante=" + Comprobante.TipoComprobante.Id.ToString() + " and NroPuntoVta=" + Comprobante.NroPuntoVta.ToString() + " and Nrocomprobante=" + (Comprobante.NaturalezaComprobante.Id == "VentaContrato" ? -Comprobante.Nro : Comprobante.Nro).ToString() + " ");
            Ejecutar(a.ToString(), TipoRetorno.None, Transaccion.Usa, sesion.CnnStr);
        }
        public List<Entidades.StockXArticuloDetalle> ListaStock(string Cuit, string FechaHasta)
        {
            List<Entidades.StockXArticuloDetalle> lista = new List<Entidades.StockXArticuloDetalle>();
            StringBuilder a = new StringBuilder(string.Empty);
            a.Append("select Comprobante.Cuit, IdNaturalezaComprobante, IdTipoComprobante, DescrTipoComprobante, NroPuntoVta, NroComprobante, IdTipoDoc, NroDoc, RazonSocial, ");
            a.Append("Moneda, ImporteMoneda, Fecha, TipoCambio, Comprobante.Importe as ImporteTotalComprabante, Comprobante.IdWF, Comprobante.Estado, ");
            a.Append("IdTipoItem, NroItem, ComprobanteDetalle.IdArticulo, Articulo.DescrArticulo, IdRubro, Cantidad, PrecioUnitario, ComprobanteDetalle.Importe, IdUbicacion, IndicadorExentoGravado ");
            a.Append("from Comprobante, ComprobanteDetalle, Articulo where Comprobante.IdWF = ComprobanteDetalle.IdWF and ComprobanteDetalle.IdArticulo = Articulo.IdArticulo and Articulo.Cuit = Comprobante.Cuit ");
            a.Append("and Comprobante.Fecha <= '" + FechaHasta + "' and Comprobante.Cuit = '" + Cuit + "'");
            DataTable dt = (DataTable)Ejecutar(a.ToString(), TipoRetorno.TB, Transaccion.NoAcepta, sesion.CnnStr);
            if (dt.Rows.Count != 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Entidades.StockXArticuloDetalle elem = new Entidades.StockXArticuloDetalle();
                    CopiarListaStock(dt.Rows[i], elem);
                    lista.Add(elem);
                }
            }
            return lista;
        }
        private void CopiarListaStock(DataRow Desde, Entidades.StockXArticuloDetalle Hasta)
        {
            Hasta.IdArticulo = Convert.ToString(Desde["IdArticulo"]); ;
            Hasta.Descr = Convert.ToString(Desde["DescrArticulo"]); ;
            Hasta.IdNaturalezaComprobante = Convert.ToString(Desde["IdNaturalezaComprobante"]);
            Hasta.CompTipo = Convert.ToString(Desde["IdTipoComprobante"]);
            Hasta.CompPtoVta = Convert.ToString(Desde["NroPuntoVta"]);
            Hasta.CompNro = Math.Abs(Convert.ToInt64(Desde["NroComprobante"])).ToString();
            //Hasta.NroLote = Convert.ToInt64(Desde["NroLote"]);
            Hasta.EmpCodDoc = Convert.ToString(Desde["IdTipoDoc"]);
            FeaEntidades.Documentos.Documento tipoDocumento = FeaEntidades.Documentos.Documento.Lista().Find(delegate(FeaEntidades.Documentos.Documento d)
            {
                return Hasta.EmpCodDoc == d.Codigo.ToString();
            });
            if (tipoDocumento != null)
            {
                Hasta.EmpDescrDoc = tipoDocumento.Descr;
            }
            else
            {
                Hasta.EmpDescrDoc = "Desconocido";
            }
            Hasta.EmpNroDoc = Convert.ToString(Desde["NroDoc"]);
            Hasta.EmpNombre = Convert.ToString(Desde["RazonSocial"]);
            Hasta.CompFecEmi = Convert.ToDateTime(Desde["Fecha"]).ToString("dd/MM/yyyy");
            Hasta.Moneda = Convert.ToString(Desde["Moneda"]);
            Hasta.TipoCambio = Convert.ToDouble(Desde["TipoCambio"]);
            Hasta.Cantidad = Convert.ToDouble(Desde["Cantidad"]);
            Hasta.PrecioUnitario = Convert.ToDouble(Desde["PrecioUnitario"]);
            Hasta.ImporteTotal = Convert.ToDouble(Desde["Importe"]);
        }
        public int CantidadDeFilasParaLista(Entidades.Request.ComprobanteListaRequest comprobanteListaRequest)
        {
            StringBuilder a = new StringBuilder(string.Empty);
            a.AppendLine("select COUNT(Comprobante.Cuit) ");
            a.AppendLine("from Comprobante where 1=1 ");
            a.Append(FiltroParaLista(comprobanteListaRequest));
            DataTable dt = new DataTable();
            dt = (DataTable)Ejecutar(a.ToString(), TipoRetorno.TB, Transaccion.NoAcepta, sesion.CnnStr);
            return Convert.ToInt32(dt.Rows[0][0]);
        }
        public List<Entidades.Comprobante> Lista(Entidades.Request.ComprobanteListaRequest comprobanteListaRequest)
        {

            System.Text.StringBuilder a = new StringBuilder();
            a.Append("Select Comprobante.Cuit, Comprobante.IdTipoComprobante, Comprobante.DescrTipoComprobante, Comprobante.NroPuntoVta, Comprobante.NroComprobante, Comprobante.NroLote, Comprobante.IdTipoDoc, Comprobante.NroDoc, Comprobante.IdPersona, Comprobante.DesambiguacionCuitPais, Comprobante.RazonSocial, Comprobante.Detalle, Comprobante.Fecha, Comprobante.FechaVto, Comprobante.Moneda, Comprobante.ImporteMoneda, Comprobante.TipoCambio, Comprobante.Importe, Comprobante.Request, Comprobante.Response, Comprobante.IdDestinoComprobante, Comprobante.IdWF, Comprobante.Estado, Comprobante.UltActualiz, Comprobante.IdNaturalezaComprobante, NaturalezaComprobante.DescrNaturalezaComprobante, Comprobante.PeriodicidadEmision, Comprobante.FechaProximaEmision, Comprobante.CantidadComprobantesAEmitir, Comprobante.CantidadComprobantesEmitidos, Comprobante.CantidadDiasFechaVto, Comprobante.EmailAvisoComprobanteActivo, Comprobante.IdDestinatarioFrecuente, Comprobante.EmailAvisoComprobanteAsunto, Comprobante.EmailAvisoComprobanteCuerpo ");
            a.Append("from (select top {0} ROW_NUMBER() OVER (ORDER BY {1}) as ROW_NUM, ");
            a.Append("Cuit, IdTipoComprobante, DescrTipoComprobante, NroPuntoVta, NroComprobante, NroLote, IdTipoDoc, NroDoc, IdPersona, DesambiguacionCuitPais, RazonSocial, Detalle, Fecha, FechaVto, Moneda, ImporteMoneda, TipoCambio, Importe, Request, Response, IdDestinoComprobante, IdWF, Estado, UltActualiz, IdNaturalezaComprobante, DescrNaturalezaComprobante, PeriodicidadEmision, FechaProximaEmision, CantidadComprobantesAEmitir, CantidadComprobantesEmitidos, CantidadDiasFechaVto, EmailAvisoComprobanteActivo, IdDestinatarioFrecuente, EmailAvisoComprobanteAsunto, EmailAvisoComprobanteCuerpo ");
            a.Append("from Comprobante where 1=1 ");
            a.Append(FiltroParaLista(comprobanteListaRequest));
            a.Append("ORDER BY ROW_NUM) innerSelect WHERE ROW_NUM > {2} ");
            string[] se = ModificarSortExpression(comprobanteListaRequest.Paginacion.OrderBy);
            comprobanteListaRequest.Paginacion.OrderBy = se[1];
            string commandText = string.Format(a.ToString(), ((comprobanteListaRequest.Paginacion.Pagina) * sesion.Usuario.CantidadFilasXPagina), se[0], ((comprobanteListaRequest.Paginacion.Pagina - 1) * sesion.Usuario.CantidadFilasXPagina));
            DataTable dt = new DataTable();
            dt = (DataTable)Ejecutar(commandText.ToString(), TipoRetorno.TB, Transaccion.NoAcepta, sesion.CnnStr);
            List<Entidades.Comprobante> lista = new List<Entidades.Comprobante>();
            if (dt.Rows.Count != 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Entidades.Comprobante Comprobante = new Entidades.Comprobante();
                    Copiar_ListaPaging(dt.Rows[i], Comprobante);
                    lista.Add(Comprobante);
                }
            }
            return lista;
        }
        public string FiltroParaLista(Entidades.Request.ComprobanteListaRequest comprobanteListaRequest)
        {
            System.Text.StringBuilder a = new StringBuilder();
            string estados = String.Empty;
            //for (int i = 0; i < comprobanteListaRequest.Estados.Count; i++)
            //{
            //    estados += "'" + comprobanteListaRequest.Estados[i].Id + "'";
            //    if (i != (comprobanteListaRequest.Estados.Count - 1)) estados += ", ";
            //}
            //string estadosCompras = String.Empty;
            //for (int i = 0; i < comprobanteListaRequest.EstadosCompra.Count; i++)
            //{
            //    estadosCompras += "'" + comprobanteListaRequest.EstadosCompra[i].Id + "'";
            //    if (i != (comprobanteListaRequest.EstadosCompra.Count - 1)) estadosCompras += ", ";
            //}
            //if (estados != String.Empty && estadosCompras != String.Empty)
            //{
            //    a.Append("and ((Comprobante.Estado in (" + estados + ") and Comprobante.IdNaturalezaComprobante='Venta') or (Comprobante.Estado in (" + estadosCompras + ") and Comprobante.IdNaturalezaComprobante<>'Venta')) ");
            //}
            //else if (estados != String.Empty)
            //{
            //    a.Append("and (Comprobante.Estado in (" + estados + ") and Comprobante.IdNaturalezaComprobante='Venta') ");
            //}
            //else if (estadosCompras != String.Empty)
            //{
            //    a.Append("and (Comprobante.Estado in (" + estadosCompras + ") and Comprobante.IdNaturalezaComprobante<>'Venta') ");
            //}
            //string tiposComprobante = String.Empty;
            //for (int i = 0; i < comprobanteListaRequest.TiposDeComprobante.Count; i++)
            //{
            //    tiposComprobante += "'" + comprobanteListaRequest.TiposDeComprobante[i].Id + "'";
            //    if (i != (comprobanteListaRequest.TiposDeComprobante.Count - 1)) tiposComprobante += ", ";
            //}
            //if (tiposComprobante != String.Empty)
            //{
            //    a.Append("and Comprobante.IdTipoComprobante in (" + tiposComprobante + ") ");
            //}
            //if (comprobanteListaRequest.FechaDsd != String.Empty)
            //{
            //    a.Append("and Comprobante.Fecha>='" + comprobanteListaRequest.FechaDsd + "' ");
            //}
            //if (comprobanteListaRequest.FechaHst != String.Empty)
            //{
            //    a.Append("and Comprobante.Fecha<='" + comprobanteListaRequest.FechaHst + "' ");
            //}
            //if (comprobanteListaRequest.PuntoDeVenta != String.Empty)
            //{
            //    a.Append("and Comprobante.NroPuntoVta='" + comprobanteListaRequest.PuntoDeVenta + "' ");
            //}
            //if (comprobanteListaRequest.NumeroDeComprobante != String.Empty)
            //{
            //    a.Append("and Comprobante.NroComprobante='" + comprobanteListaRequest.NumeroDeComprobante + "' ");
            //}
            //if (comprobanteListaRequest.Cuit != String.Empty)
            //{
            //    a.Append("and Comprobante.IdTipoDoc=80 ");
            //    a.Append("and Comprobante.NroDoc='" + comprobanteListaRequest.Cuit.ToString() + "' ");
            //    //a.Append("and Comprobante.IdPersona='" + Persona.IdPersona + "' ");
            //    //a.Append("and Comprobante.DesambiguacionCuitPais=" + Persona.DesambiguacionCuitPais.ToString() + " ");
            //}
            //if (comprobanteListaRequest.NaturalezaComprobante.Id != String.Empty)
            //{
            //    a.Append("and Comprobante.IdNaturalezaComprobante='" + comprobanteListaRequest.NaturalezaComprobante.Id + "' ");
            //}
            //if (comprobanteListaRequest.Detalle != string.Empty)
            //{
            //    a.Append("and Comprobante.Detalle like '%" + comprobanteListaRequest.Detalle + "%' ");
            //}
            return a.ToString();
        }
        private string[] ModificarSortExpression(string SortExpression)
        {
            string[] se = new string[2];
            SortExpression = SortExpression.Trim().ToUpper();
            se[1] = SortExpression;
            switch (SortExpression)
            {
                case "RAZONSOCIAL":
                case "RAZONSOCIAL DESC":
                case "RAZONSOCIAL ASC":
                case "DESCRNATURALEZACOMPROBANTE":
                case "DESCRNATURALEZACOMPROBANTE DESC":
                case "DESCRNATURALEZACOMPROBANTE ASC":
                case "DESCRTIPOCOMPROBANTE":
                case "DESCRTIPOCOMPROBANTE DESC":
                case "DESCRTIPOCOMPROBANTE ASC":
                case "NROCOMPROBANTE":
                case "NROCOMPROBANTE DESC":
                case "NROCOMPROBANTE ASC":
                case "NROPUNTOVTA":
                case "NROPUNTOVTA DESC":
                case "NROPUNTOVTA ASC":
                case "FECHA":
                case "FECHA DESC":
                case "FECHA ASC":
                case "ESTADO":
                case "ESTADO DESC":
                case "ESTADO ASC":
                    se[0] = "Comprobante." + SortExpression;
                    break;
                default:
                    se[0] = "Comprobante.NROCOMPROBANTE";
                    se[1] = "ID";
                    break;
            }
            return se;
        }
        private void Copiar_ListaPaging(DataRow Desde, Entidades.Comprobante Hasta)
        {
            Hasta.Cuit = Convert.ToString(Desde["Cuit"]);
            Hasta.TipoComprobante.Id = Convert.ToInt32(Desde["IdTipoComprobante"]);
            Hasta.TipoComprobante.Descr = Convert.ToString(Desde["DescrTipoComprobante"]);
            Hasta.NroPuntoVta = Convert.ToInt32(Desde["NroPuntoVta"]);
            Hasta.Nro = Math.Abs(Convert.ToInt64(Desde["NroComprobante"]));
            Hasta.NroLote = Convert.ToInt64(Desde["NroLote"]);
            Hasta.Documento.Tipo.Id = Convert.ToString(Desde["IdTipoDoc"]);
            FeaEntidades.Documentos.Documento tipoDocumento = FeaEntidades.Documentos.Documento.Lista().Find(delegate (FeaEntidades.Documentos.Documento d)
            {
                return Hasta.Documento.Tipo.Id == d.Codigo.ToString();
            });
            if (tipoDocumento != null)
            {
                Hasta.Documento.Tipo.Descr = tipoDocumento.Descr;
            }
            else
            {
                Hasta.Documento.Tipo.Descr = "Desconocido";
            }
            Hasta.Documento.Nro = Convert.ToString(Desde["NroDoc"]);
            Hasta.IdPersona = Convert.ToString(Desde["IdPersona"]);
            Hasta.DesambiguacionCuitPais = Convert.ToInt32(Desde["DesambiguacionCuitPais"]);
            Hasta.RazonSocial = Convert.ToString(Desde["RazonSocial"]);
            Hasta.Detalle = Convert.ToString(Desde["Detalle"]);
            Hasta.Fecha = Convert.ToDateTime(Desde["Fecha"]);
            Hasta.FechaVto = Convert.ToDateTime(Desde["FechaVto"]);
            Hasta.Moneda = Convert.ToString(Desde["Moneda"]);
            Hasta.ImporteMoneda = Convert.ToDouble(Desde["ImporteMoneda"]);
            Hasta.TipoCambio = Convert.ToDouble(Desde["TipoCambio"]);
            Hasta.Importe = Convert.ToDouble(Desde["Importe"]);
            Hasta.Request = Convert.ToString(Desde["Request"]);
            Hasta.Response = Convert.ToString(Desde["Response"]);
            Hasta.IdDestinoComprobante = Convert.ToString(Desde["IdDestinoComprobante"]);
            Hasta.WF.Id = Convert.ToInt32(Desde["IdWF"]);
            Hasta.WF.Estado = Convert.ToString(Desde["Estado"]);
            Hasta.UltActualiz = Convert.ToString(Desde["UltActualiz"]);
            Hasta.NaturalezaComprobante.Id = Convert.ToString(Desde["IdNaturalezaComprobante"]);
            Hasta.NaturalezaComprobante.Descr = Convert.ToString(Desde["DescrNaturalezaComprobante"]);
            switch (Hasta.NaturalezaComprobante.Id)
            {
                case "Compra":
                    Hasta.NaturalezaComprobante.Descr = "Compra";
                    break;
                case "Venta":
                    Hasta.NaturalezaComprobante.Descr = "Venta";
                    break;
                case "VentaTradic":
                    Hasta.NaturalezaComprobante.Descr = "Vta.Tradic ";
                    break;
                case "VentaContrato":
                    Hasta.NaturalezaComprobante.Descr = "Contrato";
                    break;
            }
            Hasta.PeriodicidadEmision = Convert.ToString(Desde["PeriodicidadEmision"]);
            Hasta.FechaProximaEmision = Convert.ToDateTime(Desde["FechaProximaEmision"]);
            Hasta.CantidadComprobantesAEmitir = Convert.ToInt32(Desde["CantidadComprobantesAEmitir"]);
            Hasta.CantidadComprobantesEmitidos = Convert.ToInt32(Desde["CantidadComprobantesEmitidos"]);
            Hasta.CantidadDiasFechaVto = Convert.ToInt32(Desde["CantidadDiasFechaVto"]);
            Hasta.DatosEmailAvisoComprobanteContrato.Activo = Convert.ToBoolean(Desde["EmailAvisoComprobanteActivo"]);
            Hasta.DatosEmailAvisoComprobanteContrato.DestinatarioFrecuente.Id = Convert.ToString(Desde["IdDestinatarioFrecuente"]);
            Hasta.DatosEmailAvisoComprobanteContrato.Asunto = Convert.ToString(Desde["EmailAvisoComprobanteAsunto"]);
            Hasta.DatosEmailAvisoComprobanteContrato.Cuerpo = Convert.ToString(Desde["EmailAvisoComprobanteCuerpo"]);
        }

        public List<Entidades.Comprobante> Lista(out int CantReg, int IndicePagina, List<Entidades.Estado> Estados, List<Entidades.Estado> EstadosCompras, List<FeaEntidades.TiposDeComprobantes.TipoComprobante> TiposComprobante, string OrderBy, string FechaDesde, string FechaHasta, Entidades.Persona Persona, Entidades.NaturalezaComprobante NaturalezaComprobante, string PuntoDeVenta, string NumeroDeComprobante, bool IncluirContratos, bool IncluirRequestResponse, string Detalle, bool Ajuste)
        {
            CantReg = 0;
            List<Entidades.Comprobante> lista = new List<Entidades.Comprobante>();
            if (sesion.Cuit.Nro != null)
            {
                System.Text.StringBuilder a = new StringBuilder();
                System.Text.StringBuilder aSelect = new StringBuilder();
                System.Text.StringBuilder aSelectPaging = new StringBuilder();
                aSelectPaging.Append("select * ");
                aSelectPaging.Append("from (select top {0} ROW_NUMBER() OVER (ORDER BY {1}) as ROW_NUM, ");
                if (IncluirRequestResponse)
                {
                    aSelectPaging.Append("Comprobante.Cuit, Comprobante.IdTipoComprobante, Comprobante.DescrTipoComprobante, Comprobante.NroPuntoVta, Comprobante.NroComprobante, Comprobante.NroLote, Comprobante.IdTipoDoc, Comprobante.NroDoc, Comprobante.IdPersona, Comprobante.DesambiguacionCuitPais, Comprobante.RazonSocial, Comprobante.Detalle, Comprobante.Fecha, Comprobante.FechaVto, Comprobante.Moneda, Comprobante.ImporteMoneda, Comprobante.TipoCambio, Comprobante.Importe, Comprobante.Request, Comprobante.Response, Comprobante.IdDestinoComprobante, Comprobante.IdWF, Comprobante.Estado, Comprobante.UltActualiz, Comprobante.IdNaturalezaComprobante, NaturalezaComprobante.DescrNaturalezaComprobante, Comprobante.PeriodicidadEmision, Comprobante.FechaProximaEmision, Comprobante.CantidadComprobantesAEmitir, Comprobante.CantidadComprobantesEmitidos, Comprobante.CantidadDiasFechaVto, Comprobante.EmailAvisoComprobanteActivo, Comprobante.IdDestinatarioFrecuente, Comprobante.EmailAvisoComprobanteAsunto, Comprobante.EmailAvisoComprobanteCuerpo ");
                }
                else
                {
                    aSelectPaging.Append("Comprobante.Cuit, Comprobante.IdTipoComprobante, Comprobante.DescrTipoComprobante, Comprobante.NroPuntoVta, Comprobante.NroComprobante, Comprobante.NroLote, Comprobante.IdTipoDoc, Comprobante.NroDoc, Comprobante.IdPersona, Comprobante.DesambiguacionCuitPais, Comprobante.RazonSocial, Comprobante.Detalle, Comprobante.Fecha, Comprobante.FechaVto, Comprobante.Moneda, Comprobante.ImporteMoneda, Comprobante.TipoCambio, Comprobante.Importe, '' as Request, '' as Response, Comprobante.IdDestinoComprobante, Comprobante.IdWF, Comprobante.Estado, Comprobante.UltActualiz, Comprobante.IdNaturalezaComprobante, NaturalezaComprobante.DescrNaturalezaComprobante, Comprobante.PeriodicidadEmision, Comprobante.FechaProximaEmision, Comprobante.CantidadComprobantesAEmitir, Comprobante.CantidadComprobantesEmitidos, Comprobante.CantidadDiasFechaVto, Comprobante.EmailAvisoComprobanteActivo, Comprobante.IdDestinatarioFrecuente, Comprobante.EmailAvisoComprobanteAsunto, Comprobante.EmailAvisoComprobanteCuerpo ");
                }
                aSelect.Append("select Count(*) ");
                a.Append("from Comprobante, NaturalezaComprobante ");
                a.Append("where Comprobante.Cuit='" + sesion.Cuit.Nro + "' ");
                a.Append("and Comprobante.IdNaturalezaComprobante=NaturalezaComprobante.IdNaturalezaComprobante ");
                string estados = String.Empty;
                for (int i = 0; i < Estados.Count; i++)
                {
                    estados += "'" + Estados[i].Id + "'";
                    if (i != (Estados.Count - 1)) estados += ", ";
                }
                string estadosCompras = String.Empty;
                for (int i = 0; i < EstadosCompras.Count; i++)
                {
                    estadosCompras += "'" + EstadosCompras[i].Id + "'";
                    if (i != (EstadosCompras.Count - 1)) estadosCompras += ", ";
                }
                if (estados != String.Empty && estadosCompras != String.Empty)
                {
                    a.Append("and ((Comprobante.Estado in (" + estados + ") and Comprobante.IdNaturalezaComprobante='Venta') or (Comprobante.Estado in (" + estadosCompras + ") and Comprobante.IdNaturalezaComprobante<>'Venta')) ");
                }
                else if (estados != String.Empty)
                {
                    a.Append("and (Comprobante.Estado in (" + estados + ") and Comprobante.IdNaturalezaComprobante='Venta') ");
                }
                else if (estadosCompras != String.Empty)
                {
                    a.Append("and (Comprobante.Estado in (" + estadosCompras + ") and Comprobante.IdNaturalezaComprobante<>'Venta') ");
                }
                string tiposComprobante = String.Empty;
                for (int i = 0; i < TiposComprobante.Count; i++)
                {
                    tiposComprobante += "'" + TiposComprobante[i].Codigo + "'";
                    if (i != (TiposComprobante.Count - 1)) tiposComprobante += ", ";
                }
                if (tiposComprobante != String.Empty)
                {
                    a.Append("and Comprobante.IdTipoComprobante in (" + tiposComprobante + ") ");
                }
                if (FechaDesde != String.Empty)
                {
                    a.Append("and Comprobante.Fecha>='" + FechaDesde + "' ");
                }
                if (FechaHasta != String.Empty)
                {
                    a.Append("and Comprobante.Fecha<='" + FechaHasta + "' ");
                }
                if (PuntoDeVenta != String.Empty)
                {
                    a.Append("and Comprobante.NroPuntoVta='" + PuntoDeVenta + "' ");
                }
                if (NumeroDeComprobante != String.Empty)
                {
                    a.Append("and Comprobante.NroComprobante='" + NumeroDeComprobante + "' ");
                }
                if (Persona.RazonSocial != String.Empty)
                {
                    a.Append("and Comprobante.RazonSocial like '%" + Persona.RazonSocial + "%' ");
                }
                if (Persona.Orden != 0)
                {
                    a.Append("and Comprobante.IdTipoDoc=" + Persona.Documento.Tipo.Id + " ");
                    a.Append("and Comprobante.NroDoc='" + Persona.Documento.Nro.ToString() + "' ");
                    //a.Append("and Comprobante.IdPersona='" + Persona.IdPersona + "' ");
                    //a.Append("and Comprobante.DesambiguacionCuitPais=" + Persona.DesambiguacionCuitPais.ToString() + " ");
                }
                if (NaturalezaComprobante.Id != String.Empty)
                {
                    a.Append("and Comprobante.IdNaturalezaComprobante='" + NaturalezaComprobante.Id + "' ");
                }
                else if (!IncluirContratos)
                {
                    a.Append("and Comprobante.IdNaturalezaComprobante<>'VentaContrato' ");
                }
                if (Detalle != string.Empty)
                {
                    a.Append("and Comprobante.Detalle like '%" + Detalle + "%' ");
                }
                if (!Ajuste)
                {
                    a.Append("and (Comprobante.IdNaturalezaComprobante<>'Compra' or (Comprobante.IdNaturalezaComprobante='Compra' and Comprobante.NroDoc <> '" + sesion.Cuit.Nro + "')) ");
                }
                aSelect.Append(a);
                string OrderByQuery = "";
                switch (OrderBy)
                {
                    case "RazSoc-FecEmi-PtoVta-TipoComp-NroComp":
                        OrderByQuery = "Comprobante.RazonSocial asc, Comprobante.Fecha desc, Comprobante.DescrTipoComprobante desc, Comprobante.NroPuntoVta desc, ABS(Comprobante.NroComprobante) desc ";
                        break;
                    case "FecEmi-RazSoc-PtoVta-TipoComp-NroComp":
                        OrderByQuery = "Comprobante.Fecha desc, Comprobante.RazonSocial asc, Comprobante.DescrTipoComprobante desc, Comprobante.NroPuntoVta desc, ABS(Comprobante.NroComprobante) desc ";
                        break;
                    case "FecEmi-PtoVta-TipoComp-NroComp":
                        OrderByQuery = "Comprobante.Fecha desc, Comprobante.DescrTipoComprobante desc, Comprobante.NroPuntoVta desc, ABS(Comprobante.NroComprobante) desc ";
                        break;
                    case "PtoVta-TipoComp-NroComp":
                        OrderByQuery = "Comprobante.NroPuntoVta desc, Comprobante.DescrTipoComprobante asc, ABS(Comprobante.NroComprobante) desc ";
                        break;
                    case "TipoComp-PtoVta-NroComp":
                    default:
                        OrderByQuery = "Comprobante.DescrTipoComprobante desc, Comprobante.NroPuntoVta desc, ABS(Comprobante.NroComprobante) desc ";
                        break;
                }
                aSelectPaging.Append(a);
                aSelectPaging.Append("ORDER BY ROW_NUM) innerSelect WHERE ROW_NUM > {2} ");
                string commandText = string.Format(aSelectPaging.ToString(), OrderByQuery);
                commandText = commandText + aSelect;
                DataSet ds = (DataSet)Ejecutar(commandText.ToString(), TipoRetorno.DS, Transaccion.NoAcepta, sesion.CnnStr);
                if (ds.Tables.Count != 0)
                {
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        List<Entidades.UN> listaUN = sesion.Cuit.UNs.FindAll(delegate (Entidades.UN un)
                        {
                            return un.Id == sesion.UN.Id;
                        });
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Entidades.Comprobante elem = new Entidades.Comprobante();
                            Copiar(ds.Tables[0].Rows[i], elem);
                            if (elem.NaturalezaComprobante.Id != "Compra")
                            {
                                List<Entidades.PuntoVta> listaPV = listaUN[0].PuntosVta.FindAll(delegate (Entidades.PuntoVta pv)
                                {
                                    return pv.Nro == elem.NroPuntoVta;
                                });
                                if (listaPV.Count > 0)
                                {
                                    lista.Add(elem);
                                }
                            }
                            else
                            {
                                lista.Add(elem);
                            }
                        }
                        if (ds.Tables[1].Rows.Count != 0)
                        {
                            CantReg = Convert.ToInt32(ds.Tables[1].Rows[0][0].ToString());
                        }
                    }
                }
            }
            return lista;
        }
    }
}