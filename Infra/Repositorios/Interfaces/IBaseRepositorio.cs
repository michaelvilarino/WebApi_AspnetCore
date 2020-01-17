using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infra.Repositorios.Interfaces
{
    public interface IBaseRepositorio<T>: IDisposable
    {
        Task<T> ObterPorId(Guid Id);
        Task Adicionar(T entidade);
        Task Atualizar(T entidade);
        Task Remover(T entidade);
        Task<List<T>> ObterTodos();
        Task<Tuple<IEnumerable<T>, int>> Buscar
        (
            Expression<Func<T, bool>> filtros,
            Expression<Func<T, object>> orderBy = null,
            bool asNoTracking = false, 
            int skip = 0, 
            int take = 0
        );
    }
}
