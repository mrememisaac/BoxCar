using BoxCar.Admin.Core.Contracts.Persistence;
using BoxCar.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxCar.Admin.Tests.Fakes.Repositories
{
    public class ListBasedFactoryRepository : IAsyncRepository<Factory, Guid>
    {
        protected readonly List<Factory> context;

        public ListBasedFactoryRepository()
        {
            context = new List<Factory>();
            var factory1 = new Factory(Guid.NewGuid(), "North Factory", new Address("1 North Street", "Industrial Zone", "Durban", "12345", "South Africa"));
            var factory2 = new Factory(Guid.NewGuid(), "South Factory", new Address("2 South Street", "Industrial District", "Johanessburg", "67890", "South Africa"));
            context.Add(factory1);
            context.Add(factory2);
        }

        public async Task<Factory> CreateAsync(Factory entity, CancellationToken cancellationToken)
        {
            context.Add(entity);
            return entity;
        }

        public async Task DeleteAsync(Factory entity, CancellationToken cancellationToken)
        {
            context.Remove(entity);
        }

        public async Task<IReadOnlyList<Factory>> GetAllAsync(CancellationToken cancellationToken)
        {
            return context;
        }

        public async Task<Factory?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return context.Find(f => f.Id == id);
        }

        public async Task<IReadOnlyList<Factory>> GetPagedAsync(int page, int pageSize, CancellationToken cancellationToken)
        {
            return context.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task UpdateAsync(Factory entity, CancellationToken cancellationToken)
        {
            var e = await GetByIdAsync(entity.Id, cancellationToken);
            context.Remove(e);
            context.Add(entity);
        }
    }
}
