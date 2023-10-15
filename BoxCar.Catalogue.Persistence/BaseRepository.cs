using BoxCar.Catalogue.Core.Contracts.Persistence;
using BoxCar.Catalogue.Domain;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Threading;

namespace BoxCar.Catalogue.Persistence
{
    public class BaseRepository<T, TId> : IAsyncRepository<T, TId> where T : BaseEntity<TId>
    {
        protected readonly BoxCarDbContext _dbContext;

        public BaseRepository(BoxCarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken)
        {
            await _dbContext.AddAsync<T>(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(TId id, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetPagedAsync(int page, int pageSize, CancellationToken cancellationToken)
        {
            page = page < 0 ? 0 : page;
            pageSize = pageSize < 0 ? 100 : pageSize;
            pageSize = pageSize > 500 ? 500 : pageSize;
            return await _dbContext.Set<T>().Skip(page * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
