
using Data;
using Domain.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services
{
    public static class ServiceExtensions
    {
        //method used to configure identity in startup file
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            //required that the email is unique
            var builder = services.AddIdentityCore<ApiUser>(q => q.User.RequireUniqueEmail = true);

            //
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), services);
            //adding the store location of our identity classes
            builder.AddEntityFrameworkStores<WmContext>().AddDefaultTokenProviders();
        
        }

        //configuring jwt in startup
        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("Jwt");

            //use cmd admin mode to save system variable instead of writing a key value in json file.. for security
            // setx KEY "da655380-a5f9-42a7-8ac5-e40a59cf273a" /M
            // /M means its system not local variable
            // same should be done on the machiene the server is on
            var key = Environment.GetEnvironmentVariable("KEY");

            services.AddAuthentication(o =>{
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(o => {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.GetSection("Issuer").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))  //encoding the id and hashing it
                    //more validations depending what we need
                };

});
        
        }
    }
}
