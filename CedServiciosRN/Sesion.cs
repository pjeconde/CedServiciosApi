using System;
using System.Collections.Generic;
using CedServicios;
using System.IO;

namespace CedServicios.RN
{
    public class Sesion
    {
        public static void Cerrar(Entidades.Sesion Sesion)
        {
            Sesion.Usuario = new Entidades.Usuario();
            Sesion.Cuit = new Entidades.Cuit();
            Sesion.UN = new Entidades.UN();
            Sesion.CuitsDelUsuario = new List<Entidades.Cuit>();
        }

        public static void Crear(string Ambiente, string CnnStr, CedServicios.Entidades.Sesion Sesion)
        {
            try
            {
                Sesion.Ambiente = Ambiente;
                Sesion.CnnStr = CnnStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

     
   
        
        public static void AsignarCuit(Entidades.Cuit Cuit, Entidades.Sesion Sesion)
        {
            Sesion.Cuit = Cuit;
            Sesion.Cuit.UNs = RN.UN.ListaPorCuitParaElUsuarioLogueado(Sesion);
            //Sesion.ClientesDelCuit = RN.Persona.ListaPorCuit(false, CedServicios.Entidades.Enum.TipoPersona.Cliente, Sesion);
            //Sesion.ProveedoresDelCuit = RN.Persona.ListaPorCuit(false, CedServicios.Entidades.Enum.TipoPersona.Proveedor, Sesion);
            List<Entidades.UN> estaLaUNEnLaLista = new List<Entidades.UN>();
            if (Sesion.Cuit.UNs.Count != 0)
            {
                if (Sesion.UN.Id == 0)
                {
                    //Todavía no eligió una UN ...

                }
                else
                {
                    //Ya eligió la UN
                    estaLaUNEnLaLista = Sesion.Cuit.UNs.FindAll(delegate (Entidades.UN p) { return p.Id == Sesion.UN.Id; });
                    if (estaLaUNEnLaLista.Count == 1)
                    {
                        AsignarUN(estaLaUNEnLaLista[0], Sesion);
                    }
                    else
                    {
                        AsignarUN(Sesion.Cuit.UNs[0], Sesion);
                    }
                }
            }
            else
            {
                BorrarUN(Sesion);
            }
        }
        public static void AsignarUN(Entidades.UN UN, Entidades.Sesion Sesion)
        {
            Sesion.UN = UN;
            Sesion.UN.PuntosVta = RN.PuntoVta.ListaPorUN(Sesion);
        }
        public static void BorrarCuit(Entidades.Sesion Sesion)
        {
            Sesion.Cuit = new Entidades.Cuit();
            //Sesion.ClientesDelCuit = new List<Entidades.Persona>();
            //Sesion.ProveedoresDelCuit = new List<Entidades.Persona>();
            BorrarUN(Sesion);
        }
        public static void BorrarUN(Entidades.Sesion Sesion)
        {
            Sesion.UN = new Entidades.UN();
        }
        public static void GrabarLogTexto(string archivo, string mensaje)
        {
            try
            {
                using (FileStream fs = File.Open(archivo, FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8))
                    {
                        sw.WriteLine(DateTime.Now.ToString("yyyyMMdd hh:mm:ss") + "  " + mensaje);
                    }
                }
            }
            catch
            {
            }
        }
    }
}