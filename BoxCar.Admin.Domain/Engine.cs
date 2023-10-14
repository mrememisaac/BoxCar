namespace BoxCar.Admin.Domain
{
    public class Engine : BaseEntity<Guid>
    {
        public string Name { get; private set; } = null!;

        public FuelType FuelType { get; private set; }

        public IgnitionMethod IgnitionMethod { get; private set; }

        public int Strokes { get; private set; }

        public Engine(string name, FuelType fuelType, IgnitionMethod ignitionMethod, int strokes)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            FuelType = fuelType;
            IgnitionMethod = ignitionMethod;
            Strokes = strokes < 0 ? throw new ArgumentNullException(nameof(strokes)) : strokes;
        }
    }
}