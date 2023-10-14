using BoxCar.Admin.Core.Contracts.Persistence;
using AutoMapper;
using BoxCar.Admin.Domain;
using Microsoft.Extensions.DependencyInjection;
using BoxCar.Admin.Core.Profiles;
using BoxCar.Admin.Core;
using BoxCar.Admin.Tests.Fakes.Repositories;

namespace BoxCar.Admin.Tests.VehicleTests
{
    public class VehicleTestsBase : EntityTestsBase<Vehicle>
    {
        protected readonly IAsyncRepository<Vehicle, Guid> repository;
        protected readonly IAsyncRepository<Chassis, Guid> chassisRepository;
        protected readonly IAsyncRepository<Engine, Guid> enginesRepository;
        protected readonly IAsyncRepository<OptionPack, Guid> optionPacksRepository;

        public VehicleTestsBase()
        {
            repository = new ListBasedVehicleRepository();
            chassisRepository = new ListBasedChassisRepository();
            enginesRepository = new ListBasedEngineRepository();
            optionPacksRepository = new ListBasedOptionPackRepository();
        }
    }
}