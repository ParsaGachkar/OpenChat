using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.DbContexts;
using Data.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Abstracts
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>, new() where TKey : class
    {
        private readonly ApplicationDbContext dbContext;

        public async Task Commit()
        {
            await dbContext.SaveChangesAsync();
        }

        public async Task Create(TEntity entity)
        {
            await dbContext.Set<TEntity>().AddAsync(entity);
        }

        public async Task Delete(TEntity entity)
        {
            dbContext.Set<TEntity>().Remove(entity);
            await Task.CompletedTask;
        }

        public async Task<TEntity> Read(TKey Id)
        {
            return await (dbContext.Set<TEntity>().FindAsync(Id));
        }

        public async Task<ICollection<TEntity>> ReadAll()
        {
            return await (dbContext.Set<TEntity>().ToArrayAsync());
        }

        public async Task Update(TEntity entity)
        {
            dbContext.Update(entity);
        }

        public GenericRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}