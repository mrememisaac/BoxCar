using BoxCar.Admin.Core.Features.Chasis.AddChassis;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace BoxCar.Admin.Tests.ChassisTests
{
    public class AddChassisTests : ChassisTestsBase
    {

        [Fact]
        public async void Can_Add_Chassis()
        {
            var command = new AddChassisCommand
            {
                Id = Guid.NewGuid(),
                Name = "East Chassis",
                Description = "East Chassis is a great chassis designed in the East",
                Price = 2300
            };
            var logger = new Mock<ILogger<AddChassisCommandHandler>>();
            var validator = new AddChassisCommandValidator();
            var handler = new AddChassisCommandHandler(mapper, repository, logger.Object, validator, messageBus.Object, configuration);

            var collection = await repository.GetAllAsync(cancellationToken);
            var count = collection.Count();

            await handler.Handle(command, cancellationToken);

            collection.Count.ShouldBe(count + 1);
        }
    }
}