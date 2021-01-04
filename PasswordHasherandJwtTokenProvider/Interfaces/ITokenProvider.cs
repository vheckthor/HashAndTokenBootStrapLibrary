using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace PasswordHasherandJwtTokenProvider.Interfaces
{
    public interface ITokenProvider
    {
        void Tokenizer(string UserUniqueIdentity, string Username, DateTime expirationDate, out JwtSecurityTokenHandler tokenHandler, out SecurityToken token);
        string GeneratedToken(string UserUniqueIdentity, string Username, DateTime expirationDate);
    }
}
