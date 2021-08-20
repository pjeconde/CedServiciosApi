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
    public class PersonaRepositorio : IPersonaRepositorio
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PersonaRepositorio(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<Persona> CreateAsync(Persona persona)
        {
            await _applicationDbContext.Personas.AddAsync(persona);
            await _applicationDbContext.SaveChangesAsync();

            return persona;

        }

        public async Task DeleteAsync(int id)
        {
            var resultado = _applicationDbContext.Personas.FirstOrDefault(x => x.Id == id);

            _applicationDbContext.Personas.Remove(resultado);
            await _applicationDbContext.SaveChangesAsync();

        }

        public async Task<List<Persona>> GetAllAsync()
        {
            return await _applicationDbContext.Personas.ToListAsync();

        }

        public async Task<Persona> GetByIdAsync(int id)
        {
            return await _applicationDbContext.Personas

             .FirstOrDefaultAsync(persona => persona.Id == id);

        }

        public async Task UpdateAsync(Persona persona)
        {
            _applicationDbContext.Entry(persona).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _applicationDbContext.SaveChangesAsync();

        }

    }
}
