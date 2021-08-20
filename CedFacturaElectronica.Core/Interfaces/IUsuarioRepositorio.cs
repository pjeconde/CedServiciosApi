using CedFacturaElectronica.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CedFacturaElectronica.Core.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Task<List<UsuarioAplicacion>> GetAllAsync();
        Task<UsuarioAplicacion> GetByIdAsync(int id);
        Task<UsuarioAplicacion> GetByAccountAsync(string nombreUsuario, string clave);
        Task<UsuarioAplicacion> CreateAsync(UsuarioAplicacion usuario);
        Task UpdateAsync(UsuarioAplicacion usuario);
        Task DeleteAsync(int id);
    }
}
