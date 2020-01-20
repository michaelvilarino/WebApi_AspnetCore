using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhaAPICore.Configurations
{
    public static class ApiConfig
    {
        public static IServiceCollection WebApiConfig(this IServiceCollection servicos)
        {
            servicos.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //Desabilita a verificação automática das viewmodels
            servicos.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            //Habilita as chamadas para uma aplicação poder consumir

            servicos.AddCors(options =>
            {
                options.AddPolicy(name: "Development",
                                  configurePolicy: builder => builder.AllowAnyOrigin()
                                                                      .AllowAnyMethod()
                                                                      .AllowAnyHeader()
                                                                      .AllowCredentials());
            });

            return servicos;
        }

        public static IApplicationBuilder UseMvcConfiguration(this IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            app.UseCors();
            app.UseMvc();

            return app;
        }
    }
}
