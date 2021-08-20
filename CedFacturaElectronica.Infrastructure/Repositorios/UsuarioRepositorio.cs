using CedFacturaElectronica.Core.Entidades;
using CedFacturaElectronica.Core.Interfaces;
using CedFacturaElectronica.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CedFacturaElectronica.Infrastructure.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UsuarioRepositorio(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<UsuarioAplicacion> CreateAsync(UsuarioAplicacion usuario)
        {
            await _applicationDbContext.Usuarios.AddAsync(usuario);
            await _applicationDbContext.SaveChangesAsync();

            return usuario;

        }

        public async Task DeleteAsync(int id)
        {
            var resultado = _applicationDbContext.Usuarios.FirstOrDefault(x => x.Id == id.ToString());

            _applicationDbContext.Usuarios.Remove(resultado);
            await _applicationDbContext.SaveChangesAsync();

        }

        public async Task<List<UsuarioAplicacion>> GetAllAsync()
        {
            return await _applicationDbContext.Usuarios.ToListAsync();

        }

        public async Task<UsuarioAplicacion> GetByIdAsync(int id)
        {
            return await _applicationDbContext.Usuarios

             .FirstOrDefaultAsync(usuario => usuario.Id == id.ToString());

        }

        public async Task<UsuarioAplicacion> GetByAccountAsync(string nombreUsuario, string clave)
        {
            return await _applicationDbContext.Usuarios

             .FirstOrDefaultAsync(usuario => usuario.UserName == nombreUsuario && usuario.Clave == clave);

        }

        public async Task UpdateAsync(UsuarioAplicacion usuario)
        {
            _applicationDbContext.Entry(usuario).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _applicationDbContext.SaveChangesAsync();

        }
    }
}
