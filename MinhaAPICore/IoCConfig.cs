using Business.Notificacoes;
using Business.Servicos;
using Business.Servicos.Interfaces;
using Infra;
using Infra.Contexto;
using Infra.Repositorios;
using Infra.Repositorios.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MinhaAPICore.Configurations;
using MinhaAPICore.Data;
using MinhaAPICore.Extensoes;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MinhaAPICore
{
    public class IoCConfig
    {
        private static IServiceCollection _services;

        public static void RegistrarServicos(IServiceCollection servicos)
        {
            servicos.AddScoped<AppDbContext>();

            //Registrar repositórios
            servicos.AddScoped<IEnderecoRepositorio, EnderecoRepositorio>();
            servicos.AddScoped<IFornecedorRepositorio, FornecedorRepositorio>();
            servicos.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();

            //Registrar serviços
            servicos.AddScoped<IEnderecoAppServico, EnderecoAppServico>();
            servicos.AddScoped<IFornecedorAppServico, FornecedorAppServico>();
            servicos.AddScoped<IProdutoAppServico, ProdutoAppServico>();
            servicos.AddScoped<INotificador, Notificador>();

            //Data
            servicos.AddDbContext<AplicacaoContexto>();
            servicos.AddScoped<IUnitOfWork, UnitOfWork>();

            servicos.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            servicos.AddScoped<IUser, AspNetUser>();

            servicos.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            _services = servicos;

        }

        public static T GetService<T>() where T : class
        {
            return _services.BuildServiceProvider().GetService<T>();
        }
    }
}
