namespace BoxCar.Catalogue.Core.Features.Vehicles.GetVehicle
{
    public class OptionDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Value { get; set; } = null!;
        public int Price { get; set; }
    }
}
