using CedFacturaElectronica.Core.Entidades;
using CedFacturaElectronica.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CedFacturaElectronica.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly IPersonaRepositorio _personaRepositorio;

        public PersonaController(IPersonaRepositorio personaRepositorio)
        {
            _personaRepositorio = personaRepositorio;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Persona>>> Get()
        {
            return await _personaRepositorio.GetAllAsync();
        }

        [HttpGet("{id}", Name = "ObtenerPersona")]
        public async Task<ActionResult<Persona>> Get(int id)
        {
            var resultado = await _personaRepositorio.GetByIdAsync(id);

            if (resultado == null)
            {
                return NotFound();
            }

            return resultado;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Persona persona)
        {
            await _personaRepositorio.CreateAsync(persona);
            return new CreatedAtRouteResult("ObtenerPersona", new { id = persona.Id }, persona);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Persona value)
        {
            if (id != value.Id)
            {
                return BadRequest();
            }

            await _personaRepositorio.UpdateAsync(value);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Persona> Delete(int id)
        {
            _personaRepositorio.DeleteAsync(id);
            return Ok();
        }
    }
}
