using CedServicios.Entidades;
using System;
using System.Collections.Generic;

namespace CedServicios.RN
{
    public class Usuario
    {
        public static Entidades.Usuario Leer(string IdUsuario, Entidades.Sesion Sesion)
        {
            CedServicios.DB.Usuario db = new  DB.Usuario(Sesion);
            return db.Leer(IdUsuario);
        }
        //SIN TOKEN
        public static Entidades.Response.ValorBoolResponse Login(string IdUsuario, string Clave, Entidades.Sesion Sesion)
        {
            Entidades.Response.ValorBoolResponse valorBoolResponse = new Entidades.Response.ValorBoolResponse();
            Entidades.Usuario usuario = new Entidades.Usuario();
            try
            {
                valorBoolResponse.Respuesta = LoginValidator(IdUsuario, Clave);
                if (valorBoolResponse.Respuesta.Resultado.Severidad == Resultado.SeveridadEnum.Ok)
                {
                    usuario = Leer(IdUsuario, Sesion);
                    //Validar si coincide la clave.
                    if (Clave != usuario.Password)
                    {
                        throw new CedServicios.EX.Usuario.LoginRechazadoXPasswordInvalida();
                    }
                    //Impide el login a cuenta pendientes de confirmacion o dadas de baja.
                    if (usuario.WF.Estado != "Vigente")
                    {
                        throw new CedServicios.EX.Usuario.LoginRechazadoXEstadoCuenta();
                    }
                    valorBoolResponse.Valor = true;
                }
            }
            catch (Exception ex)
            {
                valorBoolResponse.Respuesta = Respuesta.ExceptionToRespuesta(ex);
            }
            return valorBoolResponse;
        }
        private static Entidades.Respuesta LoginValidator(string IdUsuario, string Clave)
        {
            Entidades.Respuesta respuesta = new Entidades.Respuesta();
            Entidades.Resultado resultado = Respuesta.ValidarRequeridoString(IdUsuario, "Id.Usuario");
            if (resultado.Severidad == Resultado.SeveridadEnum.Error) { respuesta.Detalle.Add(resultado); }
            resultado = Respuesta.ValidarRequeridoString(Clave, "Clave");
            if (resultado.Severidad == Resultado.SeveridadEnum.Error) { respuesta.Detalle.Add(resultado); }
            if (respuesta.Detalle.Count > 0)
            {
                respuesta.Resultado = respuesta.Detalle[0];
            }
            return respuesta;
        }

