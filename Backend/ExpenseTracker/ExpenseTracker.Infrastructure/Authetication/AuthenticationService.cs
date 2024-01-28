using ExpenseTracker.Application;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Infrastructure
{
    internal class AuthenticationService : IAuthenticationService
    {
        public AuthenticationService()
        {
            
        }
        public string GenerateJwtToken(string userName, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = Encoding.ASCII.GetBytes("ThisIsSecretKeyForTokenValidation"); //TODO: Configurable

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Role, role)
                }),
                Audience = "https://localhost:7044/", // who or what the token intended for (UI in most of the seenario)
                Issuer = "https://localhost:7044/",  // who created and signed this token (Same API domain in this case, if we move auth service out of the project it will the project domain)
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
