using AutoMapper;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MinhaAPICore.Configurations;
using MinhaAPICore.Extensoes;


namespace MinhaAPICore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment hostingEnvironment)
        {
            var builder = new ConfigurationBuilder()
                 .SetBasePath(hostingEnvironment.ContentRootPath)
                 .AddJsonFile("appsettings.json", true, true)
                 .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", true, true)
                 .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddLoggingConfiguration(configuration);          

            services.AddHealthChecksUI();// Instalar o pacote: aspnetcore.HealthChecks.ui para gerenciar via interface

            IoCConfig.RegistrarServicos(services);

            services.AddIdentityConfiguration(Configuration);

            services.AddAutoMapper(typeof(Startup));

            services.WebApiConfig();

            services.AddSwaggerConfig();           
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseCors("Development");
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseCors("Production");
                app.UseHsts();
            }

            app.UseAuthentication();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseMvcConfiguration();

            app.UseSwaggerConfig(provider);

           // app.UseLoggingConfiguration();

        }
    }
}

//PARA VERSIONAMENTO DA API, INSTALAR: microsoft.aspnetcore.mvc.versioning E microsoft.aspnetcore.mvc.Versioning.ApiExplorer

    //PARA RODAR PO MIGRATION EM PRODUÇÃO, RODAR O SEGUINTE COMANDO NO PACKAGE MANAGER PRIMEIRO:
    //$env:ASPNETCORE_ENVIRONMENT='Production'