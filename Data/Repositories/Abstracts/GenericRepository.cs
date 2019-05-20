using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.DbContexts;
using Data.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Abstracts
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>, new()
    {
        protected ApplicationDbContext dbContext;

        public virtual async Task Commit()
        {
            await dbContext.SaveChangesAsync();
        }

        public virtual async Task Create(TEntity entity)
        {
            await dbContext.Set<TEntity>().AddAsync(entity);
        }

        public virtual async Task Delete(TEntity entity)
        {
            dbContext.Set<TEntity>().Remove(entity);
            await Task.CompletedTask;
        }

        public virtual async Task<TEntity> Read(TKey Id)
        {
            return await (dbContext.Set<TEntity>().FindAsync(Id));
        }

        public virtual async Task<ICollection<TEntity>> ReadAll()
        {
            return await (dbContext.Set<TEntity>().ToListAsync());
        }

        public virtual async Task Update(TEntity entity)
        {
            dbContext.Update(entity);
            await Task.CompletedTask;
        }

        public async Task SetDbContext(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            await Task.CompletedTask;
        }

        public GenericRepository()
        {

        }
    }
}