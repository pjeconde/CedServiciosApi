using CedServicios.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CedServicios.RN
{
    public class Usuario
    {
        public static void Leer(Entidades.Usuario Usuario, Entidades.Sesion Sesion)
        {
            CedServicios.DB.Usuario db = new  DB.Usuario(Sesion);
            db.Leer(Usuario);
        }
        public static Respuesta Login(Entidades.Usuario Usuario, Entidades.Sesion Sesion)
        {
            Respuesta respuesta = LoginValidator(Usuario);
            string passwordIngresada = Usuario.Password;
            if (respuesta.Resultado.Severidad == Resultado.SeveridadEnum.Ok) { 
                Leer(Usuario, Sesion);
                respuesta.Entidad = Usuario;
            }
            return respuesta;

                    //if (passwordIngresada != Usuario.Password)
                    //{
                    //    throw new CedServicios.EX.Usuario.LoginRechazadoXPasswordInvalida();
                    //}
                    ////Se impide el login a cuenta pendientes de confirmacion o dadas de baja
                    ////(las cuentas "Prem" suspendidas se comportan como cuentas "Free")
                    //if (Usuario.WF.Estado != "Vigente")
                    //{
                    //    throw new CedServicios.EX.Usuario.LoginRechazadoXEstadoCuenta();
                    //}
                
            
        }

        public static Respuesta LoginValidator(Entidades.Usuario usuario)
        {

            Respuesta respuesta = new Respuesta();
            Resultado resultado;


            resultado = ValidarString(usuario.Id, (nameof(usuario.Id)).ToLower());
            if (resultado.Severidad == Resultado.SeveridadEnum.Error)
            {
                respuesta.Detalle.Add(resultado);
            }

            resultado = ValidarString(usuario.Id, (nameof(usuario.Password)).ToLower());
            if (resultado.Severidad == Resultado.SeveridadEnum.Error)
            {
                respuesta.Detalle.Add(resultado);
            }
   
            if (respuesta.Detalle.Count > 0)
            {
                respuesta.Resultado.Severidad = Resultado.SeveridadEnum.Error;
            }
            return respuesta;
        }

        private static Resultado ValidarString(string dato, string nombreCampo)
        {
            Resultado resultado = new Resultado();
            try
            {
                if (String.IsNullOrWhiteSpace(dato))
                {
                    resultado.Severidad = Resultado.SeveridadEnum.Error;
                    resultado.Codigo = "01";
                    resultado.Descripcion = "Debe ingresar el " + nombreCampo + " del usuario.";

                }
            }
            catch (Exception ex)
            {
                resultado.Severidad = Resultado.SeveridadEnum.Error;
                resultado.Codigo = "01";
                resultado.Descripcion = ex.Message;
            }

            return resultado;

        }

  
        //public static void Registrar(Entidades.UsuarioCrear Usuario, bool EnviarCorreo, Entidades.Sesion Sesion)
        //{
        //    Usuario.WF.Estado = "PteConf";
        //    DB.Usuario usuario = new DB.Usuario(Sesion);
        //    usuario.Crear(Usuario);
        //    if (EnviarCorreo) RN.EnvioCorreo.ConfirmacionAltaUsuario(Usuario, Sesion);
        //}

        public static bool IdCuentaDisponible(Entidades.Usuario Usuario, Entidades.Sesion Sesion)
        {
            if (Usuario.Id == String.Empty)
            {
                throw new EX.Validaciones.ValorNoInfo("Id.Usuario");
            }
            else
            {
                try
                {
                    DB.Usuario usuario = new DB.Usuario(Sesion);
                    return usuario.IdUsuarioDisponible(Usuario);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public static int CantidadDeFilas(Entidades.Sesion Sesion)
        {
            DB.Usuario usuario = new DB.Usuario(Sesion);
            return usuario.CantidadDeFilas();
        }
        public static void CambiarPassword(Entidades.Usuario Usuario, string PasswordActual, string PasswordNueva, string ConfirmacionPasswordNueva, Entidades.Sesion Sesion)
        {
            if (PasswordActual == String.Empty)
            {
                throw new EX.Validaciones.ValorNoInfo("Contraseña actual");
            }
            else
            {
                if (PasswordNueva == String.Empty)
                {
                    throw new EX.Validaciones.ValorNoInfo("Contraseña nueva");
                }
                else
                {
                    if (ConfirmacionPasswordNueva == String.Empty)
                    {
                        throw new EX.Validaciones.ValorNoInfo("Confirmación de Contraseña nueva");
                    }
                    else
                    {
                        if (Usuario.Password != PasswordActual)
                        {
                            throw new EX.Usuario.PasswordNoMatch();
                        }
                        else
                        {
                            if (PasswordNueva != ConfirmacionPasswordNueva)
                            {
                                throw new EX.Usuario.PasswordYConfirmacionNoCoincidente();
                            }
                            else
                            {
                                if (Usuario.Password == PasswordNueva)
                                {
                                    throw new EX.Usuario.PasswordNuevaIgualAActual();
                                }
                                else
                                {
                                    DB.Usuario usuario = new DB.Usuario(Sesion);
                                    usuario.CambiarPassword(Usuario, PasswordNueva);
                                }
                            }
                        }
                    }
                }
            }
        }
        public static string ListaIdUsuariosParaSQLscript(Entidades.Sesion Sesion)
        {
            DB.Usuario usuario = new DB.Usuario(Sesion);
            return usuario.ListaIdUsuariosParaSQLscript();
        }
        public static void RegistrarAceptacioneFactTyC(Entidades.Sesion Sesion)
        {
            CedServicios.DB.Configuracion db = new DB.Configuracion(Sesion);
            db.CrearFechaOKeFactTyC(Sesion.Usuario);
        }
        public static void ReenviarMail(Entidades.Usuario Usuario, Entidades.Sesion Sesion)
        {
            DB.Usuario usuario = new DB.Usuario(Sesion);
            usuario.RegistrarReenvioMail(Usuario);
            //RN.EnvioCorreo.ConfirmacionAltaUsuario(Usuario, Sesion);
        }
        public static List<Entidades.Usuario> ListaSegunFiltros(string IdUsuario, string Nombre, string Email, string Estado, Entidades.Sesion Sesion)
        {
            DB.Usuario usuario = new DB.Usuario(Sesion);
            return usuario.ListaSegunFiltros(IdUsuario, Nombre, Email, Estado);
        }
        public static List<Entidades.Usuario> ListaPaging(out int CantidadFilas, int IndicePagina, string OrderBy, string IdUsuario, string Nombre, string Email, string Estado, string SessionID, Entidades.Sesion Sesion)
        {
            List<Entidades.Usuario> listaUsuario = new List<Entidades.Usuario>();
            DB.Usuario db = new DB.Usuario(Sesion);
            if (OrderBy.Equals(String.Empty))
            {
                OrderBy = "IdUsuario desc";
            }
            listaUsuario = db.ListaSegunFiltros(IdUsuario, Nombre, Email, Estado);
            int cantidadFilas = listaUsuario.Count;
            CantidadFilas = cantidadFilas;
            return db.ListaPaging(IndicePagina, OrderBy, SessionID, listaUsuario);
        }
    }
}