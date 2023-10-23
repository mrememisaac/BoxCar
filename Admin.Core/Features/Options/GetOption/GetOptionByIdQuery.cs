using MediatR;

namespace BoxCar.Admin.Core.Features.Options.GetOption
{
    public class GetOptionByIdQuery : IRequest<GetOptionByIdResponse>
    {
        public Guid Id { get; set; }
    }
}
