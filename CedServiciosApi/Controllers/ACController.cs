//using CedPACWebAPI.Models;
//using Com.Bgba.Arqtec.Contextdelivery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
//using System.DirectoryServices;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;

namespace CedServiciosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ACController : ControllerBase
    {
        //protected readonly ACContext _context;
        protected AppSettings AppSettings { get; set; }
        //protected IPathProvider pathProvider;
        protected IMemoryCache cache;

        public ACController(IOptions<AppSettings> settings, IMemoryCache cache)
        {
            //_context = context;
            AppSettings = settings.Value;
            //this.pathProvider = pathProvider;
            this.cache = cache;
        }

        protected CedServicios.Entidades.Sesion ObtenerSesion()
        {
            var cacheId = "sesion";
            return cache.GetOrCreate<CedServicios.Entidades.Sesion>(cacheId,
                cacheEntry =>
                {
                    CedServicios.Entidades.Sesion sesion = new CedServicios.Entidades.Sesion();
                    sesion.Ambiente = AppSettings.Ambiente;
                    sesion.CnnStr = AppSettings.CnnStr;
                    sesion.Usuario.Id = AppSettings.DBUsuario;
                    sesion.Usuario.Password = AppSettings.DBPassword;
                    //CedServicios.RN.Sesion.Crear(sesion);
                    return sesion;
                });
        }
    }
}
