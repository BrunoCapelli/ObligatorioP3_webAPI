using Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Servicios.IServicios;
using Servicios.Servicios;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private IServicioUsuario _servicioUsuario;
        private readonly IConfiguration _configuration;
        public UsuarioController(IServicioUsuario servicioUsuario, IConfiguration configuration)
        {
            _servicioUsuario = servicioUsuario;
            _configuration = configuration;
        }

        //Restringirlo al Admin
        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult Register([Required] string Alias, [Required] string Password, [Required] string PassConfirm) {
            try {
                if (!String.IsNullOrEmpty(Alias) && !String.IsNullOrEmpty(Password) && !String.IsNullOrEmpty(PassConfirm)) {
                    if (Password.ToLower() == PassConfirm.ToLower()) {
                        UsuarioDTO usuario = new UsuarioDTO { Alias = Alias, Password = Password };
                        try {
                            usuario = _servicioUsuario.Add(usuario);
                            //_servicioAudit.Log(HttpContext.Session.GetString("email") ?? "NULL", usuario.UsuarioDTOId, "Usuario (Add)");
                            return Ok("El usuario se creó correctamente!");

                        }
                        catch (Exception ex) {
                            return Conflict(ex.Message);
                        }
                    }
                    else {
                        return BadRequest("Las contraseñas deben coincidir");
                    }
                }
                else {
                    return BadRequest("Debe completar todos los campos");
                }



            }
            catch (Exception ex) {
                return BadRequest("Error: " + ex.Message);
            }
        }



        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult Login(string Alias, string password)
        {
            if (!String.IsNullOrEmpty(Alias) && !String.IsNullOrEmpty(password))
            {
                UsuarioDTO usuario = new UsuarioDTO { Alias = Alias, Password = password };

                try
                {
                    UsuarioDTO userLogged = _servicioUsuario.FindUser(usuario);
                    if (userLogged != null)
                    {
                        
                        string token = GenerarToken(Alias);
                        return Ok(new { AccessToken = token});

                    }

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

                return Unauthorized();

            }
            else
            {
                return BadRequest("Debe completar los campos");
            }
        }

        [HttpPost]
        public string GenerarToken(string User)
        {
            // Información del usuario
            var userId = User;
            

            // Configuración de la clave secreta y credenciales
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("unaClaveSeguraCienPorCientoRealNoFake"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Crear las reclamaciones del token (claims)
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId)
            };

            // Crear el token JWT
            var token = new JwtSecurityToken(
                //issuer: _configuration["JWTConfig:ValidIssuer"],
                //audience: _configuration["JWTConfig:ValidAudience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1), // Token válido durante 1 hora
                signingCredentials: credentials
            );

            // Convertir el token en una cadena
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return  tokenString;
        }


    }
}
