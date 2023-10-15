using BoxCar.Admin.Core.Features.Vehicles.AddVehicle;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace BoxCar.Admin.Tests.VehicleTests
{
    public class AddVehicleTests : VehicleTestsBase
    {

        [Fact]
        public async void Can_Add_Vehicle()
        {
            var optionPacks = await optionPacksRepository.GetAllAsync(CancellationToken.None);
            var engines = await enginesRepository.GetAllAsync(CancellationToken.None);
            var chassis = await chassisRepository.GetAllAsync(CancellationToken.None);
            var command = new AddVehicleCommand
            {
                Id = Guid.NewGuid(),
                Name = "East Vehicle",
                EngineId = engines.First().Id,
                ChassisId = chassis.First().Id,
                OptionPackId = optionPacks.First().Id,
            };
            var logger = new Mock<ILogger<AddVehicleCommandHandler>>();
            var validator = new AddVehicleCommandValidator(chassisRepository, enginesRepository, optionPacksRepository);
            var handler = new AddVehicleCommandHandler(mapper, repository, chassisRepository, enginesRepository, optionPacksRepository, logger.Object, validator, messageBus.Object);

            var collection = await repository.GetAllAsync(cancellationToken);
            var count = collection.Count();

            await handler.Handle(command, cancellationToken);

            collection.Count.ShouldBe(count + 1);
        }
    }
}