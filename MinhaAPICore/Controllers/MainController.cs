using System;
using System.Linq;
using Business.Notificacoes;
using Business.Servicos.Interfaces;
using Infra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MinhaAPICore.Controllers
{

    [ApiController]
    public abstract class MainController : ControllerBase
    {
        // validação de notificações de erros
        // validação de modelstate
        // validação de operação de negócios

        private readonly INotificador _notificador;

        public MainController(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected bool OperacaoValida()
        {
            return !_notificador.TemNotificacao();
        }

        protected ActionResult CustomResponse(object resultado = null)
        {
            if (OperacaoValida())
            {
                return Ok(new { 
                  success = true,
                  data = resultado
                });
            }

            return BadRequest(new
            {
                success = false,
                erros = _notificador.ObterNotificacoes().Select(n => n.Mensagem)
            });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotificarErroModelInvalida(modelState);
            return CustomResponse();
        }

        protected void NotificarErroModelInvalida(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);

            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotificarErro(erroMsg);
            }
        }

        protected void NotificarErro(string msg)
        {
            _notificador.Handle(new Notificacao(msg));
        }

    }
}
