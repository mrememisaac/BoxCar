using BoxCar.Catalogue.Domain;

namespace BoxCar.Catalogue.Core.Contracts.Persistence
{
    public interface IEngineRepository : IAsyncRepository<Engine, Guid>
    {

    }
}
