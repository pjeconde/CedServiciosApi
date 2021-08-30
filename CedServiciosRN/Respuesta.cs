using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CedServicios.RN
{
    public class Respuesta
    {
        public static CedServicios.Entidades.Respuesta ExceptionToRespuesta(Exception ex)
        {
            CedServicios.Entidades.Respuesta respuesta = new CedServicios.Entidades.Respuesta();
            respuesta.Resultado = new CedServicios.Entidades.Resultado(CedServicios.Entidades.Resultado.SeveridadEnum.Error, "", ex.Message.ToString());
            respuesta.Detalle.Add(respuesta.Resultado);
            return respuesta;
        }

        public static Entidades.Resultado ValidarRequeridoString(string dato, string nombreCampo)
        {
            Entidades.Resultado resultado = new Entidades.Resultado();
            if (String.IsNullOrWhiteSpace(dato))
            {
                resultado.Severidad = Entidades.Resultado.SeveridadEnum.Error;
                resultado.Descripcion = "Debe ingresar: " + nombreCampo;
            }
            return resultado;
        }

        public static Entidades.Resultado ValidarNumeric(string dato, string nombreCampo)
        {
            Entidades.Resultado resultado = new Entidades.Resultado();
            if (!RN.Funciones.IsValidNumeric(dato))
            { 
                resultado.Severidad = Entidades.Resultado.SeveridadEnum.Error;
                resultado.Descripcion = "Debe ingresar un dato numérico en: " + nombreCampo;
            }
            return resultado;
        }
    }
}
