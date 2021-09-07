using CedFacturaElectronica.Core.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CedFacturaElectronica.Core.Interfaces
{
    public interface IUsuarioService
    {
        Task<List<UsuarioAplicacion>> GetAllAsync();
        Task<UsuarioAplicacion> GetByIdAsync(int id);
        Task<UsuarioAplicacion> GetLoginByCredentials(string nombreUsuario, string clave);
        Task CreateAsync(UsuarioAplicacion usuario);
        Task UpdateAsync(UsuarioAplicacion usuario);
        Task DeleteAsync(int id);
    }
}