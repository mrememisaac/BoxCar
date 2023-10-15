using BoxCar.Catalogue.Domain;

namespace BoxCar.Catalogue.Core.Contracts.Persistence
{
    public interface IVehicleRepository : IAsyncRepository<Vehicle, Guid>
    {
        Task<Vehicle?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
