using BoxCar.Catalogue.Core.Contracts.Persistence;
using BoxCar.Catalogue.Domain;

namespace BoxCar.Catalogue.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BoxCarDbContext _dbContext;
        private IAsyncRepository<Vehicle, Guid> _vehicleRepository;
        private IAsyncRepository<Chassis, Guid> _chassisRepository;
        private IAsyncRepository<Engine, Guid> _engineRepository;
        private IAsyncRepository<OptionPack, Guid> _optionPackRepository;
        private IAsyncRepository<Factory, Guid> _factoryRepository;
        private IAsyncRepository<WareHouse, Guid> _warehouseRepository;
        private IAsyncRepository<IntegrationEvent, Guid> _integrationEventRepository;

        public UnitOfWork(BoxCarDbContext databaseContext)
        {
            _dbContext = databaseContext;
        }

        public IAsyncRepository<Vehicle, Guid> VehicleRepository
        {
            get { return _vehicleRepository = _vehicleRepository ?? new BaseRepository<Vehicle, Guid>(_dbContext); }
        }


        public IAsyncRepository<Engine, Guid> EngineRepository
        {
            get { return _engineRepository = _engineRepository ?? new BaseRepository<Engine, Guid>(_dbContext); }
        }

        public IAsyncRepository<Factory, Guid> FactoryRepository
        {
            get { return _factoryRepository = _factoryRepository ?? new BaseRepository<Factory, Guid>(_dbContext); }
        }

        public IAsyncRepository<WareHouse, Guid> WareHouseRepository
        {
            get { return _warehouseRepository = _warehouseRepository ?? new BaseRepository<WareHouse, Guid>(_dbContext); }
        }

        public IAsyncRepository<Chassis, Guid> ChassisRepository
        {
            get { return _chassisRepository = _chassisRepository ?? new BaseRepository<Chassis, Guid>(_dbContext); }
        }

        public IAsyncRepository<OptionPack, Guid> OptionPackRepository
        {
            get { return _optionPackRepository = _optionPackRepository ?? new BaseRepository<OptionPack, Guid>(_dbContext); }
        }

        public IAsyncRepository<IntegrationEvent, Guid> IntegrationEventRepository
        {
            get { return _integrationEventRepository = _integrationEventRepository ?? new BaseRepository<IntegrationEvent, Guid>(_dbContext); }
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public void Rollback()
        {
            _dbContext.Dispose();
        }
    }
}
