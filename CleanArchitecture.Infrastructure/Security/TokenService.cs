using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Constants;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CleanArchitecture.Infrastructure.Security
{
   public class TokenService : ITokenService
   {
      private readonly SymmetricSecurityKey _key;
      private readonly UserManager<ApplicationUser> _userManager;
      public TokenService(IConfiguration config, UserManager<ApplicationUser> userManager)
      {
         _userManager = userManager;
         _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config[GlobalConstants.TOKEN_KEY_SECTION]));
      }

      public async Task<string> CreateToken(ApplicationUser user)
      {
         var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.NameId,user.Id),
                new Claim(JwtRegisteredClaimNames.UniqueName,user.UserName)
            };

         // Adding Roles to claims
         var roles = await _userManager.GetRolesAsync(user);

         foreach (var role in roles)
         {
            claims.Add(new Claim(ClaimTypes.Role, role));
         }

         // generate signing Credentials
         var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

         var tokenDescriptor = new SecurityTokenDescriptor
         {
            Subject = new ClaimsIdentity(claims),
            // To test token expiration
            // Expires = DateTime.UtcNow.AddMinutes(1),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = creds
         };

         var tokenHandler = new JwtSecurityTokenHandler();

         var token = tokenHandler.CreateToken(tokenDescriptor);

         return tokenHandler.WriteToken(token);
      }
   }
}