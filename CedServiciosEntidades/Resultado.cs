using System;
using System.Collections.Generic;
using System.Text;

namespace CedServicios.Entidades
{
    /// <summary>
    /// SeveridadEnum --> Ok, Info, Aviso, Error, Warning
    /// </summary>
    /// <remarks>
    /// Sample value of message
    /// 
    ///     POST /Todo
    ///     {
    ///        "variable1": "Hi",
    ///        "variable2": "Sukhpinder"
    ///     }
    ///     
    /// </remarks>
    public class Resultado
    {

        public enum SeveridadEnum
        {
            Ok,
            Info,
            Aviso,
            Error
        };
        private SeveridadEnum severidad;
        private string codigo = String.Empty;
        private string descripcion = String.Empty;
        public Resultado()
        {
            severidad = SeveridadEnum.Ok;
        }
        public Resultado(SeveridadEnum Severidad, string Codigo, string Descripcion)
        {
            severidad = Severidad;
            codigo = Codigo;
            descripcion = Descripcion;
        }
        public SeveridadEnum Severidad
        {
            get
            {
                return severidad;
            }
            set
            {
                severidad = value;
            }
        }
        public string Codigo
        {
            get
            {
                return codigo;
            }
            set
            {
                codigo = value;
            }
        }
        public string Descripcion
        {
            get
            {
                return descripcion;
            }
            set
            {
                descripcion = value;
            }
        }
    }
}

