using BoxCar.Admin.Core.Contracts.Persistence;
using AutoMapper;
using BoxCar.Admin.Domain;
using Microsoft.Extensions.DependencyInjection;
using BoxCar.Admin.Core.Profiles;
using BoxCar.Admin.Core;
using BoxCar.Admin.Tests.Fakes.Repositories;

namespace BoxCar.Admin.Tests.EngineTests
{
    public class EngineTestsBase : EntityTestsBase<Engine>
    {
        protected readonly IAsyncRepository<Engine, Guid> repository;

        public EngineTestsBase()
        {
            repository = new ListBasedEngineRepository();
        }

    }
}