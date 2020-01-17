using Business.Servicos.Interfaces;
using Infra;
using Infra.Repositorios.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Servicos
{
    public class EnderecoAppServico : BaseAplicacaoAppServico, IEnderecoAppServico
    {
        private readonly IEnderecoRepositorio enderecoRepositorio;

        public EnderecoAppServico
        (
            IUnitOfWork IUnitOfWork, 
            IEnderecoRepositorio IEnderecoRepositorio,
            INotificador notificador
        ) : base(IUnitOfWork, notificador)
        {
            enderecoRepositorio = IEnderecoRepositorio;
        }

        public async Task<bool> Atualizar(Endereco endereco)
        {
            await enderecoRepositorio.Atualizar(endereco);

            await Uow.Save();

            return true;
        }

        public async Task<bool> Adicionar(Endereco endereco)
        {
            await enderecoRepositorio.Adicionar(endereco);

            await Uow.Save();

            return true;
        }

        public async Task<Tuple<IEnumerable<Endereco>, int>> ObterLista()
        {
            return await enderecoRepositorio.Buscar(f => f != null);
        }

        public async Task<Endereco> ObterPorIdAsync(Guid Id)
        {
            return await enderecoRepositorio.ObterPorId(Id);
        }

        public async Task<bool> Remover(Endereco endereco)
        {
            await enderecoRepositorio.Remover(endereco);

            await Uow.Save();

            return true;
        }

        public async Task<Endereco> ObterPorId(Guid Id)
        {
            return await enderecoRepositorio.ObterPorId(Id);
        }
    }
}
