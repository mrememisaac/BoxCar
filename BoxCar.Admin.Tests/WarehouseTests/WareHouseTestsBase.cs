using BoxCar.Admin.Core.Contracts.Persistence;
using AutoMapper;
using BoxCar.Admin.Domain;
using Microsoft.Extensions.DependencyInjection;
using BoxCar.Admin.Core.Profiles;
using BoxCar.Admin.Core;
using BoxCar.Admin.Tests.Fakes.Repositories;

namespace BoxCar.Admin.Tests.WareHouseTests
{
    public class WareHouseTestsBase : EntityTestsBase<WareHouse>
    {
        protected readonly IAsyncRepository<WareHouse, Guid> repository;

        public WareHouseTestsBase()
        {
            repository = new ListBasedWareHouseRepository();
        }
    }
}