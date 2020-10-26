using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CedServiciosApi.Models
{
    public class ACContext : DbContext
    {
        public ACContext(DbContextOptions<ACContext> options)
       : base(options)
        {

        }
        //public DbSet<Entidades.Usuario> Usuario { get; set; }
        //public DbSet<CedAC.Entidades.UsuarioAgenteColocador> UsuarioAgenteColocador { get; set; }
    }
}
