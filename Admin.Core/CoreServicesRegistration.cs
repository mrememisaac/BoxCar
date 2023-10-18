using BoxCar.Admin.Core.Features;
using BoxCar.Admin.Core.Features.Chasis.AddChassis;
using BoxCar.Admin.Core.Features.Chasis.GetChassis;
using BoxCar.Admin.Core.Features.Engines.AddEngine;
using BoxCar.Admin.Core.Features.Engines.GetEngine;
using BoxCar.Admin.Core.Features.Factories.AddFactory;
using BoxCar.Admin.Core.Features.Factories.GetFactory;
using BoxCar.Admin.Core.Features.OptionPacks.AddOptionPack;
using BoxCar.Admin.Core.Features.OptionPacks.GetOptionPack;
using BoxCar.Admin.Core.Features.Vehicles.AddVehicle;
using BoxCar.Admin.Core.Features.Vehicles.GetVehicle;
using BoxCar.Admin.Core.Features.Warehouses.AddWareHouse;
using BoxCar.Admin.Core.Features.Warehouses.GetWareHouse;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BoxCar.Admin.Core
{
    public static class CoreServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetValue<string>("CacheSettings:RedisCache");
            });
            services.Add(ServiceDescriptor.Singleton<IDistributedCache, RedisCache>());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddSingleton<AddFactoryCommandValidator>();
            services.AddSingleton<AddWareHouseCommandValidator>();
            services.AddSingleton<AddVehicleCommandValidator>();
            services.AddSingleton<AddEngineCommandValidator>();
            services.AddSingleton<AddChassisCommandValidator>();
            services.AddSingleton<AddOptionPackCommandValidator>();
            services.AddSingleton<AddressDtoValidator>();
            services.AddSingleton<AddOptionDtoValidator>();
            services.AddSingleton<GetWareHouseByIdQueryValidator>();
            services.AddSingleton<GetVehicleByIdQueryValidator>();
            services.AddSingleton<GetOptionPackByIdQueryValidator>();
            services.AddSingleton<GetEngineByIdQueryValidator>();
            services.AddSingleton<GetChassisByIdQueryValidator>();
            services.AddSingleton<GetFactoryByIdQueryValidator>();
            return services;
        }
    }
}
