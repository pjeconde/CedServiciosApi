using System;
using System.Collections.Generic;
using CedServicios;

namespace CedServicios.RN
{
    public class Permiso
    {
        public static void Leer(Entidades.Permiso Permiso, Entidades.Sesion Sesion)
        {
            CedServicios.DB.Permiso db = new DB.Permiso(Sesion);
            db.Leer(Permiso);
        }
        public static List<Entidades.Permiso> LeerListaPermisosPorUsuario(Entidades.Usuario Usuario, Entidades.Sesion Sesion)
        {
            CedServicios.DB.Permiso db = new DB.Permiso(Sesion);
            List<Entidades.Permiso> lista = db.LeerListaPermisosPorUsuario(Usuario);
            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].TipoPermiso.Id == "eFact")
                {
                    Entidades.Permiso permisoCuit = LeerPermisoPorCuit(lista[i].Cuit, lista[i].TipoPermiso.Id, Sesion);
                    lista[i].WF.Estado = permisoCuit.Estado;
                }
            }
            return lista;
        }
        public static List<Entidades.Permiso> LeerListaPermisosPteAutoriz(Entidades.Usuario Usuario, Entidades.Sesion Sesion)
        {
            CedServicios.DB.Permiso db = new DB.Permiso(Sesion);
            return db.LeerListaPermisosPteAutoriz(Usuario);
        }
        public static List<Entidades.Permiso> LeerListaPermisosFiltrados(string IdUsuario, string CUIT, string IdTipoPermiso, string Estado, Entidades.Sesion Sesion, string VerPermisosDe)
        {
            CedServicios.DB.Permiso db = new DB.Permiso(Sesion);
            return db.LeerListaPermisosFiltrados(IdUsuario, CUIT, IdTipoPermiso, Estado, VerPermisosDe);
        }
        public static Entidades.Permiso LeerPermisoPorCuit(string CUIT, string IdTipoPermiso, Entidades.Sesion Sesion)
        {
            CedServicios.DB.Permiso db = new DB.Permiso(Sesion);
            return db.LeerPermisoPorCuit(CUIT, IdTipoPermiso);
        }
    
     
   
        public static bool Autorizar(Entidades.Permiso Permiso, Entidades.Sesion Sesion)
        {
            DB.Permiso db = new DB.Permiso(Sesion);
            bool resultado = db.CambioEstado(Permiso, "Autoriz", "Vigente");
            //if (resultado) RN.EnvioCorreo.RespuestaAutorizacion(Permiso, Sesion.Usuario);
            return resultado;
        }
        public static bool Rechazar(Entidades.Permiso Permiso, Entidades.Sesion Sesion)
        {
            DB.Permiso db = new DB.Permiso(Sesion);
            bool resultado = db.CambioEstado(Permiso, "Rech", "Rechazado");
            //if (resultado) RN.EnvioCorreo.RespuestaAutorizacion(Permiso, Sesion.Usuario);
            return resultado;
        }
        public static bool CambiarEstado(Entidades.Permiso Permiso, string Evento, string IdEstado, Entidades.Sesion Sesion)
        {
            DB.Permiso db = new DB.Permiso(Sesion);
            return db.CambioEstado(Permiso, Evento, IdEstado);
        }
        public static string DescrPermiso(Entidades.Permiso Permiso)
        {
            string descripcion = String.Empty;
            switch (Permiso.TipoPermiso.Id)
            {
                case "AdminCUIT":
                    descripcion = "Administrador del CUIT " + Permiso.Cuit;
                    break;
                case "AdminUN":
                    descripcion = "Administrador de la Unidad de Negocio '" + Permiso.UN.Descr + "' del CUIT " + Permiso.Cuit;
                    break;
                case "UsoCUITXUN":
                    descripcion = "Relación entre la nueva Unidad de Negocio '" + Permiso.UN.Descr + "' y el CUIT " + Permiso.Cuit;
                    break;
                default:
                    descripcion = "Operador del servicio '" + Permiso.TipoPermiso.Descr.Replace("Operador servicio ", String.Empty) + "' de la Unidad de Negocio '" + Permiso.UN.Descr + "' del CUIT " + Permiso.Cuit;
                    break;
            }
            return descripcion;
        }
        public static void PermisoAdminSITEParaUsuarioAprobado(Entidades.Usuario Usuario, Entidades.Sesion Sesion)
        {
            Entidades.Permiso permiso = new Entidades.Permiso();
            permiso.Usuario = Sesion.Usuario;
            permiso.Cuit = String.Empty;
            permiso.UN.Id = 0;
            permiso.TipoPermiso.Id = "AdminSITE";
            permiso.FechaFinVigencia = new DateTime(2062, 12, 31);
            permiso.UsuarioSolicitante = Usuario;
            permiso.WF.Estado = "Vigente";
            CedServicios.DB.Permiso db = new DB.Permiso(Sesion);
            db.Alta(permiso);
        }
        public static string PermisoAdminCUITParaUsuarioAprobadoHandler(Entidades.Cuit Cuit, Entidades.Sesion Sesion)
        {
            Entidades.Permiso permiso = new Entidades.Permiso();
            permiso.Usuario = Sesion.Usuario;
            permiso.Cuit = Cuit.Nro;
            permiso.UN.Id = 0;
            permiso.TipoPermiso.Id = "AdminCUIT";
            permiso.FechaFinVigencia = new DateTime(2062, 12, 31);
            permiso.UsuarioSolicitante = Sesion.Usuario;
            permiso.WF.Estado = "Vigente";
            CedServicios.DB.Permiso db = new DB.Permiso(Sesion);
            return db.AltaHandler(permiso, false, false, false);
        }
        public static string PermisoUsoCUITxUNAprobadoHandler(Entidades.UN UN, Entidades.Sesion Sesion)
        {
            CedServicios.DB.Permiso db = new DB.Permiso(Sesion);
            Entidades.Permiso permiso = new Entidades.Permiso();
            permiso.Cuit = UN.Cuit;
            permiso.UN = UN;
            permiso.TipoPermiso.Id = "UsoCUITxUN";
            permiso.FechaFinVigencia = new DateTime(2062, 12, 31);
            permiso.UsuarioSolicitante = Sesion.Usuario;
            permiso.WF.Estado = "Vigente";
            return db.AltaHandler(permiso, false, false, true);
        }
        public static string PermisoOperServUNParaUsuarioAprobadoHandler(Entidades.UN UN, Entidades.TipoPermiso TipoPermiso, DateTime FechaFinVigencia, Entidades.Sesion Sesion)
        {
            Entidades.Permiso permiso = new Entidades.Permiso();
            permiso.Usuario = Sesion.Usuario;
            permiso.Cuit = UN.Cuit;
            permiso.UN = UN;
            permiso.TipoPermiso = TipoPermiso;
            permiso.FechaFinVigencia = FechaFinVigencia;
            permiso.UsuarioSolicitante = Sesion.Usuario;
            permiso.WF.Estado = "Vigente";
            CedServicios.DB.Permiso db = new DB.Permiso(Sesion);
            return db.AltaHandler(permiso, false, false, true);
        }
        public static string ServxCUITAprobadoHandler(Entidades.Cuit Cuit, Entidades.TipoPermiso TipoPermiso, DateTime FechaFinVigencia, Entidades.Sesion Sesion)
        {
            Entidades.Permiso permiso = new Entidades.Permiso();
            permiso.Usuario.Id = String.Empty;
            permiso.Cuit = Cuit.Nro;
            permiso.UN.Id = 0;
            permiso.TipoPermiso = TipoPermiso;
            permiso.FechaFinVigencia = FechaFinVigencia;
            permiso.UsuarioSolicitante = Sesion.Usuario;
            permiso.WF.Estado = "Vigente";
            CedServicios.DB.Permiso db = new DB.Permiso(Sesion);
            return db.AltaHandler(permiso, false, false, false);
        }

        public static List<Entidades.Permiso> ListaPaging(out int CantidadFilas, int IndicePagina, string OrderBy, string IdUsuario, string Cuit, string TipoPermiso, string Estado, string VerPermisosDe, string SessionID, Entidades.Sesion Sesion)
        {
            List<Entidades.Permiso> listaPermiso = new List<Entidades.Permiso>();
            DB.Permiso db = new DB.Permiso(Sesion);
            if (OrderBy.Equals(String.Empty))
            {
                OrderBy = "Cuit desc, IdUsuario asc ";
            }
            listaPermiso = db.LeerListaPermisosFiltrados(IdUsuario, Cuit, TipoPermiso, Estado, VerPermisosDe);
            int cantidadFilas = listaPermiso.Count;
            CantidadFilas = cantidadFilas;
            return db.ListaPaging(IndicePagina, OrderBy, SessionID, listaPermiso);
        }
    }
}