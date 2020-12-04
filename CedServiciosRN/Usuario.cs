using CedServicios.Entidades;
using System;
using System.Collections.Generic;

namespace CedServicios.RN
{
    public class Usuario
    {
        public static void Leer(Entidades.Usuario Usuario, Entidades.Sesion Sesion)
        {
            CedServicios.DB.Usuario db = new  DB.Usuario(Sesion);
            db.Leer(Usuario);
        }
        public static Entidades.Respuesta Login(Entidades.Usuario Usuario, Entidades.Sesion Sesion)
        {
            Entidades.Respuesta respuesta = new Entidades.Respuesta();
            string passwordIngresada = Usuario.Password;
            try
            {
                respuesta = LoginValidator(Usuario);
                if (respuesta.Severidad == RespuestaDetalle.SeveridadEnum.Ok)
                {
                    Leer(Usuario, Sesion);
                    //Validar si coincide la clave.
                    if (passwordIngresada != Usuario.Password)
                    {
                        throw new CedServicios.EX.Usuario.LoginRechazadoXPasswordInvalida();
                    }
                    //Impide el login a cuenta pendientes de confirmacion o dadas de baja.
                    if (Usuario.WF.Estado != "Vigente")
                    {
                        throw new CedServicios.EX.Usuario.LoginRechazadoXEstadoCuenta();
                    }
                }
            }
            catch (Exception ex)
            {
                Respuesta.ExceptionToRespuesta(respuesta, ex);
                
                respuesta.Severidad = RespuestaDetalle.SeveridadEnum.Error;
                RespuestaDetalle respuestaDetalle = new RespuestaDetalle();
                respuestaDetalle.Severidad = RespuestaDetalle.SeveridadEnum.Error;
                if (ex.GetType().FullName.StartsWith("CedServicios.EX.Validaciones"))
                {
                    respuestaDetalle.Codigo = "02";
                }
                else if (ex.GetType().FullName.StartsWith("CedServicios.EX.Usuario"))
                {
                    respuestaDetalle.Codigo = "01";
                }
                else
                {
                    respuestaDetalle.Codigo = "99";
                }
                respuestaDetalle.Descripcion = ex.Message.ToString();
                respuesta.Detalle.Add(respuestaDetalle);
            }
            return respuesta;
        }
        private static Entidades.Respuesta LoginValidator(Entidades.Usuario usuario)
        {
            Entidades.Respuesta respuesta = new Entidades.Respuesta();
            Entidades.RespuestaDetalle respuestaDetalle;
            respuestaDetalle = ValidarString(usuario.Id, (nameof(usuario.Id)).ToLower());
            if (respuestaDetalle.Severidad == RespuestaDetalle.SeveridadEnum.Error)
            {
                respuesta.Detalle.Add(respuestaDetalle);
            }
            respuestaDetalle = ValidarString(usuario.Password, (nameof(usuario.Password)).ToLower());
            if (respuestaDetalle.Severidad == RespuestaDetalle.SeveridadEnum.Error)
            {
                respuesta.Detalle.Add(respuestaDetalle);
            }
            if (respuesta.Detalle.Count > 0)
            {
                respuesta.Severidad = RespuestaDetalle.SeveridadEnum.Error;
            }
            return respuesta;
        }
        private static Entidades.Respuesta RegistrarValidator(Entidades.Usuario usuario)
        {
            Entidades.Respuesta respuesta = new Entidades.Respuesta();
            Entidades.RespuestaDetalle respuestaDetalle;
            respuesta = LoginValidator(usuario);
            respuestaDetalle = ValidarString(usuario.Nombre, (nameof(usuario.Nombre)).ToLower());
            if (respuestaDetalle.Severidad == RespuestaDetalle.SeveridadEnum.Error)
            {
                respuesta.Detalle.Add(respuestaDetalle);
            }
            respuestaDetalle = ValidarString(usuario.Pregunta, (nameof(usuario.Pregunta)).ToLower());
            if (respuestaDetalle.Severidad == RespuestaDetalle.SeveridadEnum.Error)
            {
                respuesta.Detalle.Add(respuestaDetalle);
            }
            respuestaDetalle = ValidarString(usuario.Respuesta, (nameof(usuario.Respuesta)).ToLower());
            if (respuestaDetalle.Severidad == RespuestaDetalle.SeveridadEnum.Error)
            {
                respuesta.Detalle.Add(respuestaDetalle);
            }
            if (respuesta.Detalle.Count > 0)
            {
                respuesta.Severidad = RespuestaDetalle.SeveridadEnum.Error;
            }
            return respuesta;
        }
        private static Entidades.RespuestaDetalle ValidarString(string dato, string nombreCampo)
        {
            RespuestaDetalle respuesta = new RespuestaDetalle();
            try
            {
                if (String.IsNullOrWhiteSpace(dato))
                {
                    respuesta.Severidad = RespuestaDetalle.SeveridadEnum.Error;
                    respuesta.Codigo = "01";
                    respuesta.Descripcion = "Debe ingresar: " + nombreCampo + " del usuario.";
                }
            }
            catch (Exception ex)
            {
                respuesta.Severidad = RespuestaDetalle.SeveridadEnum.Error;
                respuesta.Codigo = "99";
                respuesta.Descripcion = "ValidarString: " + ex.Message;
            }
            return respuesta;
        }
        public static Entidades.Respuesta Registrar(Entidades.Usuario Usuario, Entidades.Sesion Sesion)
        {
            Entidades.Respuesta respuesta = new Entidades.Respuesta();
            try
            {
                respuesta = RegistrarValidator(Usuario);
                if (respuesta.Severidad == RespuestaDetalle.SeveridadEnum.Ok)
                {
                    bool EnviarCorreo = true;
                    Usuario.WF.Estado = "PteConf";
                    DB.Usuario usuario = new DB.Usuario(Sesion);
                    usuario.Crear(Usuario);
                    if (EnviarCorreo) RN.EnvioCorreo.ConfirmacionAltaUsuario(Usuario, Sesion);
                }
            }
            catch (Exception ex)
            {
                respuesta.Severidad = RespuestaDetalle.SeveridadEnum.Error;
                RespuestaDetalle respuestaDetalle = new RespuestaDetalle();
                respuestaDetalle.Severidad = RespuestaDetalle.SeveridadEnum.Error;
                if (ex.GetType().FullName.StartsWith("CedServicios.EX.Validaciones"))
                {
                    respuestaDetalle.Codigo = "02";
                }
                else if (ex.GetType().FullName.StartsWith("CedServicios.EX.Usuario"))
                {
                    respuestaDetalle.Codigo = "01";
                }
                else
                {
                    respuestaDetalle.Codigo = "99";
                }
                respuestaDetalle.Descripcion = ex.Message.ToString();
                respuesta.Detalle.Add(respuestaDetalle);
            }
            return respuesta;
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
        public static Entidades.UsuarioLista ListaPaging(int Pagina, string OrderBy, string IdUsuario, string Nombre, string Email, string Estado, string SessionID, Entidades.Sesion Sesion)
        {
            Entidades.UsuarioLista usuarioLista = new Entidades.UsuarioLista();
            List<Entidades.Usuario> listaUsuario = new List<Entidades.Usuario>();
            DB.Usuario db = new DB.Usuario(Sesion);
            if (OrderBy.Equals(String.Empty))
            {
                OrderBy = "IdUsuario desc";
            }
            listaUsuario = db.ListaSegunFiltros(IdUsuario, Nombre, Email, Estado);
            usuarioLista.CantidadFilas = listaUsuario.Count;
            usuarioLista.CantidadFilasXPagina = Sesion.Usuario.CantidadFilasXPagina;
            usuarioLista.OrderBy = OrderBy;
            usuarioLista.Pagina = Pagina;
            usuarioLista.Usuarios = db.ListaPaging(Pagina, OrderBy, SessionID, listaUsuario);
            return usuarioLista;
        }

    }
}