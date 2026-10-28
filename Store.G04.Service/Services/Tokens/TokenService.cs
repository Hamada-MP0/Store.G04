using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Store.G04.Core.Entity.Identity;
using Store.G04.Core.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Store.G04.Service.Services.Tokens
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;

        public TokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

     


        public async Task<string> CreatTokenAsync(AppUser user , UserManager<AppUser>userManager)
        {


            var authclaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.DisplayName),
                new Claim(ClaimTypes.MobilePhone,user.PhoneNumber),

            };

            var userRoles = await userManager.GetRolesAsync(user);

            foreach(var role in userRoles )
            {
                authclaims.Add( new Claim (ClaimTypes.Role, role));
            }

            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));

            var Token = new JwtSecurityToken(
                
                issuer: configuration["JWT:Issuer"],
                audience: configuration["JWT:Audience"],
                expires:DateTime.Now.AddHours(double.Parse(configuration["JWT:DurationInDays"])),
                claims :authclaims,
                signingCredentials:new SigningCredentials(authKey,SecurityAlgorithms.HmacSha256Signature)


                );
            return new JwtSecurityTokenHandler().WriteToken(Token); // to encoding a token
        }
    }
}
