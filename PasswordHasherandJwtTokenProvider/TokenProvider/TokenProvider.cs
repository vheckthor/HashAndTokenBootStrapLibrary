using PasswordHasherandJwtTokenProvider.Interfaces;
using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace PasswordHasherandJwtTokenProvider
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IConfiguration _config;
        public TokenProvider(IConfiguration config)
        {
            _config = config;

        }

        public string GeneratedToken(string UserUniqueIdentity, string Username, DateTime expirationDate)
        {
            JwtSecurityTokenHandler tokenHandler;
            SecurityToken token;
            Tokenizer(UserUniqueIdentity, Username, expirationDate, out tokenHandler, out token);
            var jwtToken = tokenHandler.WriteToken(token);
            return jwtToken;
        }

        public void Tokenizer(string UserUniqueIdentity, string Username,DateTime expirationDate, out JwtSecurityTokenHandler tokenHandler, out SecurityToken token)
        {

            //you can
            var claims = new[]{
                new Claim(ClaimTypes.NameIdentifier,UserUniqueIdentity.ToString()),
                new Claim(ClaimTypes.Name,Username)

            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                //you can change the expiration date as required
                Expires = expirationDate,
                SigningCredentials = credentials
            };

            tokenHandler = new JwtSecurityTokenHandler();
            token = tokenHandler.CreateToken(tokenDescriptor);
        }

    }
}
