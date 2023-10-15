namespace BoxCar.Admin.Core.Features.Vehicles.GetVehicle
{
    public class OptionPackDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;


        public List<OptionDto> Options = new List<OptionDto>();

    }
}
