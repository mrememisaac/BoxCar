using BoxCar.Admin.Core.Contracts.Identity;
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
using System.Reflection;

namespace BoxCar.Admin.Core
{
    public static class CoreServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetValue<string>("RedisConnectionString");
            });
            services.Add(ServiceDescriptor.Singleton<IDistributedCache, RedisCache>());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddScoped<AddFactoryCommandValidator>();
            services.AddScoped<AddWareHouseCommandValidator>();
            services.AddScoped<AddVehicleCommandValidator>();
            services.AddScoped<AddEngineCommandValidator>();
            services.AddScoped<AddChassisCommandValidator>();
            services.AddScoped<AddOptionPackCommandValidator>();
            services.AddScoped<AddressDtoValidator>();
            services.AddScoped<AddOptionDtoValidator>();
            services.AddScoped<GetWareHouseByIdQueryValidator>();
            services.AddScoped<GetVehicleByIdQueryValidator>();
            services.AddScoped<GetOptionPackByIdQueryValidator>();
            services.AddScoped<GetEngineByIdQueryValidator>();
            services.AddScoped<GetChassisByIdQueryValidator>();
            services.AddScoped<GetFactoryByIdQueryValidator>();
            services.AddTransient(typeof(IResult<>), typeof(Result<>));

            return services;
        }
    }
}
