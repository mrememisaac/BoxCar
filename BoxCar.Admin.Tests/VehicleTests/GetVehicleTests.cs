using BoxCar.Admin.Core.Features.Vehicles.GetVehicle;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace BoxCar.Admin.Tests.VehicleTests
{

    public class GetVehicleTests : VehicleTestsBase
    {

        [Fact]
        public async void Can_Get_Vehicle_By_Id()
        {
            var collection = await repository.GetAllAsync(cancellationToken);
            var factoryId = collection.Last().Id;

            var query = new GetVehicleByIdQuery { Id = factoryId };
            var logger = new Mock<ILogger<GetVehicleByIdQueryHandler>>();
            var validator = new GetVehicleByIdQueryValidator();
            var handler = new GetVehicleByIdQueryHandler(repository, logger.Object, validator, mapper, cache.Object);


            var factory = await handler.Handle(query, cancellationToken);

            factory.ShouldNotBeNull();
        }
    }
}