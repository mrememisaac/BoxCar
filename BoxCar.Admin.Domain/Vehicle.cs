namespace BoxCar.Admin.Domain
{
    public class Vehicle : Entity
    {
        public string Name { get; set; } = null!;

        public Guid EngineId { get; private set; }
        public Engine Engine { get; private set; }

        public Guid ChassisId { get; private set; }
        public Chassis Chassis { get; private set; }

        public Guid OptionPackId { get; private set; }
        public OptionPack OptionPack { get; private set; }
        public int Price { get; private set; }

        public Vehicle(Guid id, string name, Engine engine, Chassis chassis, OptionPack optionPack, int price)
        {
            Id = id == Guid.Empty ? throw new ArgumentNullException(nameof(id)) : id;
            Engine = engine ?? throw new ArgumentNullException(nameof(Engine));
            EngineId = engine.Id;
            Chassis = chassis ?? throw new ArgumentNullException(nameof(chassis));
            ChassisId = chassis.Id;
            OptionPack = optionPack ?? throw new ArgumentNullException(nameof(optionPack));
            OptionPackId = optionPack.Id;
            Price = price;
            ChangeName(name);
        }

        public Vehicle(Guid id, string name, Guid engineId, Guid chassisId, Guid optionPackId, int price)
        {
            Id = id == Guid.Empty ? throw new ArgumentNullException(nameof(id)) : id;
            EngineId = engineId == Guid.Empty ? throw new ArgumentNullException(nameof(engineId)) : engineId;
            ChassisId = chassisId == Guid.Empty ? throw new ArgumentNullException(nameof(chassisId)) : chassisId;
            OptionPackId = optionPackId == Guid.Empty ? throw new ArgumentNullException(nameof(optionPackId)) : optionPackId;
            Price = price;
            ChangeName(name);
        }

        public void ChangeName(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
        private Vehicle()
        {
            
        }
    }
}