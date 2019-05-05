using System.Net.Mime;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.DbContexts;

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
        Task SetDbContext(ApplicationDbContext dbContext);
    }
}