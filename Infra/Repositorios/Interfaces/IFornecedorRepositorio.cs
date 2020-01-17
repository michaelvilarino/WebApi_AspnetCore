using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositorios.Interfaces
{
    public interface IFornecedorRepositorio: IBaseRepositorio<Fornecedor>
    {
        Task<Fornecedor> ObterFornecedorEndereco(Guid Id);
        Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid Id);
        
    }
}
