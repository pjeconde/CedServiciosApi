using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CedServicios.Entidades
{
    [Serializable]
    public class UsuarioLista
    {
        private int pagina;
        private int cantidadFilas;
        private int cantidadFilasXPagina;
        private string orderBy;
        private List<Usuario> usuarios;

        public UsuarioLista()
        {
            usuarios = new List<Usuario>();
        }

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
        public int CantidadFilas
        {
            set
            {
                cantidadFilas = value;
            }
            get
            {
                return cantidadFilas;
            }
        }
        public int CantidadFilasXPagina
        {
            set
            {
                cantidadFilasXPagina = value;
            }
            get
            {
                return cantidadFilasXPagina;
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
        public List<Usuario> Usuarios
        {
            set
            {
                usuarios = value;
            }
            get
            {
                return usuarios;
            }
        }
    }
}
