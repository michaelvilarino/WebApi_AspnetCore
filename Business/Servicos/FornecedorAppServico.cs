using Business.Servicos.Interfaces;
using Business.Validacoes;
using Infra;
using Infra.Repositorios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Servicos
{
    public class FornecedorAppServico : BaseAplicacaoAppServico, IFornecedorAppServico
    {
        IFornecedorRepositorio FornecedorRepositorio;
        IEnderecoRepositorio enderecoRepositorio;

        public FornecedorAppServico(
            IUnitOfWork IUnitOfWork,
            IFornecedorRepositorio IFornecedorRepositorio,
            INotificador notificador,
            IEnderecoRepositorio EnderecoRepositorio
            ) : base(IUnitOfWork, notificador)
        {
            FornecedorRepositorio = IFornecedorRepositorio;
            enderecoRepositorio = EnderecoRepositorio;
        }

        public async Task<bool> Atualizar(Fornecedor entidade)
        {
            try
            {
                if (!ExecutarValidacao(new FornecedorValidacao(), entidade)) return false;

                var fornecedorBanco = await FornecedorRepositorio.ObterPorId(entidade.Id);

                fornecedorBanco.Ativo = entidade.Ativo != fornecedorBanco.Ativo ? entidade.Ativo : fornecedorBanco.Ativo;
                fornecedorBanco.DataCadastro = entidade.DataCadastro != fornecedorBanco.DataCadastro ? entidade.DataCadastro : fornecedorBanco.DataCadastro;
                fornecedorBanco.Documento = entidade.Documento != fornecedorBanco.Documento ? entidade.Documento : fornecedorBanco.Documento;
                fornecedorBanco.Nome = entidade.Nome != fornecedorBanco.Nome ? entidade.Nome : fornecedorBanco.Nome;

                await FornecedorRepositorio.Atualizar(fornecedorBanco);

                await Uow.Save();

                return true;
            }
            catch (Exception ex)
            {
                Notificar("Ocorreu um erro: " + ex.Message);
                return false;
            }

        }

        public async Task<bool> Adicionar(Fornecedor entidade)
        {
            try
            {
                if (!ExecutarValidacao(new FornecedorValidacao(), entidade)) return false;

                if (FornecedorRepositorio.Buscar(f => f.Documento == entidade.Documento, null, true, 0, 0).Result.Item1.Any())
                {
                    Notificar(mensagem: "Já existe um fornecedor com este documento informado");
                    return false;
                }

                await FornecedorRepositorio.Adicionar(entidade);

                await Uow.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }

        public Task<Tuple<IEnumerable<Fornecedor>, int>> ObterLista()
        {
            return FornecedorRepositorio.Buscar(f => f.Id != null, o => o.Id, false, 0, 0);
        }

        public Task<Fornecedor> ObterPorId(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Remover(Fornecedor entidade)
        {
            var fornecedor = await FornecedorRepositorio.ObterFornecedorProdutosEndereco(entidade.Id);

            if (fornecedor.Produtos.Any())
            {
                Notificar(mensagem: "O fornecedor possui produtos cadastrados");
                return false;
            }

            if (fornecedor.Enderecos.Any())
            {
                Notificar(mensagem: "O fornecedor possui endereços cadastrados");
                return false;
            }

            await FornecedorRepositorio.Remover(fornecedor);

            await Uow.Save();

            return true;
        }

        public async Task<Fornecedor> ObterPorId(Guid Id)
        {
            return await FornecedorRepositorio.ObterPorId(Id);
        }
    }
}
