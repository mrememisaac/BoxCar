using BoxCar.Admin.Core.Contracts.Persistence;
using AutoMapper;
using BoxCar.Admin.Domain;
using Microsoft.Extensions.DependencyInjection;
using BoxCar.Admin.Core.Profiles;
using BoxCar.Admin.Core;
using BoxCar.Admin.Tests.Fakes.Repositories;

namespace BoxCar.Admin.Tests.FactoryTests
{
    public class FactoryTestsBase
    {
        protected readonly IMapper mapper;
        protected readonly IAsyncRepository<Factory, Guid> repository;
        protected readonly CancellationToken cancellationToken;
        protected readonly IServiceProvider _serviceProvider;

        public FactoryTestsBase()
        {
            cancellationToken = new CancellationToken();
            repository = new ListBasedFactoryRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ChassisProfiles>();
                cfg.AddProfile<EngineProfiles>();
                cfg.AddProfile<AddressProfiles>();
                cfg.AddProfile<OptionProfiles>();
                cfg.AddProfile<OptionPackProfiles>();
                cfg.AddProfile<VehicleProfiles>();
                cfg.AddProfile<WareHouseProfiles>();
                cfg.AddProfile<FactoryProfiles>();
            });
            mapper = configurationProvider.CreateMapper();

            var services = new ServiceCollection();
            services.AddApplicationServices();
            services.AddScoped<IAsyncRepository<Factory, Guid>, ListBasedFactoryRepository>();
            _serviceProvider = services.BuildServiceProvider();
        }

    }
}