using AutoMapper;
using BoxCar.Admin.Core.Features.Factories.AddFactory;
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

    public class AddFactoryTests : FactoryTestsBase
    {

        [Fact]
        public async void Can_Add_Factory()
        {
            var command = new AddFactoryCommand
            {
                Id = Guid.NewGuid(),
                Address = new Core.Features.AddressDto()
                {
                    City = "Test City",
                    Country = "Test Country",
                    PostalCode = "87659",
                    State = "Test State",
                    Street = "1 Test Street"
                },
                Name = "East Factory"
            };
            var logger = new Mock<ILogger<AddFactoryCommandHandler>>();
            var validator = new AddFactoryCommandValidator();
            var handler = new AddFactoryCommandHandler(mapper, repository, logger.Object, validator);

            var collection = await repository.GetAllAsync(cancellationToken);
            var count = collection.Count();

            await handler.Handle(command, cancellationToken);

            collection.Count.ShouldBe(count + 1);
        }
    }
}