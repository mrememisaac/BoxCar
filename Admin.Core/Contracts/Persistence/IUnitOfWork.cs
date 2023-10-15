using BoxCar.Admin.Domain;

namespace BoxCar.Admin.Core.Contracts.Persistence
{
    public interface IUnitOfWork
    {
        IAsyncRepository<Vehicle, Guid> VehicleRepository { get; }
        IAsyncRepository<Engine, Guid> EngineRepository { get; }
        IAsyncRepository<Factory, Guid> FactoryRepository { get; }
        IAsyncRepository<WareHouse, Guid> WareHouseRepository { get; }
        IAsyncRepository<Chassis, Guid> ChassisRepository { get; }
        IAsyncRepository<OptionPack, Guid> OptionPackRepository { get; }
        IAsyncRepository<IntegrationEvent, Guid> IntegrationEventRepository { get; }

        void Commit();
        void Rollback();
    }
}
