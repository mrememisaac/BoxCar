using BoxCar.Admin.Core.Features.Warehouses.GetWareHouse;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace BoxCar.Admin.Tests.WareHouseTests
{

    public class GetWareHouseTests : WareHouseTestsBase
    {

        [Fact]
        public async void Can_Get_WareHouse_By_Id()
        {
            var collection = await repository.GetAllAsync(cancellationToken);
            var factoryId = collection.Last().Id;

            var query = new GetWareHouseByIdQuery { Id = factoryId };
            var logger = new Mock<ILogger<GetWareHouseByIdQueryHandler>>();
            var validator = new GetWareHouseByIdQueryValidator();
            var handler = new GetWareHouseByIdQueryHandler(repository, logger.Object, validator, mapper);


            var factory = await handler.Handle(query, cancellationToken);

            factory.ShouldNotBeNull();
        }
    }
}