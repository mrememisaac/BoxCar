﻿using BoxCar.Admin.Core.Contracts.Persistence;
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
            optionPack1.AddOption(new Option(Guid.NewGuid(), "Color", "Black"));
            optionPack1.AddOption(new Option(Guid.NewGuid(), "Seat Material", "Fabric"));
            
            var optionPack2 = new OptionPack(Guid.NewGuid(), "Deluxe");
            optionPack2.AddOption(new Option(Guid.NewGuid(), "Color", "Gold"));
            optionPack2.AddOption(new Option(Guid.NewGuid(), "Seat Material", "Leather"));

            var engine1 = new Engine(Guid.NewGuid(), "Electric Car", FuelType.Electricity, IgnitionMethod.ElectricMotor, 0);
            var engine2 = new Engine(Guid.NewGuid(), "Diesel Car", FuelType.Diesel, IgnitionMethod.Compression, 0);

            var chassis1 = new Chassis(Guid.NewGuid(), "Simple Chassis", "This is the standard chassis ");
            var chassis2 = new Chassis(Guid.NewGuid(), "Enhanced Chassis", "This chassis has extra protection");

            var one = new Vehicle(Guid.NewGuid(), engine1, chassis1, optionPack1);
            var two = new Vehicle(Guid.NewGuid(), engine2, chassis2, optionPack2);
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
