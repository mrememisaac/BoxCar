using BoxCar.Admin.Core.Contracts.Persistence;
using BoxCar.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxCar.Admin.Tests.Fakes.Repositories
{
    public class ListBasedChassisRepository : IAsyncRepository<Chassis, Guid>
    {
        protected readonly List<Chassis> context;

        public ListBasedChassisRepository()
        {
            context = new List<Chassis>();
            var one = new Chassis(Guid.NewGuid(), "Simple Chassis", "This is the standard chassis ");
            var two = new Chassis(Guid.NewGuid(), "Enhanced Chassis", "This chassis has extra protection");
            context.Add(one);
            context.Add(two);
        }

        public async Task<Chassis> CreateAsync(Chassis entity, CancellationToken cancellationToken)
        {
            context.Add(entity);
            return entity;
        }

        public async Task DeleteAsync(Chassis entity, CancellationToken cancellationToken)
        {
            context.Remove(entity);
        }

        public async Task<IReadOnlyList<Chassis>> GetAllAsync(CancellationToken cancellationToken)
        {
            return context;
        }

        public async Task<Chassis?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return context.Find(f => f.Id == id);
        }

        public async Task<IReadOnlyList<Chassis>> GetPagedAsync(int page, int pageSize, CancellationToken cancellationToken)
        {
            return context.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task UpdateAsync(Chassis entity, CancellationToken cancellationToken)
        {
            var e = await GetByIdAsync(entity.Id, cancellationToken);
            context.Remove(e);
            context.Add(entity);
        }
    }
}
