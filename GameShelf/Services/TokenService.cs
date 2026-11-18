using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GameShelf.Entities;
using Microsoft.IdentityModel.Tokens;

namespace GameShelf.Services
{
    public class TokenService(IConfiguration config) : ITokenService
    {
        public string CreateAccessToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(config.GetValue<int>("Jwt:ExpiresMinutes")),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
