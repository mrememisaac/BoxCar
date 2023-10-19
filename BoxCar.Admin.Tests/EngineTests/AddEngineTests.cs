using BoxCar.Admin.Core.Features.Engines.AddEngine;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace BoxCar.Admin.Tests.EngineTests
{
    public class AddEngineTests : EngineTestsBase
    {

        [Fact]
        public async void Can_Add_Engine()
        {
            var command = new AddEngineCommand
            {
                Id = Guid.NewGuid(),
                Name = "East Engine",
                FuelType = Domain.FuelType.Gasoline,
                IgnitionMethod = Domain.IgnitionMethod.HCCI,
                Strokes = 4,
                Price = 1500
            };
            var logger = new Mock<ILogger<AddEngineCommandHandler>>();
            var validator = new AddEngineCommandValidator();
            var handler = new AddEngineCommandHandler(mapper, repository, logger.Object, validator, messageBus.Object, configuration);

            var collection = await repository.GetAllAsync(cancellationToken);
            var count = collection.Count();

            await handler.Handle(command, cancellationToken);

            collection.Count.ShouldBe(count + 1);
        }
    }
}