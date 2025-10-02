
using Microsoft.IdentityModel.Tokens;
using ProjetoPokegochi.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjetoPokegochi.Services
{
    public class TokenService
    {
        private IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(Usuario usuario)
        {
            Claim[] claims = new Claim[]
            {
                new Claim("username", usuario.UserName),
                new Claim("id", usuario.Id),
                new Claim(ClaimTypes.DateOfBirth, usuario.DataNascimento.ToString())
            };
            var key = _configuration["SymmetricSecurityKey"] ?? throw new InvalidOperationException("SymmetricSecurityKey not found in configuration");
            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            var signinCredentials = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                expires:DateTime.Now.AddMinutes(10),
                claims: claims,
                signingCredentials: signinCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}