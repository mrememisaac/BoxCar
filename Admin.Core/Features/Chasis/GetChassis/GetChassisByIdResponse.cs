namespace BoxCar.Admin.Core.Features.Chasis.GetChassis
{
    public class GetChassisByIdResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = string.Empty;
    }
}
