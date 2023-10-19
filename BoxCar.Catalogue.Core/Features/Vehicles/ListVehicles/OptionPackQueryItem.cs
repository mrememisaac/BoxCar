namespace BoxCar.Catalogue.Core.Features.Vehicles.ListVehicles
{
    public class OptionPackQueryItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;


        public List<OptionQueryItem> Options = new List<OptionQueryItem>();

    }
}
