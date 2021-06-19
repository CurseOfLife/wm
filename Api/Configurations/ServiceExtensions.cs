
using Api.Models.Other;
using AspNetCoreRateLimit;
using Data;
using Domain.Identity;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Serilog;
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
            var builder = services.AddIdentityCore<User>(q => q.User.RequireUniqueEmail = true);

            //
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), services);
            //adding the store location of our identity classes
            builder.AddEntityFrameworkStores<WmContext>()
                .AddDefaultTokenProviders()
                .AddRoleManager<RoleManager<IdentityRole>>(); //.AddSignInManager<SignInManger<User>>()
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

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(o =>
            {
                o.SaveToken = true;
                //o.RequireHttpsMetadata = true; //default is true
          
                o.TokenValidationParameters = new TokenValidationParameters
                {
                 
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.GetSection("Issuer").Value,
                    ValidateAudience = true, 
                    ValidAudience = jwtSettings.GetSection("Audience").Value,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,                   
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),  //encoding the id and hashing it
                    ClockSkew = TimeSpan.Zero // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                };            

            });

        }

        //global exceptions
        public static void ConfigureGlobalExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        Log.Error($"Something went wrong while trying {contextFeature.Error}");

                        await context.Response.WriteAsync(new GlobalError
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error. Please Try Again Later."
                        }.ToString());
                    }

                });
            });
        }

        //cache headers Marvin.Cache.Headers library
        public static void ConfigureHttpCacheHeaders(this IServiceCollection services)
        {
            services.AddResponseCaching();
            services.AddHttpCacheHeaders(
                (expirationOption) =>
                {
                    expirationOption.MaxAge = 60;
                    expirationOption.CacheLocation = CacheLocation.Private;
                },
                (validationOption) =>
                {
                    validationOption.MustRevalidate = true; //once data changed must go through getting data from db not cache
                });
        }

        //api versioning
        public static void ConfigureApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(opt =>
            {
                opt.ReportApiVersions = true; // responce header will have an api version field
                opt.AssumeDefaultVersionWhenUnspecified = true; //if user didnt specify use default version
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });
        }

        //adding throttling otherwise called rate limiting
        public static void ConfigureThrottling(this IServiceCollection services)
        {
            //defining the throttling rules
            var throttlingRules = new List<RateLimitRule>
            {
                new RateLimitRule
                {
                    Endpoint = "*",
                    Limit = 100,
                    Period = "10m"
                }
                //we can have multiple rules for specific endpoints
                /*new RateLimitRule  {
                    Endpoint = "*",
                    Limit = 100,
                    Period = "10m"
                },*/
            };

            services.Configure<IpRateLimitOptions>(o =>
            {
                o.GeneralRules = throttlingRules;
            });

            //required for AspNetCoreRateLimit library
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        }
    }
}
