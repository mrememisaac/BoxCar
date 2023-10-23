using BoxCar.Admin.Domain;

namespace BoxCar.Admin.Core.Contracts.Persistence
{
    public interface IVehicleRepository : IAsyncRepository<Vehicle, Guid>
    {
        Task<Vehicle?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