        private static Entidades.Respuesta ValidarRegistrar(Entidades.UsuarioDatosBasicos usuario)
        {
            Entidades.Respuesta respuesta = new Entidades.Respuesta();
            respuesta = LoginValidator(usuario.Id, usuario.Password);
            Entidades.Resultado resultado = Respuesta.ValidarRequeridoString(usuario.Id, (nameof(usuario.Id)).ToLower());
            if (resultado.Severidad == Resultado.SeveridadEnum.Error) { respuesta.Detalle.Add(resultado); }
            resultado = Respuesta.ValidarRequeridoString(usuario.Password, (nameof(usuario.Password)).ToLower());
            if (resultado.Severidad == Resultado.SeveridadEnum.Error) { respuesta.Detalle.Add(resultado); }
            resultado = Respuesta.ValidarRequeridoString(usuario.Nombre, (nameof(usuario.Nombre)).ToLower());
            if (resultado.Severidad == Resultado.SeveridadEnum.Error) { respuesta.Detalle.Add(resultado); }
            resultado = Respuesta.ValidarRequeridoString(usuario.Email, (nameof(usuario.Email)).ToLower());
            if (resultado.Severidad == Resultado.SeveridadEnum.Error) { respuesta.Detalle.Add(resultado); }
            resultado = Respuesta.ValidarRequeridoString(usuario.Pregunta, (nameof(usuario.Pregunta)).ToLower());
            if (resultado.Severidad == Resultado.SeveridadEnum.Error) { respuesta.Detalle.Add(resultado); }
            resultado = Respuesta.ValidarRequeridoString(usuario.Respuesta, (nameof(usuario.Respuesta)).ToLower());
            if (resultado.Severidad == Resultado.SeveridadEnum.Error) { respuesta.Detalle.Add(resultado); }
            if (respuesta.Detalle.Count > 0)
            {
                respuesta.Resultado = respuesta.Detalle[0];
            }
            return respuesta;
        }
        public static Entidades.Response.ValorBoolResponse Registrar(Entidades.UsuarioDatosBasicos Usuario, Entidades.Sesion Sesion)
        {
            Entidades.Response.ValorBoolResponse valorBoolResponse = new Entidades.Response.ValorBoolResponse();
            try
            {
                valorBoolResponse.Respuesta = ValidarRegistrar(Usuario);
                if (valorBoolResponse.Respuesta.Resultado.Severidad == Resultado.SeveridadEnum.Ok)
                {
                    bool EnviarCorreo = true;
                    DB.Usuario db= new DB.Usuario(Sesion);
                    db.Crear(Usuario);
                    if (EnviarCorreo) RN.EnvioCorreo.ConfirmacionAltaUsuario(Usuario, Sesion);
                }
            }
            catch (Exception ex)
            {
                valorBoolResponse.Respuesta = Respuesta.ExceptionToRespuesta(ex);
            }
            return valorBoolResponse;
        }
        //SIN TOKEN
        public static bool IdCuentaDisponible(string IdUsuario, Entidades.Sesion Sesion)
        {
            if (IdUsuario == String.Empty)
            {
                throw new EX.Validaciones.ValorNoInfo("Id.Usuario");
            }
            DB.Usuario usuario = new DB.Usuario(Sesion);
            return usuario.IdUsuarioDisponible(IdUsuario);
        }
        public static int CantidadDeFilas(Entidades.Sesion Sesion)
        {
            DB.Usuario usuario = new DB.Usuario(Sesion);
            return usuario.CantidadDeFilas();
        }
        public static bool CambiarClave(string IdUsuario, string ClaveActual, string ClaveNueva, string ConfirmacionClaveNueva, Entidades.Sesion Sesion)
        {
            if (ClaveActual == String.Empty)
            {
                throw new EX.Validaciones.ValorNoInfo("Contraseña actual");
            }
            else if (ClaveNueva == String.Empty)
            {
                throw new EX.Validaciones.ValorNoInfo("Contraseña nueva");
            }
            else if (ConfirmacionClaveNueva == String.Empty)
            {
                throw new EX.Validaciones.ValorNoInfo("Confirmación de Contraseña nueva");
            }
            else if (ClaveNueva != ConfirmacionClaveNueva)
            {
                throw new EX.Usuario.PasswordYConfirmacionNoCoincidente();
            }
            else
            {
                DB.Usuario db = new DB.Usuario(Sesion);
                Entidades.Usuario usuario = RN.Usuario.Leer(IdUsuario, Sesion);
                if (usuario.Password != ClaveActual)
                {
                    throw new EX.Usuario.PasswordNoMatch();
                }
                else if (usuario.Password == ClaveNueva)
                {
                    throw new EX.Usuario.PasswordNuevaIgualAActual();
                }
                else
                {
                    db.CambiarPassword(IdUsuario, ClaveNueva);
                }
            }
            return true;
        }
        //SIN TOKEN
        public static bool CambiarClaveConPreguntaDeSeguridad(Entidades.Request.UsuarioCambiarClaveConPreguntaSegRequest UsuarioCambiarClaveConPreguntaSegRequest, Entidades.Sesion Sesion)
        {
            if (UsuarioCambiarClaveConPreguntaSegRequest.ClaveActual == String.Empty)
            {
                throw new EX.Validaciones.ValorNoInfo("Contraseña actual");
            }
            else if (UsuarioCambiarClaveConPreguntaSegRequest.ClaveNueva == String.Empty)
            {
                throw new EX.Validaciones.ValorNoInfo("Contraseña nueva");
            }
            else if (UsuarioCambiarClaveConPreguntaSegRequest.ClaveNuevaConfirmacion == String.Empty)
            {
                throw new EX.Validaciones.ValorNoInfo("Confirmación de Contraseña nueva");
            }
            else if (UsuarioCambiarClaveConPreguntaSegRequest.ClaveNueva != UsuarioCambiarClaveConPreguntaSegRequest.ClaveNuevaConfirmacion)
            {
                throw new EX.Usuario.PasswordYConfirmacionNoCoincidente();
            }
            else
            {
                DB.Usuario db = new DB.Usuario(Sesion);
                Entidades.Usuario usuario = RN.Usuario.Leer(UsuarioCambiarClaveConPreguntaSegRequest.IdUsuario, Sesion);
                if (usuario.Password != UsuarioCambiarClaveConPreguntaSegRequest.ClaveActual)
                {
                    throw new EX.Usuario.PasswordNoMatch();
                }
                else if (usuario.Password == UsuarioCambiarClaveConPreguntaSegRequest.ClaveNueva)
                {
                    throw new EX.Usuario.PasswordNuevaIgualAActual();
                }
                else
                {
                    db.CambiarPassword(UsuarioCambiarClaveConPreguntaSegRequest.IdUsuario, UsuarioCambiarClaveConPreguntaSegRequest.ClaveNueva);
                }
            }
            return true;
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
        public static Entidades.Response.UsuarioListaResponse Lista(int Pagina, string OrderBy, string IdUsuario, string Nombre, string Email, string Estado, Entidades.Sesion Sesion)
        {
            if (Pagina <= 0)
            {
                throw new CedServicios.EX.Validaciones.ValorNoInfo("Pagina");
            }

            Entidades.Response.UsuarioListaResponse usuarioListaResponse = new Entidades.Response.UsuarioListaResponse();
            DB.Usuario db = new DB.Usuario(Sesion);
            if (OrderBy.Equals(String.Empty))
            {
                OrderBy = "IdUsuario desc";
            }
            int cantidadDeFilasParaLista = db.CantidadDeFilasParaLista(IdUsuario, Nombre, Email, Estado);
            if (cantidadDeFilasParaLista <= 0)
            {
                throw new CedServicios.EX.Validaciones.InformacionNoEncontrada("Lista de Usuarios");
            }
            usuarioListaResponse.Paginacion.CantidadRegistros = cantidadDeFilasParaLista;
            usuarioListaResponse.Paginacion.CantidadRegistrosXPagina = Sesion.Usuario.CantidadFilasXPagina;
            usuarioListaResponse.Paginacion.Pagina = Pagina;
            usuarioListaResponse.Usuarios = db.Lista(Pagina, ref OrderBy, IdUsuario, Nombre, Email, Estado);
            usuarioListaResponse.Paginacion.OrderBy = OrderBy;
            return usuarioListaResponse;
        }
        //SIN TOKEN
        public static Entidades.Response.ValorStringResponse PreguntaSeguridad(string IdUsuario, string Email, Entidades.Sesion Sesion)
        {
            Entidades.Response.ValorStringResponse valorStringResponse = new Entidades.Response.ValorStringResponse();
            Entidades.Usuario usuario = new Entidades.Usuario();
            try
            {
                valorStringResponse.Respuesta = PreguntaSeguridadValidator(IdUsuario, Email);
                if (valorStringResponse.Respuesta.Resultado.Severidad == Resultado.SeveridadEnum.Ok)
                {
                    usuario = Leer(IdUsuario, Sesion);
                    //Validar email.
                    if (Email != usuario.Email)
                    {
                        throw new CedServicios.EX.Usuario.EmailInvalido();
                    }
                    //Impide el acceso a la pregunta de seguridad para cuentas en estado no vigente.
                    if (usuario.WF.Estado != "Vigente")
                    {
                        throw new CedServicios.EX.Usuario.CuentaEstadoNoVigente();
                    }
                    valorStringResponse.Valor = usuario.Pregunta;
                }
            }
            catch (Exception ex)
            {
                valorStringResponse.Respuesta = Respuesta.ExceptionToRespuesta(ex);
            }
            return valorStringResponse;
        }
        private static Entidades.Respuesta PreguntaSeguridadValidator(string IdUsuario, string Email)
        {
            Entidades.Respuesta respuesta = new Entidades.Respuesta();
            Entidades.Resultado resultado = Respuesta.ValidarRequeridoString(IdUsuario, "Id.Usuario");
            if (resultado.Severidad == Resultado.SeveridadEnum.Error) { respuesta.Detalle.Add(resultado); }
            resultado = Respuesta.ValidarRequeridoString(Email, "Email");
            if (resultado.Severidad == Resultado.SeveridadEnum.Error) { respuesta.Detalle.Add(resultado); }
            if (respuesta.Detalle.Count > 0)
            {
                respuesta.Resultado = respuesta.Detalle[0];
            }
            return respuesta;
        }
    }
}