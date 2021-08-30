using System;
using System.Collections.Generic;
using System.Text;

namespace CedServicios.Entidades.Response
{
    [Serializable]
    public class PaginacionResponse
    {
        private int pagina;
        private int cantidadRegistros;
        private int cantidadRegistrosXPagina;
        private string orderBy;
        public int Pagina
        {
            set
            {
                pagina = value;
            }
            get
            {
                return pagina;
            }
        }
        public int CantidadPaginas
        {
            get
            {
                if (CantidadRegistros > 0)
                {
                    decimal cr = Convert.ToDecimal(CantidadRegistros) / Convert.ToDecimal(CantidadRegistrosXPagina);
                    int crint = Convert.ToInt32(Math.Ceiling(cr));
                    return crint;
                }
                else
                {
                    return 0;
                }
            }
        }
        public int CantidadRegistros
        {
            set
            {
                cantidadRegistros = value;
            }
            get
            {
                return cantidadRegistros;
            }
        }
        public int CantidadRegistrosXPagina
        {
            set
            {
                cantidadRegistrosXPagina = value;
            }
            get
            {
                return cantidadRegistrosXPagina;
            }
        }
        public string OrderBy
        {
            set
            {
                orderBy = value;
            }
            get
            {
                return orderBy;
            }
        }
    }
}
