namespace BoxCar.Admin.Core.Features.Vehicles.AddVehicle
{
    public class ChassisDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = string.Empty;
        public int Price { get; set; }
    }
}
