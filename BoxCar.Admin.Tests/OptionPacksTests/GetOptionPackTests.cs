using BoxCar.Admin.Core.Features.OptionPacks.GetOptionPack;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace BoxCar.Admin.Tests.OptionPackTests
{

    public class GetOptionPackTests : OptionPackTestsBase
    {

        [Fact]
        public async void Can_Get_OptionPack_By_Id()
        {
            var collection = await repository.GetAllAsync(cancellationToken);
            var factoryId = collection.Last().Id;

            var query = new GetOptionPackByIdQuery { Id = factoryId };
            var logger = new Mock<ILogger<GetOptionPackByIdQueryHandler>>();
            var validator = new GetOptionPackByIdQueryValidator();
            var handler = new GetOptionPackByIdQueryHandler(repository, logger.Object, validator, mapper, cache.Object);


            var factory = await handler.Handle(query, cancellationToken);

            factory.ShouldNotBeNull();
        }
    }
}