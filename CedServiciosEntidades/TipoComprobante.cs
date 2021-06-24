using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CedServicios.Entidades
{
    [Serializable]
    public class TipoComprobante
    {
        private int id;
        private string descr;

        public TipoComprobante()
        {
        }
        public TipoComprobante(int Id, string Descr)
        {
            id = Id;
            descr = Descr;    
        }
        public int Id
        {
            set
            {
                id = value;
            }
            get
            {
                return id;
            }
        }
        public string Descr
        {
            set
            {
                descr = value;
            }
            get
            {
                return descr;
            }
        }
    }
}