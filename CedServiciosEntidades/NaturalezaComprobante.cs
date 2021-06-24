using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CedServicios.Entidades
{
    [Serializable]
    public class NaturalezaComprobante
    {
        private string id;
        private string descr;

        public NaturalezaComprobante()
        {
        }
        public NaturalezaComprobante(string Id, string Descr)
        {
            id = Id;
            descr = Descr;
        }
        public string Id
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
        public static List<NaturalezaComprobante> Lista()
        {
            List<NaturalezaComprobante> lista = new List<NaturalezaComprobante>();
            lista.Add(new NaturalezaComprobante("Venta", "Venta"));
            lista.Add(new NaturalezaComprobante("Compra", "Compra"));
            lista.Add(new NaturalezaComprobante("VentaTradic", "Venta Tradicional"));
            return lista;
        }
    }
}