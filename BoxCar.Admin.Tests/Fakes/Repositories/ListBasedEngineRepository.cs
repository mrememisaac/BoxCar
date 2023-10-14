using BoxCar.Admin.Core.Contracts.Persistence;
using BoxCar.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxCar.Admin.Tests.Fakes.Repositories
{
    public class ListBasedEngineRepository : IAsyncRepository<Engine, Guid>
    {
        protected readonly List<Engine> context;

        public ListBasedEngineRepository()
        {
            context = new List<Engine>();
            var one = new Engine(Guid.NewGuid(), "Electric Car", FuelType.Electricity, IgnitionMethod.ElectricMotor, 0);
            var two = new Engine(Guid.NewGuid(), "Diesel Car", FuelType.Diesel, IgnitionMethod.Compression, 0);
            var three = new Engine(Guid.NewGuid(), "Gasoline Car", FuelType.Gasoline, IgnitionMethod.Spark, 0);
            context.Add(one);
            context.Add(two);
            context.Add(three);
        }

        public async Task<Engine> CreateAsync(Engine entity, CancellationToken cancellationToken)
        {
            context.Add(entity);
            return entity;
        }

        public async Task DeleteAsync(Engine entity, CancellationToken cancellationToken)
        {
            context.Remove(entity);
        }

        public async Task<IReadOnlyList<Engine>> GetAllAsync(CancellationToken cancellationToken)
        {
            return context;
        }

        public async Task<Engine?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return context.Find(f => f.Id == id);
        }

        public async Task<IReadOnlyList<Engine>> GetPagedAsync(int page, int pageSize, CancellationToken cancellationToken)
        {
            return context.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task UpdateAsync(Engine entity, CancellationToken cancellationToken)
        {
            var e = await GetByIdAsync(entity.Id, cancellationToken);
            context.Remove(e);
            context.Add(entity);
        }
    }
}
