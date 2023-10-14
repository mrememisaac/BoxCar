namespace Admin.Core.Features.Chasis.AddChassis
{
    public class AddChassisDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; }
    }
}
