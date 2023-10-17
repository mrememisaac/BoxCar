using BoxCar.Catalogue.Domain;

namespace BoxCar.Catalogue.Core.Contracts.Persistence
{
    public interface IChassisRepository : IAsyncRepository<Chassis, Guid>
    {

    }
}
