﻿using System;
using System.Collections.Generic;
using CedServicios;

namespace CedServicios.RN
{
    public class Medio
    {
        public static List<Entidades.Medio> Lista(Entidades.Sesion Sesion)
        {
            CedServicios.DB.Medio db = new DB.Medio(Sesion);
            return db.LeerLista();
        }
        //public List<Entidades.Medio> ConsultaListaMedio()
        //{
        //    DB.Medio dbMedio = new DB.Medio();
        //    List<Entidades.Medio> listaMedios = dbMedio.ConsultaListaMedio();
        //    return listaMedios;
        //}
        //public Entidades.Medio ConsultaMedio(string IdMedio, out string Resp)
        //{
        //    Entidades.Medio medio = new Entidades.Medio();
        //    DB.Medio dbMedio = new DB.Medio();
        //    medio = dbMedio.ConsultaMedio(IdMedio, out Resp);
        //    return medio;
        //}
        //public string InsertarMedio(CedServicios.Entidades.Medio Medio)
        //{
        //    DB.Medio dbMedio = new DB.Medio();
        //    string resp = dbMedio.InsertarMedio(Medio);
        //    return resp;
        //}
    }
}
