using BoxCar.Admin.Core.Features.Warehouses.AddWareHouse;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace BoxCar.Admin.Tests.WareHouseTests
{

    public class AddWareHouseTests : WareHouseTestsBase
    {

        [Fact]
        public async void Can_Add_WareHouse()
        {
            var command = new AddWareHouseCommand
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
                Name = "East WareHouse"
            };
            var logger = new Mock<ILogger<AddWareHouseCommandHandler>>();
            var validator = new AddWareHouseCommandValidator();
            var handler = new AddWareHouseCommandHandler(mapper, repository, logger.Object, validator);

            var collection = await repository.GetAllAsync(cancellationToken);
            var count = collection.Count();

            await handler.Handle(command, cancellationToken);

            collection.Count.ShouldBe(count + 1);
        }
    }
}