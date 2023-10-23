using MediatR;

namespace BoxCar.Admin.Core.Features.Options.ListOptions
{
    public class GetOptionQuery : IRequest<GetOptionQueryResponse>
    {
        private int pageSize = 50;

        public int PageNumber { get; set; }

        public int PageSize { get => pageSize; set => pageSize = value > 50 ? 50 : value; }
    }
}
