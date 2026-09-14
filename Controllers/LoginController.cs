using Bolos_do_Jacquin.DTO;
using Bolos_do_Jacquin.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bolos_do_Jacquin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsuario _usuario;
        private readonly IConfiguration _configuration;

        public LoginController(
            IUsuario usuario,
            IConfiguration configuration)
        {
            _usuario = usuario;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var usuarioEncontrado =
                await _usuario.BuscarPorEmailESenha(
                    dto.Email,
                    dto.Senha);

            if (usuarioEncontrado == null)
            {
                return Unauthorized("Email ou senha inválidos!");
            }

            var claims = new[]
            {
                new Claim(
                    JwtRegisteredClaimNames.Sub,
                    usuarioEncontrado.IdUsuario.ToString()),

                new Claim(
                    JwtRegisteredClaimNames.Email,
                    usuarioEncontrado.Email),

                new Claim(
                    "nome",
                    usuarioEncontrado.Nome),

                new Claim(
                    ClaimTypes.Role,
                    usuarioEncontrado.Perfil),

                new Claim(
                    JwtRegisteredClaimNames.Jti,
                    Guid.NewGuid().ToString())
            };

            var chaveSecreta = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["Jwt:Key"]!
                )
            );

            var credenciais = new SigningCredentials(
                chaveSecreta,
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                issuer: "Bolos_do_Jacquin",
                audience: "Bolos_do_Jacquin",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: credenciais
            );

            var tokenString =
                new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new
            {
                Token = tokenString,
                Expiracao = token.ValidTo,
                Usuario = new
                {
                    usuarioEncontrado.IdUsuario,
                    usuarioEncontrado.Nome,
                    usuarioEncontrado.Email,
                    usuarioEncontrado.Perfil
                }
            });
        }
    }
}