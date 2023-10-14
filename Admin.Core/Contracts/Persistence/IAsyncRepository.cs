using BoxCar.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Core.Contracts.Persistence
{
    public interface IAsyncRepository<T, TId> where T : BaseEntity<TId>
    {
        Task<T> CreateAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        Task<T?> GetByIdAsync(TId id);

        Task<IReadOnlyList<T>> GetAllAsync();

        Task<IReadOnlyList<T>> GetPagedAsync(int page, int pageSize);
    }
}
