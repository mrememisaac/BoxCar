using BoxCar.Admin.Core.Contracts.Persistence;
using AutoMapper;
using BoxCar.Admin.Domain;
using Microsoft.Extensions.DependencyInjection;
using BoxCar.Admin.Core.Profiles;
using BoxCar.Admin.Core;
using BoxCar.Admin.Tests.Fakes.Repositories;

namespace BoxCar.Admin.Tests.OptionPackTests
{
    public class OptionPackTestsBase : EntityTestsBase<OptionPack>
    {
        protected readonly IAsyncRepository<OptionPack, Guid> repository;

        public OptionPackTestsBase()
        {
            repository = new ListBasedOptionPackRepository();
        }
    }
}