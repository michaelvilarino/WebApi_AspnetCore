using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace MinhaAPICore.Configurations
{
    public static class ApiConfig
    {
        public static IServiceCollection WebApiConfig(this IServiceCollection servicos)
        {
            servicos.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            servicos.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true; // mostra no response do header se a api é ok ou obsoleta                
            });            

            servicos.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
            

            //Desabilita a verificação automática das viewmodels
            servicos.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            //Habilita as chamadas para uma aplicação poder consumir

            //O CORS quando habilitado conforme baixo, ele "relaxa" a aplicação para permitir ser chamada por outras
            servicos.AddCors(options =>
            {
                options.AddPolicy(name: "Development",
                                  configurePolicy: builder => builder.AllowAnyOrigin()
                                                                      .AllowAnyMethod()
                                                                      .AllowAnyHeader()
                                                                      .AllowCredentials());

                //options.AddPolicy(name: "Production",
                //        configurePolicy: builder =>
                //        builder
                //               .WithMethods("GET")
                //               .WithOrigins("http://qualsitepodechamarAPI")
                //               .SetIsOriginAllowedToAllowWildcardSubdomains()//Permite todos os subdomínios chamarem a API
                //                                                             //.WithHeaders(HeaderNames.ContentType, "x-custom-header") o que permite enviar no header
                //               .AllowAnyHeader()
                //        );
            });

            return servicos;
        }

        public static IApplicationBuilder UseMvcConfiguration(this IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            //app.UseCors(); quando cors for habilitado, ele deve ser configurado na startup (chamar sempre antes do UseMVC)
            app.UseMvc();

            return app;
        }
    }
}
