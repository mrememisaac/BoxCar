namespace BoxCar.Admin.Domain
{
    public class Engine : Entity
    {
        public string Name { get; private set; } = null!;

        public FuelType FuelType { get; private set; }

        public IgnitionMethod IgnitionMethod { get; private set; }

        public int Strokes { get; private set; }

        public int Price { get; private set; }
        {
            Id = id == Guid.Empty ? throw new ArgumentNullException(nameof(id)) : id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            FuelType = fuelType;
            IgnitionMethod = ignitionMethod;
            Price = price;
            Strokes = strokes < 0 ? throw new ArgumentNullException(nameof(strokes)) : strokes;
        }

        public List<Vehicle> Vehicles { get; } = new();

    }
}