using BoxCar.Admin.Core.Features.Chasis.GetChassis;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace BoxCar.Admin.Tests.ChassisTests
{

    public class GetChassisTests : ChassisTestsBase
    {

        [Fact]
        public async void Can_Get_Chassis_By_Id()
        {
            var collection = await repository.GetAllAsync(cancellationToken);
            var factoryId = collection.Last().Id;

            var query = new GetChassisByIdQuery { Id = factoryId };
            var logger = new Mock<ILogger<GetChassisByIdQueryHandler>>();
            var validator = new GetChassisByIdQueryValidator();
            var handler = new GetChassisByIdQueryHandler(repository, logger.Object, validator, mapper, cache.Object);


            var factory = await handler.Handle(query, cancellationToken);

            factory.ShouldNotBeNull();
        }
    }
}