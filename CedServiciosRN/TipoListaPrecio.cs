using System;
using System.Collections.Generic;
using CedServicios;

namespace CedServicios.RN
{
    public class TipoListaPrecio
    {
        public static List<Entidades.TipoListaPrecio> Lista()
        {
            List<CedServicios.Entidades.TipoListaPrecio> listaTipoListaPrecio = new List<CedServicios.Entidades.TipoListaPrecio>();
            CedServicios.Entidades.TipoListaPrecio tipoListaPrecio = new CedServicios.Entidades.TipoListaPrecio();
            tipoListaPrecio.Id = "Compra"; tipoListaPrecio.Descr = "Compra"; listaTipoListaPrecio.Add(tipoListaPrecio);
            tipoListaPrecio = new CedServicios.Entidades.TipoListaPrecio();
            tipoListaPrecio.Id = "Venta"; tipoListaPrecio.Descr = "Venta"; listaTipoListaPrecio.Add(tipoListaPrecio);
            return listaTipoListaPrecio;
        }
    }
}
