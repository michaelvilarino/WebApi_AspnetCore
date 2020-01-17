using Business.Notificacoes;
using Business.Servicos.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using Infra;
using System.Threading.Tasks;

namespace Business.Servicos
{
    public abstract class BaseAplicacaoAppServico
    {
        protected readonly IUnitOfWork Uow;
        private readonly INotificador _notificador;

        public BaseAplicacaoAppServico(IUnitOfWork IUnitOfWork, INotificador notificador)
        {
            Uow = IUnitOfWork;
            _notificador = notificador;
        }

        protected void BeginTransaction()
        {
            Uow.BeginTransaction();
        }

        protected void Commit()
        {
            Uow.Commit();
        }

        protected void Rollback()
        {
            Uow.Rollback();
        }

        protected async Task<int> Save()
        {            
             return await Uow.Save();
        }      

        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notificar(error.ErrorMessage);
            }
        }

        protected void Notificar(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
        }

        protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE>
        {
            var validator = validacao.Validate(entidade);

            if (validator.IsValid) return true;

            Notificar(validator);

            return false;
        }
    }
}
