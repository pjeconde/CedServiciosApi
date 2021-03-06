﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using CedServicios;

namespace CedServicios.DB
{
    public class Permiso : db
    {
        public Permiso(Entidades.Sesion Sesion) : base(Sesion)
        {
        }

        public void Leer(Entidades.Permiso Permiso)
        {
            StringBuilder a = new StringBuilder(string.Empty);
            a.AppendLine("select Permiso.IdUsuario, Permiso.Cuit, Permiso.IdUN, Permiso.IdTipoPermiso, Permiso.FechaFinVigencia, Permiso.IdUsuarioSolicitante, Permiso.AccionTipo, Permiso.AccionNro, Permiso.IdWF, Permiso.Estado, TipoPermiso.DescrTipoPermiso, isnull(UN.DescrUN, '') as DescrUN ");
            a.AppendLine("from Permiso ");
            a.AppendLine("join TipoPermiso on Permiso.IdTipoPermiso=TipoPermiso.IdTipoPermiso ");
            a.AppendLine("left outer join UN on Permiso.IdUN=UN.IdUN  and Permiso.Cuit=UN.Cuit ");
            a.AppendLine("where Permiso.IdUsuario='" + Permiso.Usuario.Id + "' ");
            a.AppendLine("and Permiso.Cuit='" + Permiso.Cuit + "' ");
            a.AppendLine("and Permiso.IdUN=" + Permiso.UN.Id.ToString() + " ");
            a.AppendLine("and Permiso.IdTipoPermiso='" + Permiso.TipoPermiso.Id + "' ");
            a.AppendLine("and Permiso.Estado='PteAutoriz' ");
            DataTable dt = (DataTable)Ejecutar(a.ToString(), TipoRetorno.TB, Transaccion.NoAcepta, sesion.CnnStr);
            if (dt.Rows.Count != 0)
            {
                    Copiar_Leer(dt.Rows[0], Permiso);
            }
            else
            {
                throw new EX.Validaciones.ElementoInexistente("Permiso");
            }
        }
        private void Copiar_Leer(DataRow Desde, Entidades.Permiso Hasta)
        {
            Hasta.UN.Descr = Convert.ToString(Desde["DescrUN"]);
            Hasta.TipoPermiso.Descr = Convert.ToString(Desde["DescrTipoPermiso"]);
            Hasta.FechaFinVigencia = Convert.ToDateTime(Desde["FechaFinVigencia"]);
            Hasta.UsuarioSolicitante.Id = Convert.ToString(Desde["IdUsuarioSolicitante"]);
            Hasta.Accion.Tipo = Convert.ToString(Desde["AccionTipo"]);
            Hasta.Accion.Nro = Convert.ToInt32(Desde["AccionNro"]);
            Hasta.WF.Id = Convert.ToInt32(Desde["IdWF"]);
            Hasta.WF.Estado = Convert.ToString(Desde["Estado"]);
        }
        public List<Entidades.Permiso> LeerListaPermisosPteAutoriz(Entidades.Usuario Usuario)
        {
            StringBuilder a = new StringBuilder(string.Empty);
            a.AppendLine("/* AUTORIZACIONES PARA ADMINCUITS */ ");
            a.AppendLine("select Permiso.IdUsuario, Permiso.Cuit, Permiso.IdUN, Permiso.IdTipoPermiso, Permiso.FechaFinVigencia, Permiso.IdUsuarioSolicitante, Permiso.AccionTipo, Permiso.AccionNro, Permiso.IdWF, Permiso.Estado, TipoPermiso.DescrTipoPermiso into #p from Permiso, TipoPermiso where Estado='PteAutoriz' and Permiso.IdTipoPermiso in ('UsoCUITxUN', 'AdminCUIT') and Permiso.IdTipoPermiso=TipoPermiso.IdTipoPermiso and Cuit in ");
            a.AppendLine("(select Cuit from Permiso where IdUsuario='" + Usuario.Id + "' and Permiso.IdTipoPermiso='AdminCUIT' and Estado='Vigente') ");
            a.AppendLine("/* AUTORIZACIONES PARA ADMINUNS */ ");
            a.AppendLine("insert #p select Permiso.IdUsuario, Permiso.Cuit, Permiso.IdUN, Permiso.IdTipoPermiso, Permiso.FechaFinVigencia, Permiso.IdUsuarioSolicitante, Permiso.AccionTipo, Permiso.AccionNro, Permiso.IdWF, Permiso.Estado, TipoPermiso.DescrTipoPermiso from Permiso, TipoPermiso where Estado='PteAutoriz' and Permiso.IdTipoPermiso not in ('UsoCUITxUN', 'AdminCUIT', 'AdminSITE') and Permiso.IdTipoPermiso=TipoPermiso.IdTipoPermiso and Cuit + '-' + convert(varchar(10), IdUN) in ");
            a.AppendLine("(select Cuit + '-' + convert(varchar(10), IdUN) from Permiso where IdUsuario='" + Usuario.Id + "' and Permiso.IdTipoPermiso='AdminUN' and Estado='Vigente') and ");
            a.AppendLine("IdUsuario <> '' ");
            a.AppendLine("/* AUTORIZACIONES PARA ADMINSITES */ ");
            a.AppendLine("insert #p select Permiso.IdUsuario, Permiso.Cuit, Permiso.IdUN, Permiso.IdTipoPermiso, Permiso.FechaFinVigencia, Permiso.IdUsuarioSolicitante, Permiso.AccionTipo, Permiso.AccionNro, Permiso.IdWF, Permiso.Estado, TipoPermiso.DescrTipoPermiso from Permiso, TipoPermiso where Estado='PteAutoriz' and Permiso.IdTipoPermiso in ('AdminSITE') and Permiso.IdTipoPermiso=TipoPermiso.IdTipoPermiso and ");
            a.AppendLine("IdUsuario <> '' and (select count(*) from Permiso where IdUsuario='" + Usuario.Id + "' and Permiso.IdTipoPermiso='AdminSITE' and Estado='Vigente')=1 ");
            a.AppendLine("/* RESULTADOS */ ");
            a.AppendLine("select distinct #p.IdUsuario, #p.Cuit, #p.IdUN, #p.IdTipoPermiso, #p.FechaFinVigencia, #p.IdUsuarioSolicitante, #p.AccionTipo, #p.AccionNro, #p.IdWF, #p.Estado, #p.DescrTipoPermiso, isnull(u.Nombre, '') as NombreUsuario, isnull(u.Email, '') as EmailUsuario, isnull(us.Nombre, '') as NombreUsuarioSolicitante , isnull(us.Email, '') as EmailUsuarioSolicitante, isnull(UN.DescrUN, '') as DescrUN ");
            a.AppendLine("from #p ");
            a.AppendLine("left outer join Usuario u on #p.IdUsuario=u.IdUsuario ");
            a.AppendLine("left outer join Usuario us on #p.IdUsuarioSolicitante=us.IdUsuario ");
            a.AppendLine("left outer join UN on #p.IdUN=UN.IdUN and #p.Cuit=UN.Cuit ");
            a.AppendLine("order by #p.DescrTipoPermiso, #p.Cuit, #p.IdUN, NombreUsuario ");
            a.AppendLine("drop table #p ");
            DataTable dt = (DataTable)Ejecutar(a.ToString(), TipoRetorno.TB, Transaccion.NoAcepta, sesion.CnnStr);
            List<Entidades.Permiso> lista = new List<Entidades.Permiso>();
            if (dt.Rows.Count != 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Entidades.Permiso permiso = new Entidades.Permiso();
                    Copiar_LeerListaPermisosPteAutoriz(dt.Rows[i], permiso);
                    lista.Add(permiso);
                }
            }
            return lista;
        }
        private void Copiar_LeerListaPermisosPteAutoriz(DataRow Desde, Entidades.Permiso Hasta)
        {
            Hasta.Usuario.Id = Convert.ToString(Desde["IdUsuario"]);
            Hasta.Usuario.Nombre = Convert.ToString(Desde["NombreUsuario"]);
            Hasta.Usuario.Email = Convert.ToString(Desde["EmailUsuario"]);
            Hasta.UsuarioSolicitante.Nombre = Convert.ToString(Desde["NombreUsuarioSolicitante"]);
            Hasta.UsuarioSolicitante.Email = Convert.ToString(Desde["EmailUsuarioSolicitante"]);
            Hasta.Cuit = Convert.ToString(Desde["Cuit"]);
            Hasta.UN.Id = Convert.ToInt32(Desde["IdUN"]);
            Hasta.UN.Descr = Convert.ToString(Desde["DescrUN"]);
            Hasta.TipoPermiso.Id = Convert.ToString(Desde["IdTipoPermiso"]);
            Hasta.TipoPermiso.Descr = Convert.ToString(Desde["DescrTipoPermiso"]);
            Hasta.FechaFinVigencia = Convert.ToDateTime(Desde["FechaFinVigencia"]);
            Hasta.UsuarioSolicitante.Id = Convert.ToString(Desde["IdUsuarioSolicitante"]);
            Hasta.Accion.Tipo = Convert.ToString(Desde["AccionTipo"]);
            Hasta.Accion.Nro = Convert.ToInt32(Desde["AccionNro"]);
            Hasta.WF.Id = Convert.ToInt32(Desde["IdWF"]);
            Hasta.WF.Estado = Convert.ToString(Desde["Estado"]);
        }
        public List<Entidades.Permiso> LeerListaPermisosPorUsuario(Entidades.Usuario Usuario)
        {
            List<Entidades.Permiso> lista = new List<Entidades.Permiso>();
            if (Usuario.Id != null)
            {
                StringBuilder a = new StringBuilder(string.Empty);
                a.AppendLine("select Permiso.IdUsuario, Permiso.Cuit, Permiso.IdUN, Permiso.IdTipoPermiso, Permiso.FechaFinVigencia, Permiso.IdUsuarioSolicitante, Permiso.AccionTipo, Permiso.AccionNro, Permiso.IdWF, Permiso.Estado, TipoPermiso.DescrTipoPermiso, isnull(UN.DescrUN, '') as DescrUN ");
                a.AppendLine("from Permiso ");
                a.AppendLine("join TipoPermiso on Permiso.IdTipoPermiso=TipoPermiso.IdTipoPermiso ");
                a.AppendLine("left outer join UN on Permiso.IdUN=UN.IdUN  and Permiso.Cuit=UN.Cuit ");
                a.AppendLine("where IdUsuario='" + Usuario.Id + "' ");
                DataTable dt = (DataTable)Ejecutar(a.ToString(), TipoRetorno.TB, Transaccion.NoAcepta, sesion.CnnStr);
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Entidades.Permiso permiso = new Entidades.Permiso();
                        Copiar_LeerListaPermisosPorUsuario(dt.Rows[i], permiso);
                        lista.Add(permiso);
                    }
                }
            }
            return lista;
        }
        private void Copiar_LeerListaPermisosPorUsuario(DataRow Desde, Entidades.Permiso Hasta)
        {
            Hasta.Usuario.Id = Convert.ToString(Desde["IdUsuario"]);
            Hasta.Cuit = Convert.ToString(Desde["Cuit"]);
            Hasta.UN.Id = Convert.ToInt32(Desde["IdUN"]);
            Hasta.UN.Descr = Convert.ToString(Desde["DescrUN"]);
            Hasta.TipoPermiso.Id = Convert.ToString(Desde["IdTipoPermiso"]);
            Hasta.TipoPermiso.Descr = Convert.ToString(Desde["DescrTipoPermiso"]);
            Hasta.FechaFinVigencia = Convert.ToDateTime(Desde["FechaFinVigencia"]);
            Hasta.UsuarioSolicitante.Id = Convert.ToString(Desde["IdUsuarioSolicitante"]);
            Hasta.Accion.Tipo = Convert.ToString(Desde["AccionTipo"]);
            Hasta.Accion.Nro = Convert.ToInt32(Desde["AccionNro"]);
            Hasta.WF.Id = Convert.ToInt32(Desde["IdWF"]);
            Hasta.WF.Estado = Convert.ToString(Desde["Estado"]);
        }
        public List<Entidades.Permiso> LeerListaPermisosFiltrados(string IdUsuario, string CUIT, string IdTipoPermiso, string Estado, string VerPermisosDe)
        {
            StringBuilder a = new StringBuilder(string.Empty);
            a.AppendLine("select Permiso.IdUsuario, Permiso.Cuit, Permiso.IdUN, Permiso.IdTipoPermiso, TipoPermiso.DescrTipoPermiso, Permiso.FechaFinVigencia, Permiso.IdUsuarioSolicitante, Permiso.AccionTipo, Permiso.AccionNro, Permiso.IdWF, Permiso.Estado ");
            a.AppendLine("from Permiso ");
            a.Append("left outer join TipoPermiso on Permiso.IdTipoPermiso=TipoPermiso.IdTipoPermiso ");
            a.Append("where 1=1 ");
            if (IdUsuario != String.Empty) a.AppendLine("and IdUsuario like '%" + IdUsuario + "%' ");
            if (CUIT != String.Empty) a.AppendLine("and CUIT like '%" + CUIT + "%' ");
            if (IdTipoPermiso != String.Empty) a.AppendLine("and IdTipoPermiso='" + IdTipoPermiso + "' ");
            if (Estado != String.Empty) a.AppendLine("and Estado='" + Estado + "' ");
            switch (VerPermisosDe)
            {
                case "Cuits":
                    a.AppendLine("and Cuit<>'' and IdUN=0 and IdUsuario='' ");
                    break;
                case "UNs":
                    a.AppendLine("and Cuit<>'' and IdUN<>0 and IdUsuario='' ");
                    break;
                case "Usuarios":
                    a.AppendLine("and IdUsuario<>'' ");
                    break;
                case "Todos":
                    break;
            }
            DataTable dt = (DataTable)Ejecutar(a.ToString(), TipoRetorno.TB, Transaccion.NoAcepta, sesion.CnnStr);
            List<Entidades.Permiso> lista = new List<Entidades.Permiso>();
            if (dt.Rows.Count != 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Entidades.Permiso permiso = new Entidades.Permiso();
                    Copiar_LeerListaPermisosFiltrados(dt.Rows[i], permiso);
                    lista.Add(permiso);
                }
            }
            return lista;
        }
        private void Copiar_LeerListaPermisosFiltrados(DataRow Desde, Entidades.Permiso Hasta)
        {
            Hasta.Usuario.Id = Convert.ToString(Desde["IdUsuario"]);
            Hasta.Cuit = Convert.ToString(Desde["Cuit"]);
            Hasta.UN.Id = Convert.ToInt32(Desde["IdUN"]);
            Hasta.TipoPermiso.Id = Convert.ToString(Desde["IdTipoPermiso"]);
            Hasta.TipoPermiso.Descr = Convert.ToString(Desde["DescrTipoPermiso"]);
            Hasta.FechaFinVigencia = Convert.ToDateTime(Desde["FechaFinVigencia"]);
            Hasta.UsuarioSolicitante.Id = Convert.ToString(Desde["IdUsuarioSolicitante"]);
            Hasta.Accion.Tipo = Convert.ToString(Desde["AccionTipo"]);
            Hasta.Accion.Nro = Convert.ToInt32(Desde["AccionNro"]);
            Hasta.WF.Id = Convert.ToInt32(Desde["IdWF"]);
            Hasta.WF.Estado = Convert.ToString(Desde["Estado"]);
        }
        public Entidades.Permiso LeerPermisoPorCuit(string CUIT, string IdTipoPermiso)
        {
            StringBuilder a = new StringBuilder(string.Empty);
            a.AppendLine("select Permiso.IdUsuario, Permiso.Cuit, Permiso.IdUN, Permiso.IdTipoPermiso, Permiso.FechaFinVigencia, Permiso.IdUsuarioSolicitante, Permiso.AccionTipo, Permiso.AccionNro, Permiso.IdWF, Permiso.Estado ");
            a.AppendLine("from Permiso where IdUsuario='' and IdUN='' ");
            a.AppendLine("and CUIT='" + CUIT + "' ");
            a.AppendLine("and IdTipoPermiso='" + IdTipoPermiso + "' ");
            DataTable dt = (DataTable)Ejecutar(a.ToString(), TipoRetorno.TB, Transaccion.NoAcepta, sesion.CnnStr);
            Entidades.Permiso permiso = new Entidades.Permiso();
            Copiar_LeerPermisoPorCuit(dt.Rows[0], permiso);
            return permiso;
        }
        private void Copiar_LeerPermisoPorCuit(DataRow Desde, Entidades.Permiso Hasta)
        {
            Hasta.Usuario.Id = Convert.ToString(Desde["IdUsuario"]);
            Hasta.Cuit = Convert.ToString(Desde["Cuit"]);
            Hasta.UN.Id = Convert.ToInt32(Desde["IdUN"]);
            Hasta.TipoPermiso.Id = Convert.ToString(Desde["IdTipoPermiso"]);
            Hasta.FechaFinVigencia = Convert.ToDateTime(Desde["FechaFinVigencia"]);
            Hasta.UsuarioSolicitante.Id = Convert.ToString(Desde["IdUsuarioSolicitante"]);
            Hasta.Accion.Tipo = Convert.ToString(Desde["AccionTipo"]);
            Hasta.Accion.Nro = Convert.ToInt32(Desde["AccionNro"]);
            Hasta.WF.Id = Convert.ToInt32(Desde["IdWF"]);
            Hasta.WF.Estado = Convert.ToString(Desde["Estado"]);
        }
        public void Alta(Entidades.Permiso Permiso)
        {
            Ejecutar(AltaHandler(Permiso, true, true, false), TipoRetorno.None, Transaccion.Usa, sesion.CnnStr);
        }
        public string AltaHandler(Entidades.Permiso Permiso, bool GeneroAccion, bool DeclaroIdWF, bool EnAltaDeUN)
        {
            StringBuilder a = new StringBuilder(string.Empty);
            if (DeclaroIdWF)
            {
                a.AppendLine("declare @idWF varchar(256) ");
            }
            a.AppendLine("update Configuracion set @idWF=Valor=convert(varchar(256), convert(int, Valor)+1) where IdItemConfig='UltimoIdWF' ");
            if (GeneroAccion)
            {
                a.AppendLine("declare @accionTipo varchar(15) ");
                a.AppendLine("set @accionTipo='" + Permiso.Accion.Tipo + "' ");
                a.AppendLine("declare @accionNro varchar(256) ");
                a.AppendLine("update Configuracion set @accionNro=Valor=convert(varchar(256), convert(int, Valor)+1) where IdItemConfig='UltimoAccionNro' ");
            }
            string idUN = String.Empty;
            if (EnAltaDeUN)
            {
                idUN = "@IdUN";
            }
            else
            {
                idUN = "'" + Permiso.UN.Id + "'";
            }
            if (Permiso.Usuario.Id != null)
            {
                a.AppendLine("insert Permiso values ('" + Permiso.Usuario.Id + "', '" + Permiso.Cuit + "', " + idUN + ", '" + Permiso.TipoPermiso.Id + "', '" + Permiso.FechaFinVigencia.ToString("yyyyMMdd") + "', '" + Permiso.UsuarioSolicitante.Id + "', @accionTipo, @accionNro, @idWF, '" + Permiso.WF.Estado + "') ");
            }
            else
            {
                a.AppendLine("insert Permiso values ('', '" + Permiso.Cuit + "', " + idUN + ", '" + Permiso.TipoPermiso.Id + "', '" + Permiso.FechaFinVigencia.ToString("yyyyMMdd") + "', '" + Permiso.UsuarioSolicitante.Id + "', @accionTipo, @accionNro, @idWF, '" + Permiso.WF.Estado + "') ");
            }
            a.AppendLine("insert Log values (@IdWF, getdate(), '" + Permiso.UsuarioSolicitante.Id + "', 'Permiso', 'Alta', '" + Permiso.WF.Estado + "', '') ");
            return a.ToString();
        }
        public List<Entidades.Usuario> LeerListaUsuariosAutorizadores(string Cuit)
        {
            StringBuilder a = new StringBuilder(string.Empty);
            a.AppendLine("select IdUsuario from Permiso where Cuit='" + Cuit + "' and IdTipoPermiso='AdminCUIT' and Estado='Vigente' ");
            return LeerListaUsuarios(a.ToString());
        }
        public List<Entidades.Usuario> LeerListaUsuariosAutorizadores(string Cuit, int IdUN)
        {
            StringBuilder a = new StringBuilder(string.Empty);
            a.AppendLine("select IdUsuario from Permiso where Cuit='" + Cuit + "' and IdUN='" + IdUN.ToString() + "' and IdTipoPermiso='AdminUN' and Estado='Vigente' ");
            return LeerListaUsuarios(a.ToString());
        }
        public List<Entidades.Usuario> LeerListaUsuarios(string SqlScript)
        {
            DataTable dt = (DataTable)Ejecutar(SqlScript, TipoRetorno.TB, Transaccion.NoAcepta, sesion.CnnStr);
            List<Entidades.Usuario> lista = new List<Entidades.Usuario>();
            if (dt.Rows.Count != 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Entidades.Usuario elem = new Entidades.Usuario();
                    Usuario db = new Usuario(sesion);
                    elem = db.Leer(Convert.ToString(dt.Rows[i]["IdUsuario"]));
                    lista.Add(elem);
                }
            }
            return lista;
        }
        public bool CambioEstado(Entidades.Permiso Permiso, string Evento, string EstadoHst)
        {
            StringBuilder a = new StringBuilder(string.Empty);
            a.AppendLine("declare @IdWF int ");
            a.AppendLine("select @IdWF=IdWF from Permiso where Estado='" + Permiso.WF.Estado + "' and IdUsuario='" + Permiso.Usuario.Id + "' and Cuit='" + Permiso.Cuit + "' and IdUN='" + Permiso.UN.Id + "' and IdTipoPermiso='" + Permiso.TipoPermiso.Id + "' and Estado='" + Permiso.WF.Estado + "' ");
            a.AppendLine("if not @IdWF is null ");
            a.AppendLine("   begin ");
            a.AppendLine("      update Permiso set Estado='" + EstadoHst + "' where IdWF=@IdWF ");
            a.AppendLine("      insert Log values (@IdWF, getdate(), '" + sesion.Usuario.Id + "', 'Permiso', '" + Evento + "', '" + EstadoHst + "', '') ");
            a.AppendLine("      select @@rowcount as CantidadFilas");
            a.AppendLine("   end ");
            a.AppendLine("else ");
            a.AppendLine("   select convert(int, 0) as CantidadFilas ");
            DataTable dt = (DataTable)Ejecutar(a.ToString(), TipoRetorno.TB, Transaccion.Usa, sesion.CnnStr);
            Permiso.WF.Estado = EstadoHst;
            return Convert.ToInt32(dt.Rows[0]["CantidadFilas"]) == 1;
        }

        public List<Entidades.Permiso> ListaPaging(int IndicePagina, string OrderBy, string SessionID, List<Entidades.Permiso> PermisoLista)
        {
            System.Text.StringBuilder a = new StringBuilder();
            a.Append("CREATE TABLE #Permiso" + SessionID + "( ");
            a.Append("[IdUsuario] [varchar](50) NOT NULL, ");
            a.Append("[Cuit] [varchar] (11) NOT NULL, ");
            a.Append("[IdUN] [int] NOT NULL, ");
            a.Append("[IdTipoPermiso] [varchar] (15) NOT NULL, ");
            a.Append("[DescrTipoPermiso] [varchar] (50) NOT NULL, ");
            a.Append("[FechaFinVigencia] [datetime] NOT NULL, ");
            a.Append("[IdUsuarioSolicitante] [varchar] (50) NOT NULL, ");
            a.Append("[AccionTipo] [varchar] (15) NOT NULL, ");
            a.Append("[AccionNro] [int] NOT NULL, ");
            a.Append("[IdWF] [int] NOT NULL, ");
            a.Append("[Estado] [varchar](15) NOT NULL, ");
            a.Append("CONSTRAINT [PK_Permiso" + SessionID + "] PRIMARY KEY CLUSTERED ");
            a.Append("( ");
            a.Append("[IdUsuario] ASC, ");
            a.Append("[Cuit] ASC, ");
            a.Append("[IdUN] ASC, ");
            a.Append("[IdTipoPermiso] ASC ");
            a.Append(")WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY] ");
            a.Append(") ON [PRIMARY] ");
            foreach (Entidades.Permiso Permiso in PermisoLista)
            {
                a.Append("Insert #Permiso" + SessionID + " values ('" + Permiso.IdUsuario + "', '");
                a.Append(Permiso.Cuit + "', ");
                a.Append(Permiso.IdUN + ", '");
                a.Append(Permiso.IdTipoPermiso + "', '");
                a.Append(Permiso.DescrTipoPermiso + "', '");
                a.Append(Permiso.FechaFinVigencia.ToString("yyyyMMdd") + "', '");
                a.Append(Permiso.IdUsuarioSolicitante + "', '");
                a.Append(Permiso.Accion.Tipo + "', ");
                a.Append(Permiso.Accion.Nro + ", ");
                a.Append(Permiso.WF.Id + ", '");
                a.Append(Permiso.Estado + "') ");
            }
            a.Append("select * ");
            a.Append("from (select top {0} ROW_NUMBER() OVER (ORDER BY {1}) as ROW_NUM, ");
            a.Append("IdUsuario, Cuit, IdUN, IdTipoPermiso, DescrTipoPermiso, FechaFinVigencia, IdUsuarioSolicitante, AccionTipo, AccionNro, IdWF, Estado ");
            a.Append("from #Permiso" + SessionID + " ");
            a.Append("ORDER BY ROW_NUM) innerSelect WHERE ROW_NUM > {2} ");
            a.Append("DROP TABLE #Permiso" + SessionID);
            if (OrderBy.Trim().ToUpper() == "CUIT" || OrderBy.Trim().ToUpper() == "CUIT DESC" || OrderBy.Trim().ToUpper() == "CUIT ASC")
            {
                OrderBy = "#Permiso" + SessionID + "." + OrderBy;
            }
            if (OrderBy.Trim().ToUpper() == "IDUN" || OrderBy.Trim().ToUpper() == "IDUN DESC" || OrderBy.Trim().ToUpper() == "IDUN ASC")
            {
                OrderBy = "#Permiso" + SessionID + "." + OrderBy;
            }
            if (OrderBy.Trim().ToUpper() == "IDUSUARIO" || OrderBy.Trim().ToUpper() == "IDUSUARIO DESC" || OrderBy.Trim().ToUpper() == "IDUSUARIO ASC")
            {
                OrderBy = "#Permiso" + SessionID + "." + OrderBy;
            }
            if (OrderBy.Trim().ToUpper() == "IDUSUARIOSOLICITANTE" || OrderBy.Trim().ToUpper() == "IDUSUARIOSOLICITANTE DESC" || OrderBy.Trim().ToUpper() == "IDUSUARIOSOLICITANTE ASC")
            {
                OrderBy = "#Permiso" + SessionID + "." + OrderBy;
            }
            if (OrderBy.Trim().ToUpper() == "IDTIPOPERMISO" || OrderBy.Trim().ToUpper() == "IDTIPOPERMISO DESC" || OrderBy.Trim().ToUpper() == "IDTIPOPERMISO ASC")
            {
                OrderBy = "#Permiso" + SessionID + "." + OrderBy;
            }
            if (OrderBy.Trim().ToUpper() == "FECHAFINVIGENCIA" || OrderBy.Trim().ToUpper() == "FECHAFINVIGENCIA DESC" || OrderBy.Trim().ToUpper() == "FECHAFINVIGENCIA ASC")
            {
                OrderBy = "#Permiso" + SessionID + "." + OrderBy;
            }
            if (OrderBy.Trim().ToUpper() == "ESTADO" || OrderBy.Trim().ToUpper() == "ESTADO DESC" || OrderBy.Trim().ToUpper() == "ESTADO ASC")
            {
                OrderBy = "#Permiso" + SessionID + "." + OrderBy;
            }
            string commandText = string.Format(a.ToString(), OrderBy);
            DataTable dt = new DataTable();
            dt = (DataTable)Ejecutar(commandText.ToString(), TipoRetorno.TB, Transaccion.NoAcepta, sesion.CnnStr);
            List<Entidades.Permiso> lista = new List<Entidades.Permiso>();
            if (dt.Rows.Count != 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Entidades.Permiso permiso = new Entidades.Permiso();
                    permiso.Usuario.Id = dt.Rows[i]["IdUsuario"].ToString();
                    permiso.Cuit = dt.Rows[i]["Cuit"].ToString();
                    permiso.UN.Id = Convert.ToInt32(dt.Rows[i]["IdUN"].ToString());
                    permiso.TipoPermiso.Id = dt.Rows[i]["IdTipoPermiso"].ToString();
                    permiso.TipoPermiso.Descr = dt.Rows[i]["DescrTipoPermiso"].ToString();
                    permiso.FechaFinVigencia = Convert.ToDateTime(dt.Rows[i]["FechaFinVigencia"]);
                    permiso.UsuarioSolicitante.Id = dt.Rows[i]["IdUsuarioSolicitante"].ToString();
                    permiso.Accion.Tipo = dt.Rows[i]["AccionTipo"].ToString();
                    permiso.Accion.Nro = Convert.ToInt32(dt.Rows[i]["AccionNro"].ToString());
                    permiso.WF.Id = Convert.ToInt32(dt.Rows[i]["IdWF"].ToString());
                    permiso.WF.Estado = dt.Rows[i]["Estado"].ToString();
                    lista.Add(permiso);
                }
            }
            return lista;
        }
    }
}