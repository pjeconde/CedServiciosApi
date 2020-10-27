//using CedPACWebAPI.Models;
//using Com.Bgba.Arqtec.Contextdelivery;
using CedServiciosApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        protected readonly ACContext _context;
        protected AppSettings AppSettings { get; set; }
        //protected IPathProvider pathProvider;
        protected IMemoryCache cache;

        public ACController(ACContext context, IOptions<AppSettings> settings, IMemoryCache cache)
        {
            _context = context;
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
                    sesion.CnnStr = _context.Database.GetDbConnection().ConnectionString;
                    sesion.Usuario.Id = AppSettings.DBUsuario;
                    sesion.Usuario.Password = AppSettings.DBPassword;
                    return sesion;
                });
        }
    }
}
