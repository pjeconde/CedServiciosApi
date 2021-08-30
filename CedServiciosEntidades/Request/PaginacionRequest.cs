using System;
using System.Collections.Generic;
using System.Text;

namespace CedServicios.Entidades.Request
{
    public class PaginacionRequest
    {
        private int pagina;
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
