using BoxCar.Admin.Core.Contracts.Persistence;
using BoxCar.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxCar.Admin.Tests.Fakes.Repositories
{
    public class ListBasedWareHouseRepository : IAsyncRepository<WareHouse, Guid>
    {
        protected readonly List<WareHouse> context;

        public ListBasedWareHouseRepository()
        {
            context = new List<WareHouse>();
            var one = new WareHouse(Guid.NewGuid(), "North WareHouse", new Address("1 North Street", "Industrial Zone", "Durban", "12345", "South Africa"));
            var two = new WareHouse(Guid.NewGuid(), "South WareHouse", new Address("2 South Street", "Industrial District", "Johanessburg", "67890", "South Africa"));
            context.Add(one);
            context.Add(two);
        }

        public async Task<WareHouse> CreateAsync(WareHouse entity, CancellationToken cancellationToken)
        {
            context.Add(entity);
            return entity;
        }

        public async Task DeleteAsync(WareHouse entity, CancellationToken cancellationToken)
        {
            context.Remove(entity);
        }

        public async Task<IReadOnlyList<WareHouse>> GetAllAsync(CancellationToken cancellationToken)
        {
            return context;
        }

        public async Task<WareHouse?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return context.Find(f => f.Id == id);
        }

        public async Task<IReadOnlyList<WareHouse>> GetPagedAsync(int page, int pageSize, CancellationToken cancellationToken)
        {
            return context.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task UpdateAsync(WareHouse entity, CancellationToken cancellationToken)
        {
            var e = await GetByIdAsync(entity.Id, cancellationToken);
            context.Remove(e);
            context.Add(entity);
        }
    }
}
