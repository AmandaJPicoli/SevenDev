using SevenDev.Application.AppUser.Output;
using SevenDev.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SevenDev.Api.Comum
{
    public class TokenService
    {
        public static string GenerateToken(UserViewModel user, string secrets)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secrets);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Name.ToString()),
                    new Claim(ClaimTypes.Role, user.Gender.Description),
                    new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler
                            .CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
