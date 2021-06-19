using Api.Configurations;
using Api.IRepository;
using Api.Repository;
using Api.Services;
using Api.Utilities;
using AspNetCoreRateLimit;
using Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; } 

        public Startup(IConfiguration configuration )
        {
           Configuration = configuration; 
        }


        // This method gets called by the runtime. adds services to the IoC container.
        public void ConfigureServices(IServiceCollection services)
        {

            //we have to make a reference between context and the sql server
            services.AddDbContext<WmContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );

            //in memory caching not distributed
            //required for throttling
            services.AddMemoryCache();

            //throttling aka rate limiting
            //added middleware down below too
            services.ConfigureThrottling();
            services.AddHttpContextAccessor();

            //adding server side caching 
            //we have to register the middleware after in configure
            services.ConfigureHttpCacheHeaders();

            //adding authentification and configuring it via our custom method
            //we took out the code to a custom method in order not to clog this class
            services.AddAuthentication();
            services.ConfigureIdentity(); //custom method for configuring identity 
            services.ConfigureJWT(Configuration); //adding json web token via a custom method check the underlying class

            //change policy later if needed
            //adding who can call the api
            services.AddCors(o =>
            {
                o.AddPolicy("AllowAll", builder =>
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            //auto mapper services settup
            //maybe change later to have individual profiles 
            services.AddAutoMapper(typeof(MapperInitializer));


            //controller services
            //add transient means that every time unit of work is needed a new instance of it is created
            //add scoped means that a new instance is created for a period/lifetime of a set of requests
            //add singleton means that only one instance will exist for the duration of the application
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthManagerService, AuthManagerService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Water Meter API",
                    Version = "v1",
                    Description = "Api is being developed as part of my masters thesis. Work in progress"
                });

                /*
                 * The combination of SecuritySchemeType.ApiKey & Scheme="Bearer" seems to not provide any scheme value in the parameter of the Authorization header. 
                 * Any scheme value used with SecuritySchemeType.ApiKey results in a header with a format of {"Authorization","value"}. 
                 * If {"Authorization","scheme value"} is desired, then SecuritySchemeType.Http & Scheme="Bearer" should be used. 
                 */

                c.OperationFilter <AuthorizeCheckOperationFilter>();
                c.EnableAnnotations();
              
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

              
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });

           

            /* caching
             * param for addcontroller
             * then we can put [ResponseCache(CacheProfileName ="60SecondsDuration")] over our controllers
             * 
             * conf=> {
                conf.CacheProfiles.Add("60SecondsDuration", new CacheProfile
                {
                    Duration = 60
                    //other
                });}
            *ive decided to try out a library check measurements controller for how its used, aswell as ServiceExtensions
             
             */

            //setting up Newtonsoft Json
            services.AddControllers()
                .AddNewtonsoftJson(op =>
                    op.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            //using lowercase urls instead of the default based on name
            services.AddRouting(options => options.LowercaseUrls = true);

            //configuring api versioning
            services.ConfigureApiVersioning();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    string basePath = string.IsNullOrEmpty(c.RoutePrefix) ? "." : "..";
                    c.SwaggerEndpoint($"{basePath}/swagger/v1/swagger.json", "Water Meter API");

                });
            }

            //if the decision is made to use global error handling
            //global exception handling
            //app.ConfigureGlobalExceptionHandler();

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            //caching middleware
            //Age param in postman header is how long is left for the cache
            app.UseResponseCaching();
            app.UseHttpCacheHeaders();

            //throttling middleware
            app.UseIpRateLimiting();
            app.UseRouting();

            //authentification comes first is important
            app.UseAuthentication();
            app.UseAuthorization();

          

            app.UseEndpoints(endpoints =>
            {
                /* convention based routing schema
                 * 
                 * endpoints.MapControllerRoute(
                 *      name: "default",
                 *      pattern: "{controller=Home}/{action=Index}/{id?}");
                 */
                endpoints.MapControllers();
            });
        }
    }
}
