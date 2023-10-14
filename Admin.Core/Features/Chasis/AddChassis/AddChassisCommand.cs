using MediatR;

namespace Admin.Core.Features.Chasis.AddChassis
{
    public class AddChassisCommand : IRequest<Result<AddChassisResponse>>
    {
        public Guid Id { get; set; }

        public string Name { get; private set; } = null!;

        public string Description { get; private set; }
    }
}
