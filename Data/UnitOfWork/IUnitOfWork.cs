using System.Threading.Tasks;
using Data.Domain.Abstractions;
using Data.Repositories.Abstracts;

namespace Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task Commit();
        Task<TRepository> GetRepository<TRepository, TEntity, TKey>() where TRepository : GenericRepository<TEntity, TKey>, new() where TEntity : class, IEntity<TKey>, new();
    }
}