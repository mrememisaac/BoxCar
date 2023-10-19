using BoxCar.Catalogue.Core.Contracts.Persistence;
using BoxCar.Catalogue.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Security.Cryptography;
using System.Threading;

namespace BoxCar.Catalogue.Persistence.Repositories
{
    public class BaseRepository<T, TId> : IAsyncRepository<T, TId> where T : BaseEntity<TId>
    {
        protected readonly DbContextOptions<BoxCarCatalogueDbContext> _dbContextOptions;

        public BaseRepository(DbContextOptions<BoxCarCatalogueDbContext> dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }

        public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken)
        {
            await using var _dbContext = new BoxCarCatalogueDbContext(_dbContextOptions);
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken)
        {
            await using var _dbContext = new BoxCarCatalogueDbContext(_dbContextOptions);
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> Exists(TId id, CancellationToken cancellationToken)
        {
            await using var _dbContext = new BoxCarCatalogueDbContext(_dbContextOptions);
            return await _dbContext.Set<T>().AnyAsync(e => e.Id.Equals(id), cancellationToken);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            await using var _dbContext = new BoxCarCatalogueDbContext(_dbContextOptions);
            return await _dbContext.Set<T>().ToListAsync(cancellationToken);
        }

        public async Task<T?> GetByIdAsync(TId id, CancellationToken cancellationToken)
        {
            await using var _dbContext = new BoxCarCatalogueDbContext(_dbContextOptions);
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetPagedAsync(int page, int pageSize, CancellationToken cancellationToken)
        {
            await using var _dbContext = new BoxCarCatalogueDbContext(_dbContextOptions);
            page = page < 0 ? 0 : page;
            pageSize = pageSize < 0 ? 100 : pageSize;
            pageSize = pageSize > 500 ? 500 : pageSize;
            return await _dbContext.Set<T>().Skip(page * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            await using var _dbContext = new BoxCarCatalogueDbContext(_dbContextOptions);
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
