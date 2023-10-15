using BoxCar.Admin.Core.Features.OptionPacks.AddOptionPack;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace BoxCar.Admin.Tests.OptionPackTests
{
    public class AddOptionPackTests : OptionPackTestsBase
    {

        [Fact]
        public async void Can_Add_OptionPack()
        {
            var command = new AddOptionPackCommand
            {
                Id = Guid.NewGuid(),
                Name = "East OptionPack",
                Options = new List<AddOptionDto>
                {
                    new AddOptionDto { Id = Guid.NewGuid(), Name = "Color", Value = "Green" },
                    new AddOptionDto { Id = Guid.NewGuid(), Name = "Seat Materials", Value = "Fabric" }
                }
            };
            var logger = new Mock<ILogger<AddOptionPackCommandHandler>>();
            var validator = new AddOptionPackCommandValidator();
            var handler = new AddOptionPackCommandHandler(mapper, repository, logger.Object, validator, messageBus.Object);

            var collection = await repository.GetAllAsync(cancellationToken);
            var count = collection.Count();

            await handler.Handle(command, cancellationToken);

            collection.Count.ShouldBe(count + 1);
        }
    }
}