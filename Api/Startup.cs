using Api.Configurations;
using Api.IRepository;
using Api.Repository;
using Api.Services;
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
using System.Linq;
using System.Threading.Tasks;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            //we have to make a reference between context and the sql server
            services.AddDbContext<WmContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );

            //adding authentification and configuring it via our custom method
            //we took out the code to a custom method in order not to clog this class
            services.AddAuthentication();
            services.ConfigureIdentity();
            services.ConfigureJWT(Configuration); //adding json web token 

            //change policy later if needed
            services.AddCors(o => {
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
            });

            services.AddControllers()
                .AddNewtonsoftJson(op => 
                    op.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            //using lowercase urls instead of the default based on name
            services.AddRouting(options => options.LowercaseUrls = true);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

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
