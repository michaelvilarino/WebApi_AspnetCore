using Infra.Contexto;
using Infra.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infra.Repositorios
{
    public abstract class BaseRepositorio<T> : IBaseRepositorio<T> where T : class
    {
        protected AplicacaoContexto aplicacaoContexto;
        protected DbSet<T> dbSet;

        public BaseRepositorio(AplicacaoContexto AplicacaoContexto)
        {
            aplicacaoContexto = AplicacaoContexto;
            dbSet = aplicacaoContexto.Set<T>();
        }

        public virtual async Task<T> ObterPorId(Guid Id)
        {
            return await dbSet.FindAsync(Id);
        }

        public virtual async Task<List<T>> ObterTodos()
        {
            return await dbSet.ToListAsync();
        }

        public Task Atualizar(T entidade)
        {
            dbSet.Update(entidade);
            return Task.CompletedTask;
        }

        public Task Remover(T entidade)
        {
            dbSet.Remove(entidade);
            return Task.CompletedTask;
        }

        public async Task Adicionar(T entidade)
        {
            dbSet.Add(entidade);
            await Task.CompletedTask;
        }

        public async Task<Tuple<IEnumerable<T>, int>> Buscar
        (
            Expression<Func<T, bool>> filtros,
            Expression<Func<T, object>> orderBy = null,
            bool asNoTracking = false,
            int skip = 0,
            int take = 0
        )
        {
            var databaseCount = await dbSet.CountAsync().ConfigureAwait(false);

            if (asNoTracking)
            {
                if (take > 0)
                {
                    if (orderBy != null)
                        return new Tuple<IEnumerable<T>, int>
                        (
                            await dbSet.AsNoTracking().OrderBy(orderBy).Where(filtros).Skip(skip).Take(take).ToListAsync()
                            .ConfigureAwait(false),
                            databaseCount
                        );

                    else
                        return new Tuple<IEnumerable<T>, int>
                        (
                            await dbSet.AsNoTracking().Where(filtros).Skip(skip).Take(take).ToListAsync()
                            .ConfigureAwait(false),
                            databaseCount
                        );
                }
                else
                {
                    if (orderBy != null)
                        return new Tuple<IEnumerable<T>, int>
                        (
                            await dbSet.AsNoTracking().OrderBy(orderBy).Where(filtros).ToListAsync()
                            .ConfigureAwait(false),
                            databaseCount
                        );
                    else
                        return new Tuple<IEnumerable<T>, int>
                        (
                            await dbSet.AsNoTracking().Where(filtros).ToListAsync()
                            .ConfigureAwait(false),
                            databaseCount
                        );
                }
            }
            else
            {
                if (take > 0)
                {
                    if (orderBy != null)
                        return new Tuple<IEnumerable<T>, int>
                        (
                            await dbSet.OrderBy(orderBy).Where(filtros).Skip(skip).Take(take).ToListAsync()
                            .ConfigureAwait(false),
                            databaseCount
                        );
                    else
                        return new Tuple<IEnumerable<T>, int>
                        (
                            await dbSet.Where(filtros).Skip(skip).Take(take).ToListAsync()
                            .ConfigureAwait(false),
                            databaseCount
                        );
                }
                else
                {
                    if (orderBy != null)
                        return new Tuple<IEnumerable<T>, int>
                        (
                            await dbSet.OrderBy(orderBy).Where(filtros).ToListAsync()
                            .ConfigureAwait(false),
                            databaseCount
                        );
                    else
                        return new Tuple<IEnumerable<T>, int>
                        (
                            await dbSet.Where(filtros).ToListAsync()
                            .ConfigureAwait(false),
                            databaseCount
                        );
                }
            }
        }

        public void Dispose()
        {
            aplicacaoContexto?.Dispose();
        }
    }
}
