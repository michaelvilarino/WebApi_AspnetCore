using Elmah.Io.AspNetCore;
using Elmah.Io.AspNetCore.HealthChecks;
using Elmah.Io.Extensions.Logging;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MinhaAPICore.Extensoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhaAPICore.Configurations
{
    public static class LoggerConfig
    {
        public static IServiceCollection AddLoggingConfiguration(this IServiceCollection servicos, IConfiguration Configuration)
        {
            servicos.AddElmahIo(o =>
            {
                o.ApiKey = "dfb2a9fd14324c578ec8dde2fde67c9f"; 
                o.LogId = new Guid("b8bb7020-ec91-4d5a-bfb0-3327bef7c88a");
            });

            //servicos.AddLogging(builder =>
            //{
            //    builder.AddElmahIo(o =>
            //    {
            //        o.ApiKey = "dfb2a9fd14324c578ec8dde2fde67c9f";
            //        o.LogId = new Guid("b8bb7020-ec91-4d5a-bfb0-3327bef7c88a");
            //    });

            //    builder.AddFilter<ElmahIoLoggerProvider>(null, LogLevel.Information);
            //});

            servicos.AddHealthChecks()
                    .AddElmahIoPublisher(apiKey: "dfb2a9fd14324c578ec8dde2fde67c9f", new Guid(g: "b8bb7020-ec91-4d5a-bfb0-3327bef7c88a"), application: "API Fornecedores")
                    .AddCheck("Produtos", new SqlServerHealthCheck(Configuration.GetConnectionString(name: "ConexaoBanco")))
                    .AddSqlServer(Configuration.GetConnectionString(name: "ConexaoBanco"), name: "BancoSQL");
            //Instalar o pacote: aspnetcore.healthchecks.sqlserver para melhor manuseio

            servicos.AddHealthChecksUI();// Instalar o pacote: aspnetcore.HealthChecks.ui para gerenciar via interface

            //servicos.AddHealthChecks()
            //        .AddElmahIoPublisher("388dd3a277cb44c4aa128b5c899a3106", new Guid("c468b2b8-b35d-4f1a-849d-f47b60eef096"), "API Fornecedores")
            //        .AddCheck("Produtos", new SqlServerHealthCheck(configuration.GetConnectionString("DefaultConnection")))
            //        .AddSqlServer(configuration.GetConnectionString("DefaultConnection"), name: "BancoSQL");

            return servicos;
        }

        public static IApplicationBuilder UseLoggingConfiguration(this IApplicationBuilder app)
        {
            app.UseElmahIo();


            app.UseHealthChecks("/api/hc", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseHealthChecksUI(options => {
                options.UIPath = "/api/hc-ui";
            });

            //Se for monitorar via elmah, instalar  o pacote: Elmah.Io.AspNetCore.HealthChecks:

            return app;
        }
    }
}
