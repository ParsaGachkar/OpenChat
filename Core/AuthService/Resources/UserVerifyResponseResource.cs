using System;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Core.AuthService.Resources
{
    public class UserVerifyResponseResource
    {
        public string Token { get; set; }
        public UserVerifyResponseResource(UserVerifyResource model, string key)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriber = new SecurityTokenDescriptor()
            {
                Subject = new System.Security.Claims.ClaimsIdentity(
                    new Claim[]{
                        new Claim(CustomClaim.PhoneNumber,model.PhoneNumber)
                    }
                ),Expires=DateTime.UtcNow.AddYears(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriber);
            Token = tokenHandler.WriteToken(token);
        }
    }
}