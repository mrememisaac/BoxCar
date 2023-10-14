using BoxCar.Admin.Core.Contracts.Persistence;
using AutoMapper;
using BoxCar.Admin.Domain;
using Microsoft.Extensions.DependencyInjection;
using BoxCar.Admin.Core.Profiles;
using BoxCar.Admin.Core;
using BoxCar.Admin.Tests.Fakes.Repositories;

namespace BoxCar.Admin.Tests
{
    public class EntityTestsBase<T> where T : Entity
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly CancellationToken cancellationToken;
        protected readonly IMapper mapper;

        public EntityTestsBase()
        {
            cancellationToken = new CancellationToken();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AddressProfiles>();
                cfg.AddProfile<ChassisProfiles>();
                cfg.AddProfile<EngineProfiles>();
                cfg.AddProfile<FactoryProfiles>();
                cfg.AddProfile<OptionProfiles>();
                cfg.AddProfile<OptionPackProfiles>();
                cfg.AddProfile<VehicleProfiles>();
                cfg.AddProfile<WareHouseProfiles>();
            });
            mapper = configurationProvider.CreateMapper();
            var services = new ServiceCollection();
            services.AddApplicationServices();
            services.AddScoped<IAsyncRepository<Vehicle, Guid>, ListBasedVehicleRepository>();
            services.AddScoped<IAsyncRepository<Chassis, Guid>, ListBasedChassisRepository>();
            services.AddScoped<IAsyncRepository<Engine, Guid>, ListBasedEngineRepository>();
            services.AddScoped<IAsyncRepository<Factory, Guid>, ListBasedFactoryRepository>();
            services.AddScoped<IAsyncRepository<WareHouse, Guid>, ListBasedWareHouseRepository>();
            services.AddScoped<IAsyncRepository<OptionPack, Guid>, ListBasedOptionPackRepository>();
            _serviceProvider = services.BuildServiceProvider();

        }
    }
}