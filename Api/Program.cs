using Api.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api
{
    public class Program
    {
        public const string ApplicationName = "wm";

        public static void Main(string[] args)
        {
            /*
            Log.Logger = new LoggerConfiguration()
               .WriteTo.File(
                   path: "C:\\Users\\Korisnik\\Desktop\\waterm\\log\\log-.txt",
                   outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                   rollingInterval: RollingInterval.Day,
                   restrictedToMinimumLevel: LogEventLevel.Information
               ).CreateLogger();*/

           
            
            //configuring logging
            LogCore.Configure(ApplicationName);
            

            //testing logging 
            //when ever the api starts
            try
            {
                Log.Information("Application Is Starting");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application failed to start");
            }
            finally
            {
                Log.CloseAndFlush();
            }


            CreateHostBuilder(args).Build().Run();
        }

        //using serilog as the default logger configuration
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
