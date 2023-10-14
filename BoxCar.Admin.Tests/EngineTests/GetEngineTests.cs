using BoxCar.Admin.Core.Features.Engines.GetEngine;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace BoxCar.Admin.Tests.EngineTests
{

    public class GetEngineTests : EngineTestsBase
    {

        [Fact]
        public async void Can_Get_Engine_By_Id()
        {
            var collection = await repository.GetAllAsync(cancellationToken);
            var factoryId = collection.Last().Id;

            var query = new GetEngineByIdQuery { Id = factoryId };
            var logger = new Mock<ILogger<GetEngineByIdQueryHandler>>();
            var validator = new GetEngineByIdQueryValidator();
            var handler = new GetEngineByIdQueryHandler(repository, logger.Object, validator, mapper);


            var factory = await handler.Handle(query, cancellationToken);

            factory.ShouldNotBeNull();
        }
    }
}