using System.Net.Mime;
using System;
using System.Threading.Tasks;
using Data.DbContexts;
using Data.Repositories.Abstracts;
using Data.Domain.Abstractions;

namespace Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext dbContext;

        public async Task Commit()
        {
            await dbContext.SaveChangesAsync();
        }
        public async Task<TRepository> GetRepository<TRepository, TEntity, TKey>() where TRepository : GenericRepository<TEntity, TKey>, new() where TEntity : class, IEntity<TKey>, new()
        {
            TRepository repository = new TRepository();
            await repository.SetDbContext(dbContext);
            return repository;
        }
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}