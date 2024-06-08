using Infrastrucrure.DependencyInjection.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Abstractions;
namespace Infrastrucrure.Authentication
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly JwtOption jwtOption = new JwtOption();
        public JwtTokenService(IConfiguration configuration  ) { 
        configuration.GetSection(nameof(JwtOption)).Bind(jwtOption);
        }
        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption.SecretKey));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                issuer: jwtOption.User,
                audience: jwtOption.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(jwtOption.ExpiredMin),
                signingCredentials: signingCredentials
                );
        }
    }
}
