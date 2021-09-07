using CedFacturaElectronica.Core.Entidades;
using CedFacturaElectronica.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CedFacturaElectronica.Core.Servicios
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        public UsuarioService(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public async Task<List<UsuarioAplicacion>> GetAllAsync()
        {
            return await _usuarioRepositorio.GetAllAsync();
        }

       

        public async Task<UsuarioAplicacion> GetByIdAsync(int id)
        {
            return await _usuarioRepositorio.GetByIdAsync(id);
        }

        public async Task UpdateAsync(UsuarioAplicacion usuario)
        {
             await _usuarioRepositorio.UpdateAsync(usuario);
        }

        public async Task CreateAsync(UsuarioAplicacion usuario)
        {
           
            await _usuarioRepositorio.CreateAsync(usuario);
        }

        public async Task DeleteAsync(int id)
        {
            await _usuarioRepositorio.DeleteAsync(id);
        }

        public async Task<UsuarioAplicacion> GetLoginByCredentials(string nombreUsuario, string clave)
        {
            return await _usuarioRepositorio.GetLoginByCredentials(nombreUsuario, clave);
        }

        public async Task RegisterUser(UsuarioAplicacion usuarioAplicacion)
        {
            await _usuarioRepositorio.CreateAsync(usuarioAplicacion);
           
        }

    }
}
