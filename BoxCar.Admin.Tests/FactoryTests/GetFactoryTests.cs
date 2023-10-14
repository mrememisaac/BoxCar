using AutoMapper;
using BoxCar.Admin.Core.Features.Factories.AddFactory;
using BoxCar.Admin.Core.Features.Factories.GetFactory;
using BoxCar.Admin.Domain;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BoxCar.Admin.Tests.FactoryTests
{

    public class GetFactoryTests : FactoryTestsBase
    {

        [Fact]
        public async void Can_Get_Factory_By_Id()
        {
            var collection = await repository.GetAllAsync(cancellationToken);
            var factoryId = collection.Last().Id;

            var query = new GetFactoryByIdQuery { Id = factoryId };
            var logger = new Mock<ILogger<GetFactoryByIdQueryHandler>>();
            var validator = new GetFactoryByIdQueryValidator();
            var handler = new GetFactoryByIdQueryHandler(repository, logger.Object, validator, mapper);


            var factory = await handler.Handle(query, cancellationToken);

            factory.ShouldNotBeNull();
        }
    }
}