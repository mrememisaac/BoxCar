namespace BoxCar.Admin.Core.Features.Vehicles.AddVehicle
{
    public class OptionDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Value { get; set; } = null!;
    }
}
