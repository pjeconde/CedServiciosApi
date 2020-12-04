using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CedServicios.RN
{
    public class Respuesta
    {
        public static void ExceptionToRespuesta(CedServicios.Entidades.Respuesta respuesta, Exception ex)
        {
            respuesta.Severidad = CedServicios.Entidades.RespuestaDetalle.SeveridadEnum.Error;
            CedServicios.Entidades.RespuestaDetalle respuestaDetalle = new CedServicios.Entidades.RespuestaDetalle();
            respuestaDetalle.Severidad = CedServicios.Entidades.RespuestaDetalle.SeveridadEnum.Error;
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
    }
}
