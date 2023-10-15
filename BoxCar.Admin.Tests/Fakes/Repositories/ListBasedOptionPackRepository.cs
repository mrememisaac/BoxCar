using BoxCar.Admin.Core.Contracts.Persistence;
using BoxCar.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxCar.Admin.Tests.Fakes.Repositories
{
    public class ListBasedOptionPackRepository : IAsyncRepository<OptionPack, Guid>
    {
        protected readonly List<OptionPack> context;

        public ListBasedOptionPackRepository()
        {
            context = new List<OptionPack>();
            var one = new OptionPack(Guid.NewGuid(), "Standard");
            one.AddOption(new Option(Guid.NewGuid(), "Color", "Black", 500));
            one.AddOption(new Option(Guid.NewGuid(), "Seat Material", "Fabric", 600));
            var two = new OptionPack(Guid.NewGuid(), "Deluxe");
            two.AddOption(new Option(Guid.NewGuid(), "Color", "Gold", 700));
            two.AddOption(new Option(Guid.NewGuid(), "Seat Material", "Leather", 800));
            context.Add(one);
            context.Add(two);
        }

        public async Task<OptionPack> CreateAsync(OptionPack entity, CancellationToken cancellationToken)
        {
            context.Add(entity);
            return entity;
        }

        public async Task DeleteAsync(OptionPack entity, CancellationToken cancellationToken)
        {
            context.Remove(entity);
        }

        public async Task<IReadOnlyList<OptionPack>> GetAllAsync(CancellationToken cancellationToken)
        {
            return context;
        }

        public async Task<OptionPack?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return context.Find(f => f.Id == id);
        }

        public async Task<IReadOnlyList<OptionPack>> GetPagedAsync(int page, int pageSize, CancellationToken cancellationToken)
        {
            return context.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task UpdateAsync(OptionPack entity, CancellationToken cancellationToken)
        {
            var e = await GetByIdAsync(entity.Id, cancellationToken);
            context.Remove(e);
            context.Add(entity);
        }
    }
}
