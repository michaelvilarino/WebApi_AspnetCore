using System.Threading.Tasks;

namespace Infra
{
    public interface IUnitOfWork
    {
        Task BeginTransaction();
        Task Commit();
        Task Rollback();
        Task<int> Save();        
    }
}
