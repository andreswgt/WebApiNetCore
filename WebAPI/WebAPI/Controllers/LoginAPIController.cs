using CursoUdemyWebAPI.DTO;
using CursoUdemyWebAPI.Modelo;
using CursoUdemyWebAPI.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CursoUdemyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginAPIController : ControllerBase
    {

        private readonly IServicioUsuarioAPI servicio;
        private readonly IConfiguration configuration;
        public LoginAPIController(IServicioUsuarioAPI s, IConfiguration configuration)
        {
            this.servicio = s;
            this.configuration = configuration;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<UsuarioApiDTO>> Login(LoginAPI usuarioLogin)
        {
            UsuarioAPI Usuario = null;
            Usuario = await AutenticarUsuarioAsync(usuarioLogin);
            if (Usuario == null)
                throw new Exception("Credenciales no válidas");
            else
                Usuario = GenerarTokenJWT(Usuario);


            return Usuario.convertirDTO();
        }

        private async Task<UsuarioAPI> AutenticarUsuarioAsync(LoginAPI usuarioLogin)
        {
            UsuarioAPI usuarioAPI = await servicio.DameUsuario(usuarioLogin);
            return usuarioAPI;
        }

        private UsuarioAPI GenerarTokenJWT(UsuarioAPI usuarioInfo)
        {
            // Cabecera
            var _symmetricSecurityKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["JWT:ClaveSecreta"])
                );
            var _signingCredentials = new SigningCredentials(
                    _symmetricSecurityKey, SecurityAlgorithms.HmacSha256
                );
            var _Header = new JwtHeader(_signingCredentials);
            // Claims
            var _Claims = new[] {
                new Claim("usuario", usuarioInfo.Usuario),
                new Claim("email", usuarioInfo.Email),
                new Claim(JwtRegisteredClaimNames.Email, usuarioInfo.Email),
            };

            //Payload
            var _Payload = new JwtPayload(
                    issuer: configuration["JWT:Issuer"],
                    audience: configuration["JWT:Audience"],
                    claims: _Claims,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.AddMinutes(30)
                );

            // Token
            var _Token = new JwtSecurityToken(
                    _Header,
                    _Payload
                );

            usuarioInfo.Token = new JwtSecurityTokenHandler().WriteToken(_Token);

            return usuarioInfo;

        }

    }
}
