using BoxCar.Admin.Core.Contracts.Persistence;
using AutoMapper;
using BoxCar.Admin.Domain;
using Microsoft.Extensions.DependencyInjection;
using BoxCar.Admin.Core.Profiles;
using BoxCar.Admin.Core;
using BoxCar.Admin.Tests.Fakes.Repositories;

namespace BoxCar.Admin.Tests.FactoryTests
{
    public class FactoryTestsBase : EntityTestsBase<Factory>
    {
        protected readonly IAsyncRepository<Factory, Guid> repository;

        public FactoryTestsBase()
        {
            repository = new ListBasedFactoryRepository();
        }
    }
}