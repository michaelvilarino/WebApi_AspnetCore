using Infra.Contexto;
using Infra.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositorios
{
    public class FornecedorRepositorio : BaseRepositorio<Fornecedor>, IFornecedorRepositorio
    {
        public FornecedorRepositorio(AplicacaoContexto AplicacaoContexto) : base(AplicacaoContexto)
        {
        }

        public async Task<Fornecedor> ObterFornecedorEndereco(Guid Id)
        {
            return await aplicacaoContexto.fornecedores.FirstOrDefaultAsync(f => f.Id == Id);
        }

        public async Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid Id)
        {
            return await aplicacaoContexto.fornecedores.FirstOrDefaultAsync(f => f.Id == Id);
        }
    }
}
