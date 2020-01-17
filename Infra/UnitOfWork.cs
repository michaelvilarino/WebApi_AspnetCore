using Infra.Contexto;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infra
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AplicacaoContexto aplicacaoContexto;
        private IDbContextTransaction _transaction;

        public UnitOfWork(AplicacaoContexto AplicacaoContexto)
        {
            aplicacaoContexto = AplicacaoContexto;
        }   

        public async Task BeginTransaction()
        {
            _transaction = await aplicacaoContexto.Database.BeginTransactionAsync();           
        }

        public Task Commit()
        {
            _transaction.Commit();
            return Task.CompletedTask;
        }

        public Task Dispose()
        {
            aplicacaoContexto?.Dispose();
            return Task.CompletedTask;
        }

        public Task Rollback()
        {
            _transaction.Rollback();
            return Task.CompletedTask;
        }

        public async Task<int> Save()
        {
            return await aplicacaoContexto.SaveChangesAsync();
        }

        void IDisposable.Dispose()
        {
            _transaction?.Dispose();
            aplicacaoContexto?.Dispose();
        }
    }
}
