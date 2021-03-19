using Api.Models;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services
{
    public class AuthManagerService : IAuthManagerService
    {
        private readonly UserManager<ApiUser> _usermanager;
        private readonly IConfiguration _configuration;


        private ApiUser _user;

        public AuthManagerService(UserManager<ApiUser> usermanager,
            IConfiguration configuration)
        {
            _usermanager = usermanager;
            _configuration = configuration;
        }

        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigninCredentials();
            var claims = await GetClaims();
            var token = GenerateTokenOptions(signingCredentials, claims);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //validating the user
        //does it exist and is the password correct
        public async Task<bool> ValidateUser(LoginUserDTO userDTO)
        {
             _user = await _usermanager.FindByNameAsync(userDTO.Email);

            return (_user != null && await _usermanager.CheckPasswordAsync(_user, userDTO.Password));
        }


        private SigningCredentials GetSigninCredentials()
        {
            var key = Environment.GetEnvironmentVariable("KEY");
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, _user.UserName)
            };

            var roles = await _usermanager.GetRolesAsync(_user); //fetches all roles for a user

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("Jwt");


            //how long the token lasts before its not valid 
            var expirationDate = DateTime.Now.AddHours(Convert.ToDouble(
                jwtSettings.GetSection("Lifetime").Value
                ));

            var token = new JwtSecurityToken(
                issuer: jwtSettings.GetSection("validIssuer").Value,
                claims: claims,
                expires: expirationDate,
                signingCredentials: signingCredentials
                );

            return token;
        }

    }
}
