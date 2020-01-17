using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Servicos.Interfaces
{
    public interface IBaseAppServico<T> 
    {
        Task<T> ObterPorId(Guid Id);
        Task<bool> Adicionar(T entidade);
        Task<bool> Atualizar(T entidade);
        Task<bool> Remover(T entidade);
        Task<Tuple<IEnumerable<T>, int>> ObterLista();
    }
}
