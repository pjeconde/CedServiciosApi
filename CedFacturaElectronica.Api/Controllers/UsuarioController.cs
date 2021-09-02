using CedFacturaElectronica.Core.DTOs;
using CedFacturaElectronica.Core.Entidades;
using CedFacturaElectronica.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CedFacturaElectronica.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<UsuarioAplicacion> _signInManager;
        private readonly UserManager<UsuarioAplicacion> _userManager;
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio, UserManager<UsuarioAplicacion> userManager,
        SignInManager<UsuarioAplicacion> signInManager,
        IConfiguration configuration)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("Crear")]
        public async Task<ActionResult<UsuarioLogin>> CrearUsuario([FromBody] UsuarioAplicacion model)
        {
            var user = new UsuarioAplicacion
            {
                UserName = model.UserName,
                Email = model.Email,
                Clave = model.Clave,
                PhoneNumber = model.PhoneNumber,
                CantidadEnviosMail = 1,
                FechaUltimoReenvioMail = DateTime.Now,
                Pregunta = model.Pregunta,
                Respuesta = model.Respuesta,
                IdWF = model.IdWF,
                Nombre = model.Nombre,
                Apellido = model.Apellido
            };
            var result = await _userManager.CreateAsync(user, model.Clave);

            if (result.Succeeded)

            {
                //await _usuarioRepositorio.CreateAsync(model);
                UsuarioTokenDTO usuarioTokenDTO = new UsuarioTokenDTO();
                usuarioTokenDTO = CrearToken(model, new List<string>());
                UsuarioInfoDTO usuarioInfoDTO = new UsuarioInfoDTO();
                usuarioInfoDTO.NombreCompleto = model.Nombre + " " + model.Apellido;
                usuarioInfoDTO.NombreCuenta = model.Email;

                UsuarioLogin usuarioLogin = new UsuarioLogin();
                usuarioLogin.UsuarioTokenDTO = usuarioTokenDTO;
                usuarioLogin.UsuarioInfoDTO = usuarioInfoDTO;

                return Ok(usuarioLogin);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<UsuarioAplicacion> Delete(int id)
        {
            _usuarioRepositorio.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioAplicacion>>> Get()
        {
            return await _usuarioRepositorio.GetAllAsync();
        }

        [HttpGet("{id}", Name = "ObtenerUsuario")]
        public async Task<ActionResult<UsuarioAplicacion>> Get(int id)
        {
            var resultado = await _usuarioRepositorio.GetByIdAsync(id);

            if (resultado == null)
            {
                return NotFound();
            }

            return resultado;
        }

        [HttpGet("{nombreCuenta},{clave}")]
        public async Task<ActionResult<UsuarioAplicacion>> Get(string nombreCuenta, string clave)
        {
            var resultado = await _usuarioRepositorio.GetByAccountAsync(nombreCuenta, clave);

            if (resultado == null)
            {
                return NotFound();
            }

            return resultado;
        }

        [HttpPost("Ingresar")]
        public async Task<ActionResult<UsuarioLogin>> Ingresar([FromBody]
        UsuarioAplicacion userInfo)
        {
                    var result = await
           _signInManager.PasswordSignInAsync(userInfo.UserName, userInfo.Clave,
           isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                
                var usuario = await _userManager.FindByNameAsync(userInfo.UserName);

                UsuarioTokenDTO usuarioTokenDTO = new UsuarioTokenDTO();
                usuarioTokenDTO = CrearToken(usuario, new List<string>());
                UsuarioInfoDTO usuarioInfoDTO = new UsuarioInfoDTO();
                usuarioInfoDTO.NombreCompleto = usuario.Nombre + " " + usuario.Apellido;
                usuarioInfoDTO.NombreCuenta = usuario.UserName;

                UsuarioLogin usuarioLogin = new UsuarioLogin();
                usuarioLogin.UsuarioTokenDTO = usuarioTokenDTO;
                usuarioLogin.UsuarioInfoDTO = usuarioInfoDTO;

                return Ok(usuarioLogin);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Ingreso inválido");
                return BadRequest(ModelState);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(UsuarioAplicacion usuario)
        {
            await _usuarioRepositorio.CreateAsync(usuario);
            return new CreatedAtRouteResult("ObtenerUsuario", new { id = usuario.Id }, usuario);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, UsuarioAplicacion value)
        {

            if (id.ToString() != value.Id)
            {
                return BadRequest();
            }

            await _usuarioRepositorio.UpdateAsync(value);
            return Ok();
        }

        private UsuarioTokenDTO CrearToken(UsuarioAplicacion userInfo, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
                new Claim("miValor", "FacturaElectronicaCedeira"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var rol in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, rol));
            }

            var key = new
            SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
            var creds = new SigningCredentials(key,
            SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddYears(1);
            JwtSecurityToken token = new JwtSecurityToken(
            issuer: null,
            audience: null,
            claims: claims,
            expires: expiration,
            signingCredentials: creds);
            return new UsuarioTokenDTO()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                FechaExpiracion = expiration
            };
        }
    }
}