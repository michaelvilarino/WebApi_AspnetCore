using Business.Servicos.Interfaces;
using Business.Validacoes;
using Infra;
using Infra.Repositorios.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Servicos
{
    public class ProdutoAppServico : BaseAplicacaoAppServico, IProdutoAppServico
    {
        private readonly IProdutoRepositorio _produtoRepositorio;

        public ProdutoAppServico
        (
            IUnitOfWork IUnitOfWork, 
            INotificador notificador,
            IProdutoRepositorio IProdutoRepositorio
         ) : base(IUnitOfWork, notificador)
        {
            _produtoRepositorio = IProdutoRepositorio;
        }

        public async Task<bool> Atualizar(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidacao(), produto)) return false;

            var produtoBase = await _produtoRepositorio.ObterPorId(produto.Id);

            if (produtoBase == null)
            {
                Notificar("Produto informado não existe!");
                return false;
            }

            produtoBase = produto;

            await _produtoRepositorio.Atualizar(produtoBase);

            await Uow.Save();

            return true;

        }

        public async Task<bool> Adicionar(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidacao(), produto)) return false;

            await _produtoRepositorio.Adicionar(produto);

            return true;
        }

        public async Task<Tuple<IEnumerable<Produto>, int>> ObterLista()
        {
            return await _produtoRepositorio.Buscar(f => f != null);
        }

        public async Task<bool> Remover(Produto produto)
        {
            var produtoBase = await _produtoRepositorio.ObterPorId(produto.Id);

            if (produtoBase == null)
            {
                Notificar("Produto não encontrado!");
                return false;
            }

            await _produtoRepositorio.Remover(produtoBase);

            await Uow.Save();

            return true;
        }

        public async Task<Produto> ObterPorId(Guid Id)
        {
            return await _produtoRepositorio.ObterPorId(Id);
        }
    }
}
