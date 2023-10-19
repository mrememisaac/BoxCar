namespace BoxCar.Catalogue.Domain
{
    public class Vehicle : Entity
    {
        public string Name { get; set; } = null!;

        public Engine Engine { get; private set; }

        public Chassis Chassis { get; private set; }
        public OptionPack OptionPack { get; private set; }
        public int Price { get; private set; }

        public Vehicle(Guid id, Engine engine, Chassis chassis, OptionPack optionPack, int price)
        {
            Id = id == Guid.Empty ? throw new ArgumentNullException(nameof(id)) : id;
            Engine = engine ?? throw new ArgumentNullException(nameof(Engine));
            Chassis = chassis ?? throw new ArgumentNullException(nameof(chassis));
            OptionPack = optionPack ?? throw new ArgumentNullException(nameof(optionPack));
            Price = price;
        }

        private Vehicle()
        {
            
        }
    }
}