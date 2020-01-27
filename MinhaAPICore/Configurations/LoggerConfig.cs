using Elmah.Io.AspNetCore;
using Elmah.Io.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhaAPICore.Configurations
{
    public static class LoggerConfig
    {
        public static IServiceCollection AddLoggingConfiguration(this IServiceCollection servicos)
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

            //servicos.AddHealthChecks()
            //        .AddElmahIoPublisher("388dd3a277cb44c4aa128b5c899a3106", new Guid("c468b2b8-b35d-4f1a-849d-f47b60eef096"), "API Fornecedores")
            //        .AddCheck("Produtos", new SqlServerHealthCheck(configuration.GetConnectionString("DefaultConnection")))
            //        .AddSqlServer(configuration.GetConnectionString("DefaultConnection"), name: "BancoSQL");

            return servicos;
        }

        public static IApplicationBuilder UseLoggingConfiguration(this IApplicationBuilder app)
        {
            app.UseElmahIo();

            return app;
        }
    }
}
