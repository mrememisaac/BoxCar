using Admin.Core.Features;
using Admin.Core.Features.Chasis.AddChassis;
using Admin.Core.Features.Engines.AddEngine;
using Admin.Core.Features.Factories.AddFactory;
using Admin.Core.Features.Factories.AddWareHouse;
using Admin.Core.Features.OptionPacks.AddOptionPack;
using Admin.Core.Features.Vehicles.AddVehicle;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Core
{
    public static class CoreServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
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
            return services;
        }
    }
}
