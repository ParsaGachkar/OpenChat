using System.Collections.Generic;
using System.Threading.Tasks;
namespace Data.Repositories.Abstracts
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : class, new()
    {
        Task Create(TEntity entity);
        Task<ICollection<TEntity>> ReadAll();
        Task<TEntity> Read(TKey Id);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
        Task Commit();
    }
}