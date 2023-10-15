namespace BoxCar.Admin.Core.Features.Chasis.AddChassis
{
    public class AddChassisResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; }
        
        public int Price { get; set; }
    }
}
