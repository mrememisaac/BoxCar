using MediatR;

namespace Admin.Core.Features.OptionPacks.GetOptionPack
{
    public class GetOptionPackByIdQuery : IRequest<GetOptionPackByIdResponse>
    {
        public Guid Id { get; set; }
    }
}
