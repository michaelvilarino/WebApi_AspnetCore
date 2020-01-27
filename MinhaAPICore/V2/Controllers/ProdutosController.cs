using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Servicos.Interfaces;
using Elmah.Io.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MinhaAPICore.Controllers;

namespace MinhaAPICore.V2.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/produtos")]
    public class ProdutosController : MainController
    {
        private readonly ILogger _logger;

        public ProdutosController(INotificador notificador, IUser appuser, ILogger<ProdutosController> logger) : base(notificador, appuser)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Frase()
        {            
                var i = 0;
                var result = 42 / i;


            _logger.LogTrace("Log de trace");
            _logger.LogDebug("Log de debug");
            _logger.LogInformation("Log de aviso");
            _logger.LogError("Log de erro");
            _logger.LogCritical("Log de problema crítico");

            return "eu sou a versão 2 ";
        }
    }
}