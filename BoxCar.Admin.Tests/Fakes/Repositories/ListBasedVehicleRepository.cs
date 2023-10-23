using BoxCar.Admin.Core.Contracts.Persistence;
using BoxCar.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxCar.Admin.Tests.Fakes.Repositories
{
    public class ListBasedVehicleRepository : IAsyncRepository<Vehicle, Guid>
    {
        protected readonly List<Vehicle> context;

        public ListBasedVehicleRepository()
        {
            context = new List<Vehicle>();

            var optionPack1 = new OptionPack(Guid.NewGuid(), "Standard");
            optionPack1.AddOption(new Option(Guid.NewGuid(), "Color", "Black", 500));
            optionPack1.AddOption(new Option(Guid.NewGuid(), "Seat Material", "Fabric", 600));
            
            var optionPack2 = new OptionPack(Guid.NewGuid(), "Deluxe");
            optionPack2.AddOption(new Option(Guid.NewGuid(), "Color", "Gold", 700));
            optionPack2.AddOption(new Option(Guid.NewGuid(), "Seat Material", "Leather",800));

            var engine1 = new Engine(Guid.NewGuid(), "Electric Car", FuelType.Electricity, IgnitionMethod.ElectricMotor, 0,2000);
            var engine2 = new Engine(Guid.NewGuid(), "Diesel Car", FuelType.Diesel, IgnitionMethod.Compression, 0, 4000);

            var chassis1 = new Chassis(Guid.NewGuid(), "Simple Chassis", "This is the standard chassis ", 1000);
            var chassis2 = new Chassis(Guid.NewGuid(), "Enhanced Chassis", "This chassis has extra protection", 2000);

            var one = new Vehicle(Guid.NewGuid(), "Vehicle 1", engine1, chassis1, optionPack1, 5000);
            var two = new Vehicle(Guid.NewGuid(), "Vehicle 2", engine2, chassis2, optionPack2, 6000);
            context.Add(one);
            context.Add(two);
        }

        public async Task<Vehicle> CreateAsync(Vehicle entity, CancellationToken cancellationToken)
        {
            context.Add(entity);
            return entity;
        }

        public async Task DeleteAsync(Vehicle entity, CancellationToken cancellationToken)
        {
            context.Remove(entity);
        }

        public async Task<IReadOnlyList<Vehicle>> GetAllAsync(CancellationToken cancellationToken)
        {
            return context;
        }

        public async Task<Vehicle?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return context.Find(f => f.Id == id);
        }

        public async Task<IReadOnlyList<Vehicle>> GetPagedAsync(int page, int pageSize, CancellationToken cancellationToken)
        {
            return context.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task UpdateAsync(Vehicle entity, CancellationToken cancellationToken)
        {
            var e = await GetByIdAsync(entity.Id, cancellationToken);
            context.Remove(e);
            context.Add(entity);
        }
    }
}
