using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthService.Application.Interfaces.Providers;
using Domain.Models;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Application.Common.Providers {
    public class JWTProvider : IJWTProvider {
        private readonly IConfiguration config;

        public JWTProvider(IConfiguration config) {
            this.config = config;
        }

        public string GenerateToken(User user) {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.id.ToString())
            };
            var expires = DateTime.UtcNow.AddMinutes(int.Parse(config["JWT:LifetimeMinutes"]!));

            var token = new JwtSecurityToken(signingCredentials: creds, claims: claims, expires: expires);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
