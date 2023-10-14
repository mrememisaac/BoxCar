using BoxCar.Admin.Core.Contracts.Persistence;
using AutoMapper;
using BoxCar.Admin.Domain;
using Microsoft.Extensions.DependencyInjection;
using BoxCar.Admin.Core.Profiles;
using BoxCar.Admin.Core;
using BoxCar.Admin.Tests.Fakes.Repositories;

namespace BoxCar.Admin.Tests.ChassisTests
{
    public class ChassisTestsBase : EntityTestsBase<Chassis>
    {
        protected readonly IAsyncRepository<Chassis, Guid> repository;

        public ChassisTestsBase()
        {
            repository = _serviceProvider.GetService<ListBasedChassisRepository>() ?? new ListBasedChassisRepository();
        }

    }
}