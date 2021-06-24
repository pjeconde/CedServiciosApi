using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace CedServicios.DB
{
    public class Usuario : db
    {
        public Usuario(Entidades.Sesion Sesion) : base(Sesion)
        {
        }

        public Entidades.Usuario Leer(string IdUsuario)
        {
            Entidades.Usuario usuario = new Entidades.Usuario();
            StringBuilder a = new StringBuilder(string.Empty);
            a.Append("select Usuario.IdUsuario, Usuario.Nombre, Usuario.Telefono, Usuario.Email, Usuario.Password, Usuario.Pregunta, Usuario.Respuesta, Usuario.CantidadEnviosMail, Usuario.FechaUltimoReenvioMail, Usuario.EmailSMS, Usuario.IdWF, Usuario.Estado, Usuario.UltActualiz, isnull(ConfigCUITUNpredef.Cuit, '') as CuitPredef, isnull(ConfigCUITUNpredef.IdUN, 0) as IdUNPredef, isnull(ConfigFechaOKeFactTyC.Valor, '00000000') as FechaOKeFactTyC, ");
            a.Append("isnull(ConfigCantidadFilasXPagina.Valor, '0') as CantidadFilasXPagina, ");
            a.Append("isnull(ConfigMostrarAyudaComoPaginaDefault.Valor, 'SI') as MostrarAyudaComoPaginaDefault ");
            a.Append("from Usuario ");
            a.Append("left outer join Configuracion ConfigCUITUNpredef on Usuario.IdUsuario=ConfigCUITUNpredef.IdUsuario and ConfigCUITUNpredef.IdItemConfig='CUITUNpredef' ");
            a.Append("left outer join Configuracion ConfigFechaOKeFactTyC on Usuario.IdUsuario=ConfigFechaOKeFactTyC.IdUsuario and ConfigFechaOKeFactTyC.IdItemConfig='FechaOKeFactTyC' ");
            a.Append("left outer join Configuracion ConfigCantidadFilasXPagina on Usuario.IdUsuario=ConfigCantidadFilasXPagina.IdUsuario and ConfigCantidadFilasXPagina.IdItemConfig='CantidadFilasXPagina' ");
            a.Append("left outer join Configuracion ConfigMostrarAyudaComoPaginaDefault on Usuario.IdUsuario=ConfigMostrarAyudaComoPaginaDefault.IdUsuario and ConfigMostrarAyudaComoPaginaDefault.IdItemConfig='MostrarAyudaComoPaginaDefault' ");
            a.Append("where Usuario.IdUsuario='" + IdUsuario + "' ");
            DataTable dt = (DataTable)Ejecutar(a.ToString(), TipoRetorno.TB, Transaccion.NoAcepta, sesion.CnnStr);
            if (dt.Rows.Count == 0)
            {
                throw new CedServicios.EX.Validaciones.ElementoInexistente("Usuario " + IdUsuario);
            }
            else
            {
                Copiar_Leer(dt.Rows[0], usuario);
            }
            return usuario;
        }
        private void Copiar_Leer(DataRow Desde, Entidades.Usuario Hasta)
        {
            Hasta.Id = Convert.ToString(Desde["IdUsuario"]);
            Hasta.Nombre = Convert.ToString(Desde["Nombre"]);
            Hasta.Telefono = Convert.ToString(Desde["Telefono"]);
            Hasta.Email = Convert.ToString(Desde["Email"]);
            Hasta.Password = Convert.ToString(Desde["Password"]);
            Hasta.Pregunta = Convert.ToString(Desde["Pregunta"]);
            Hasta.Respuesta = Convert.ToString(Desde["Respuesta"]);
            Hasta.CantidadEnviosMail = Convert.ToInt32(Desde["CantidadEnviosMail"]);
            Hasta.FechaUltimoReenvioMail = Convert.ToDateTime(Desde["FechaUltimoReenvioMail"]);
            Hasta.EmailSMS = Convert.ToString(Desde["EmailSMS"]);
            Hasta.WF.Id = Convert.ToInt32(Desde["IdWF"]);
            Hasta.WF.Estado = Convert.ToString(Desde["Estado"]);
            Hasta.UltActualiz = ByteArray2TimeStamp((byte[])Desde["UltActualiz"]);
        }
        private void Copiar_ListaSegunFiltros(DataRow Desde, Entidades.Usuario Hasta)
        {
            Hasta.Id = Convert.ToString(Desde["IdUsuario"]);
            Hasta.Nombre = Convert.ToString(Desde["Nombre"]);
            Hasta.Telefono = Convert.ToString(Desde["Telefono"]);
            Hasta.Email = Convert.ToString(Desde["Email"]);
            Hasta.Password = Convert.ToString(Desde["Password"]);
            Hasta.Pregunta = Convert.ToString(Desde["Pregunta"]);
            Hasta.Respuesta = Convert.ToString(Desde["Respuesta"]);
            Hasta.CantidadEnviosMail = Convert.ToInt32(Desde["CantidadEnviosMail"]);
            Hasta.FechaUltimoReenvioMail = Convert.ToDateTime(Desde["FechaUltimoReenvioMail"]);
            Hasta.EmailSMS = Convert.ToString(Desde["EmailSMS"]);
            Hasta.WF.Id = Convert.ToInt32(Desde["IdWF"]);
            Hasta.WF.Estado = Convert.ToString(Desde["Estado"]);
            Hasta.UltActualiz = ByteArray2TimeStamp((byte[])Desde["UltActualiz"]);
        }
        private void Copiar_ListaPaging(DataRow Desde, Entidades.Usuario Hasta)
        {
            Hasta.Id = Convert.ToString(Desde["IdUsuario"]);
            Hasta.Nombre = Convert.ToString(Desde["Nombre"]);
            Hasta.Telefono = Convert.ToString(Desde["Telefono"]);
            Hasta.Email = Convert.ToString(Desde["Email"]);
            Hasta.Password = Convert.ToString(Desde["Password"]);
            Hasta.Pregunta = Convert.ToString(Desde["Pregunta"]);
            Hasta.Respuesta = Convert.ToString(Desde["Respuesta"]);
            Hasta.CantidadEnviosMail = Convert.ToInt32(Desde["CantidadEnviosMail"]);
            Hasta.FechaUltimoReenvioMail = Convert.ToDateTime(Desde["FechaUltimoReenvioMail"]);
            Hasta.EmailSMS = Convert.ToString(Desde["EmailSMS"]);
            Hasta.WF.Id = Convert.ToInt32(Desde["IdWF"]);
            Hasta.WF.Estado = Convert.ToString(Desde["Estado"]);
            Hasta.UltActualiz = Desde["UltActualiz"].ToString();
        }
        public void Crear(Entidades.UsuarioDatosBasicos Usuario)
        {
            StringBuilder a = new StringBuilder(string.Empty);
            a.AppendLine("declare @idWF varchar(256) ");
            a.AppendLine("update Configuracion set @idWF=Valor=convert(varchar(256), convert(int, Valor)+1) where IdItemConfig='UltimoIdWF' ");
            a.Append("Insert Usuario (IdUsuario, Nombre, Telefono, Email, Password, Pregunta, Respuesta, CantidadEnviosMail, FechaUltimoReenvioMail, EmailSMS, IdWF, Estado) values (");
            a.Append("'" + Usuario.Id + "', ");
            a.Append("'" + Usuario.Nombre + "', ");
            a.Append("'" + Usuario.Telefono + "', ");
            a.Append("'" + Usuario.Email + "', ");
            a.Append("'" + Usuario.Password + "', ");
            a.Append("'" + Usuario.Pregunta + "', ");
            a.Append("'" + Usuario.Respuesta + "', ");
            a.Append("1, ");            //CantidadEnviosMail
            a.Append("getdate(), ");    //FechaUltimoReenvioMail
            a.Append("'', ");           //EmailSMS
            a.Append("@idWF, ");        //IdWF
            a.Append("'PteConf' ");
            a.AppendLine(") ");
            a.Append("insert Log values (@idWF, getdate(), '" + Usuario.Id + "', 'Usuario', 'Alta', 'PteConf', '') ");
            Ejecutar(a.ToString(), TipoRetorno.None, Transaccion.NoAcepta, sesion.CnnStr);
        }
        public void Confirmar(Entidades.UsuarioDatosBasicos Usuario)
        {
            StringBuilder a = new StringBuilder(string.Empty);
            a.Append("declare @idWF varchar(256) ");
            a.Append("declare @cantFilas int ");
            a.Append("select @idWF = IdWF from Usuario where IdUsuario='" + Usuario.Id + "' ");
            a.Append("update Usuario set Estado='Vigente' where IdUsuario='" + Usuario.Id + "' and Estado='PteConf' ");
            a.Append("set @cantFilas = @@ROWCOUNT ");
            a.Append("if @cantFilas = 1 ");
            a.Append("    insert Log values (@idWF, getdate(), '" + Usuario.Id + "', 'Usuario', 'Confirm', 'Vigente', '') ");
            a.Append("select @cantFilas as CantFilas ");
            DataTable dt = (DataTable)Ejecutar(a.ToString(), TipoRetorno.TB, Transaccion.NoAcepta, sesion.CnnStr);
            if (Convert.ToInt32(dt.Rows[0]["CantFilas"]) != 1)
            {
                throw new EX.Usuario.ErrorDeConfirmacion();
            }
        }
        public bool IdUsuarioDisponible(string IdUsuario)
        {
            StringBuilder a = new StringBuilder(string.Empty);
            a.Append("IF EXISTS(select * from Usuario where IdUsuario='" + IdUsuario + "') ");
            a.Append("  BEGIN ");
            a.Append("	select convert(bit, 0) as Disponible ");
            a.Append("  END ");
            a.Append("ELSE ");
            a.Append("  BEGIN ");
            a.Append("	select convert(bit, 1) as Disponible ");
            a.Append("  END ");
            DataTable dt = (DataTable)Ejecutar(a.ToString(), TipoRetorno.TB, Transaccion.NoAcepta, sesion.CnnStr);
            return Convert.ToBoolean(dt.Rows[0]["Disponible"]);
        }
        public List<Entidades.Usuario> DestinatariosAvisoAltaUsuario()
        {
            StringBuilder a = new StringBuilder(string.Empty);
            a.Append("select Usuario.IdUsuario, Usuario.Nombre, Usuario.Telefono, Usuario.Email, Usuario.Password, Usuario.Pregunta, Usuario.Respuesta, Usuario.CantidadEnviosMail, Usuario.FechaUltimoReenvioMail, Usuario.EmailSMS, Usuario.IdWF, Usuario.Estado, Usuario.UltActualiz ");
            a.Append("from Usuario, Permiso ");
            a.Append("where Usuario.IdUsuario=Permiso.IdUsuario and Permiso.IdTipoPermiso='AdminSITE' and Usuario.EmailSMS<>'' ");
            DataTable dt = (DataTable)Ejecutar(a.ToString(), TipoRetorno.TB, Transaccion.NoAcepta, sesion.CnnStr);
            List<Entidades.Usuario> lista = new List<Entidades.Usuario>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Entidades.Usuario usuario = new Entidades.Usuario();
                Copiar_DestinatariosAvisoAltaUsuario(dt.Rows[i], usuario);
                lista.Add(usuario);
            }
            return lista;
        }
        private void Copiar_DestinatariosAvisoAltaUsuario(DataRow Desde, Entidades.Usuario Hasta)
        {
            Hasta.Id = Convert.ToString(Desde["IdUsuario"]);
            Hasta.Nombre = Convert.ToString(Desde["Nombre"]);
            Hasta.Telefono = Convert.ToString(Desde["Telefono"]);
            Hasta.Email = Convert.ToString(Desde["Email"]);
            Hasta.Password = Convert.ToString(Desde["Password"]);
            Hasta.Pregunta = Convert.ToString(Desde["Pregunta"]);
            Hasta.Respuesta = Convert.ToString(Desde["Respuesta"]);
            Hasta.CantidadEnviosMail = Convert.ToInt32(Desde["CantidadEnviosMail"]);
            Hasta.FechaUltimoReenvioMail = Convert.ToDateTime(Desde["FechaUltimoReenvioMail"]);
            Hasta.EmailSMS = Convert.ToString(Desde["EmailSMS"]);
            Hasta.WF.Id = Convert.ToInt32(Desde["IdWF"]);
            Hasta.WF.Estado = Convert.ToString(Desde["Estado"]);
            Hasta.UltActualiz = ByteArray2TimeStamp((byte[])Desde["UltActualiz"]);
        }
        public int CantidadDeFilas()
        {
            string commandText = "select count(*) from Usuario ";
            DataTable dt = new DataTable();
            dt = (DataTable)Ejecutar(commandText, TipoRetorno.TB, Transaccion.NoAcepta, sesion.CnnStr);
            return Convert.ToInt32(dt.Rows[0][0]);
        }
        public void CambiarPassword(string IdUsuario, string ClaveNueva)
        {
            StringBuilder a = new StringBuilder(string.Empty);
            a.Append("update Usuario set Password='" + ClaveNueva + "' where IdUsuario='" + IdUsuario + "' ");
            Ejecutar(a.ToString(), TipoRetorno.None, Transaccion.NoAcepta, sesion.CnnStr);
        }
        public List<Entidades.Usuario> Lista(string Email)
        {
            StringBuilder a = new StringBuilder(string.Empty);
            a.Append("select Usuario.IdUsuario, Usuario.Nombre, Usuario.Telefono, Usuario.Email, Usuario.Password, Usuario.Pregunta, Usuario.Respuesta, Usuario.CantidadEnviosMail, Usuario.FechaUltimoReenvioMail, Usuario.EmailSMS, Usuario.IdWF, Usuario.Estado, Usuario.UltActualiz ");
            a.Append("from Usuario ");
            a.Append("where Usuario.Email='" + Email + "' ");
            DataTable dt = (DataTable)Ejecutar(a.ToString(), TipoRetorno.TB, Transaccion.NoAcepta, sesion.CnnStr);
            if (dt.Rows.Count == 0)
            {
                throw new EX.Usuario.NoHayUsuariosAsociadasAEmail();
            }
            else
            {
                List<Entidades.Usuario> lista = new List<Entidades.Usuario>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Entidades.Usuario elem = new Entidades.Usuario();
                    Copiar_Lista(dt.Rows[i], elem);
                    lista.Add(elem);
                }
                return lista;
            }
        }
        private void Copiar_Lista(DataRow Desde, Entidades.Usuario Hasta)
        {
            Hasta.Id = Convert.ToString(Desde["IdUsuario"]);
            Hasta.Nombre = Convert.ToString(Desde["Nombre"]);
            Hasta.Telefono = Convert.ToString(Desde["Telefono"]);
            Hasta.Email = Convert.ToString(Desde["Email"]);
            Hasta.Password = Convert.ToString(Desde["Password"]);
            Hasta.Pregunta = Convert.ToString(Desde["Pregunta"]);
            Hasta.Respuesta = Convert.ToString(Desde["Respuesta"]);
            Hasta.CantidadEnviosMail = Convert.ToInt32(Desde["CantidadEnviosMail"]);
            Hasta.FechaUltimoReenvioMail = Convert.ToDateTime(Desde["FechaUltimoReenvioMail"]);
            Hasta.EmailSMS = Convert.ToString(Desde["EmailSMS"]);
            Hasta.WF.Id = Convert.ToInt32(Desde["IdWF"]);
            Hasta.WF.Estado = Convert.ToString(Desde["Estado"]);
            Hasta.UltActualiz = ByteArray2TimeStamp((byte[])Desde["UltActualiz"]);
        }
        public string ListaIdUsuariosParaSQLscript()
        {
            string a = String.Empty;
            DataTable dt = (DataTable)Ejecutar("select Usuario.IdUsuario from Usuario ", TipoRetorno.TB, Transaccion.NoAcepta, sesion.CnnStr);
            for (int i=0; i<dt.Rows.Count; i++)
            {
                a += "'" + dt.Rows[i]["IdUsuario"] + "'";
                if (i != dt.Rows.Count - 1) a += ", ";
            }
            return a;
        }
        public void EliminarFISICAMENTEelUsuarioySusCuitsAdministrados(Entidades.Usuario Usuario)
        {
            StringBuilder a = new StringBuilder(string.Empty);
            a.AppendLine("declare @IdUsuario varchar(15) ");
            a.AppendLine("declare @Cuit varchar(11) ");
            a.AppendLine("set @IdUsuario='" + Usuario.Id + "' ");
            a.AppendLine("select @Cuit=Cuit from Permiso where IdUsuario=@IdUsuario and IdTipoPermiso='AdminCUIT' ");
            a.AppendLine("select IdWF into #ElimLog from Usuario where IdUsuario=@IdUsuario ");
            a.AppendLine("insert #ElimLog select IdWF from Cuit where Cuit=@Cuit ");
            a.AppendLine("insert #ElimLog select IdWF from UN where Cuit=@Cuit ");
            a.AppendLine("insert #ElimLog select IdWF from PuntoVta where Cuit=@Cuit ");
            a.AppendLine("insert #ElimLog select IdWF from Cliente where Cuit=@Cuit ");
            a.AppendLine("insert #ElimLog select IdWF from Permiso where Cuit=@Cuit or IdUsuario=@IdUsuario ");
            a.AppendLine("delete LogDetalle where IdLog in (select IdLog from Log where IdWF in (select IdWF from #ElimLog)) ");
            a.AppendLine("delete Log where IdWF in (select IdWF from #ElimLog) ");
            a.AppendLine("delete Permiso where Cuit=@Cuit or IdUsuario=@IdUsuario ");
            a.AppendLine("delete UN where Cuit=@Cuit ");
            a.AppendLine("delete PuntoVta where Cuit=@Cuit ");
            a.AppendLine("delete Cliente where Cuit=@Cuit ");
            a.AppendLine("delete Cuit where Cuit=@Cuit ");
            a.AppendLine("delete Usuario where IdUsuario=@IdUsuario ");
            a.AppendLine("drop table #ElimLog ");
            Ejecutar(a.ToString(), TipoRetorno.None, Transaccion.NoAcepta, sesion.CnnStr);
        }
        public int CantidadDeFilasParaLista(string IdUsuario, string Nombre, string Email, string Estado)
        {
            StringBuilder a = new StringBuilder(string.Empty);
            a.AppendLine("select COUNT(Usuario.IdUsuario) ");
            a.AppendLine("from Usuario where 1=1 ");
            if (IdUsuario != String.Empty) a.AppendLine("and IdUsuario like '%" + IdUsuario + "%' ");
            if (Nombre != String.Empty) a.AppendLine("and Nombre like '%" + Nombre + "%' ");
            if (Email != String.Empty) a.AppendLine("and Email like '%" + Email + "%' ");
            if (Estado != String.Empty) a.AppendLine("and Estado = '" + Estado + "' ");

            DataTable dt = new DataTable();
            dt = (DataTable)Ejecutar(a.ToString(), TipoRetorno.TB, Transaccion.NoAcepta, sesion.CnnStr);
            return Convert.ToInt32(dt.Rows[0][0]);
        }
        public List<Entidades.Usuario> Lista(int Pagina, ref string OrderBy, string IdUsuario, string Nombre, string Email, string Estado)
        {
            System.Text.StringBuilder a = new StringBuilder();
            a.Append("SELECT * ");
            a.Append("FROM (select TOP {0} ROW_NUMBER() OVER (ORDER BY {1}) as ROW_NUM, ");
            a.Append("IdUsuario, Nombre, Telefono, Email, Password, Pregunta, Respuesta, CantidadEnviosMail, FechaUltimoReenvioMail, EmailSMS, ");
            a.Append("IdWF, Estado, UltActualiz ");
            a.Append("from Usuario Usuario where 1=1 ");
            if (IdUsuario != String.Empty) a.AppendLine("and IdUsuario like '%" + IdUsuario + "%' ");
            if (Nombre != String.Empty) a.AppendLine("and Nombre like '%" + Nombre + "%' ");
            if (Email != String.Empty) a.AppendLine("and Email like '%" + Email + "%' ");
            if (Estado != String.Empty) a.AppendLine("and Estado = '" + Estado + "' ");
            a.Append("ORDER BY ROW_NUM) innerSelect WHERE ROW_NUM > {2} ");
            string[] se = ModificarSortExpression(OrderBy);
            OrderBy = se[1];
            string commandText = string.Format(a.ToString(), ((Pagina) * sesion.Usuario.CantidadFilasXPagina), se[0], ((Pagina - 1) * sesion.Usuario.CantidadFilasXPagina));
            //string commandText = string.Format(sb.ToString(), ((PageIndex + 1) * PageSize), ModificarSortExpression(SortExpression), (PageIndex * PageSize));
            DataTable dt = new DataTable();
            dt = (DataTable)Ejecutar(commandText.ToString(), TipoRetorno.TB, Transaccion.NoAcepta, sesion.CnnStr);
            List<Entidades.Usuario> lista = new List<Entidades.Usuario>();
            if (dt.Rows.Count != 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Entidades.Usuario usuario = new Entidades.Usuario();
                    Copiar_ListaPaging(dt.Rows[i], usuario);
                    lista.Add(usuario);
                }
            }
            return lista;
        }
        
        private string[] ModificarSortExpression(string SortExpression)
        {
            string[] se = new string[2];
            SortExpression = SortExpression.Trim().ToUpper();
            se[1] = SortExpression;
            switch (SortExpression)
            {
                case "ID":
                case "ID DESC": 
                case "ID ASC":
                    se[0] = "Usuario." + SortExpression.Replace("ID", "IDUSUARIO");
                    break;
                case "NOMBRE":
                case "NOMBRE DESC":
                case "NOMBRE ASC":
                case "TELEFONO":
                case "TELEFONO DESC":
                case "TELEFONO ASC":
                case "EMAIL":
                case "EMAIL DESC":
                case "EMAIL ASC":
                case "ESTADO":
                case "ESTADO DESC":
                case "ESTADO ASC":
                    se[0] = "Usuario." + SortExpression;
                    break;
                default:
                    se[0] = "Usuario.IdUsuario";
                    se[1] = "ID";
                    break;
            }
            return se;
        }
        public void RegistrarReenvioMail(Entidades.Usuario Usuario)
        {
            StringBuilder a = new StringBuilder(string.Empty);
            a.Append("update Usuario set Usuario.CantidadEnviosMail=Usuario.CantidadEnviosMail+1, FechaUltimoReenvioMail=getdate() where Usuario.IdUsuario='" + Usuario.Id + "' ");
            Ejecutar(a.ToString(), TipoRetorno.None, Transaccion.NoAcepta, sesion.CnnStr);
        }
    }
}