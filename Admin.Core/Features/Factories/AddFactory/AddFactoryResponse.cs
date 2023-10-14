namespace Admin.Core.Features.Factories.AddFactory
{
    public class AddFactoryResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public AddressDto Address { get; set; }
    }
}
