using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Helpers
{
    public class LogCore
    {
        public static void Configure(string appName)
        {
            var environment = GetEnvironment();

            var logConfig = ConfigureDefaults(environment);
            logConfig = ConfigureFile(logConfig, appName);
            // Add more logging sinks here...

            // Set the logger instance to the configured logger.
            Log.Logger = logConfig.CreateLogger();
        }

        //getting environemnt variable for token
        private static string GetEnvironment()
        {
            // The environment variable is needed for some logging configuration.
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
           
            if (environment == null)
            {
                throw new NullReferenceException($"{environment} environment variable is not set.");
            }

            return environment;
        }

        private static LoggerConfiguration ConfigureDefaults(string environment)
        {
            // Use the appsettings.json configuration to override minimum levels and add any additional sinks.
            var config = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json")
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .Build();

            // Minimum levels will be overriden by the configuration file if they are an exact match.
            return new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .ReadFrom.Configuration(config);
        }

        private static LoggerConfiguration ConfigureFile(LoggerConfiguration logConfig, string appName)
        {
            var fileDirectory = $"c:\\logs\\{appName}\\";
            var hostName = Environment.MachineName.ToLower();

            // Add a default async rolling file sink.
            return logConfig
                .WriteTo.Async(a => a.File(
                    formatter: new JsonFormatter(renderMessage: true),
                    path: $"{fileDirectory}\\log-{hostName}-.json",  // Auto-appends the file number to the filename (log-webvm-001.json)
                    rollingInterval: RollingInterval.Day,
                    fileSizeLimitBytes: 50000000, // 50 MB file limit
                    rollOnFileSizeLimit: true,
                    retainedFileCountLimit: 10,
                    buffered: false));
        }
    }
}
