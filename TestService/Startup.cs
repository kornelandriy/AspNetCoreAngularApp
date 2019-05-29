using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Serilog;
using Serilog.Events;

namespace TestService
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            Log.Logger = new LoggerConfiguration()
                .WriteTo.ColoredConsole(LogEventLevel.Verbose,
                    "{NewLine}{Timestamp:HH:mm:ss} [{Level}] ({CorrelationToken}) {Message}{NewLine}{Exception}")
                .WriteTo.RollingFile(GetLogFile(),
                    outputTemplate: "{NewLine}{Timestamp:HH:mm:ss} [{Level}] ({CorrelationToken}) {Message}{NewLine}{Exception}")
                .CreateLogger();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling =
                        ReferenceLoopHandling.Ignore;
                });

//            services.AddDbContext<WTADbContext>
//                (options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString")), ServiceLifetime.Transient);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddCors(
                options => options.AddPolicy("AllowCors",
                    builder =>
                    {
                        builder
//                            .WithOrigins("http://localhost:4200")
                            .AllowAnyOrigin()
                            .AllowCredentials()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    })
            );
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler("/Error");
            
            app.UseCors("AllowCors");

            app.UseStaticFiles();
            
            if (bool.Parse(Configuration["BuildAndRunUI"]))
            {
                app.UseSpaStaticFiles();
            }

            if (bool.Parse(Configuration["BuildAndRunUI"]))
            {
                app.UseSpaStaticFiles();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller}/{action=Index}/{id?}");
            });
            
            Log.Information("Try to start SPA");
            if (bool.Parse(Configuration["BuildAndRunUI"]))
            {
                app.UseSpa(spa =>
                {
                    // To learn more about options for serving an Angular SPA from ASP.NET Core,
                    // see https://go.microsoft.com/fwlink/?linkid=864501

                    spa.Options.SourcePath = "ClientApp";
                    Log.Information("Check for deployment");
                    if (env.IsDevelopment())
                    {
                        Log.Information("Start SPA");
                        spa.UseAngularCliServer(npmScript: "start");
                    }
                });
            }
        }


        private static string GetLogFile()
        {
            var date = DateTime.UtcNow.Date.ToString("dd-MMM-yyyy");
            var logFilePath = $"{AppDomain.CurrentDomain.BaseDirectory}\\Logs\\log-{date}.txt";

            return logFilePath;
        }
    }
}