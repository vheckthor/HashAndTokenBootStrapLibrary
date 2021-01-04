using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PasswordHasherandJwtTokenProvider.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PasswordHasherandJwtTokenProvider.Extensions
{
    public static class IServiceCollectionExtension 
    {
        public static void ConfigureTokenServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITokenProvider, TokenProvider>();
            services.AddScoped<IClaim, Claime>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(Options => {
               Options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                   .GetBytes(configuration
                   .GetSection("AppSettings:Token").Value)),
                   ValidateIssuer = false,
                   ValidateAudience = false
               };
           });
        }
    }
        
}
