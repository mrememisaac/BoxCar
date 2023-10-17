﻿using BoxCar.Catalogue.Domain;

namespace BoxCar.Catalogue.Core.Contracts.Persistence
{
    public interface IAsyncRepository<T, TId> where T : BaseEntity<TId>
    {
        Task<T> CreateAsync(T entity, CancellationToken cancellationToken);

        Task UpdateAsync(T entity, CancellationToken cancellationToken);

        Task DeleteAsync(T entity, CancellationToken cancellationToken);

        Task<T?> GetByIdAsync(TId id, CancellationToken cancellationToken);

        Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken);

        Task<IReadOnlyList<T>> GetPagedAsync(int page, int pageSize, CancellationToken cancellationToken);

        Task<bool> Exists(TId id, CancellationToken cancellationToken);
    }
}
