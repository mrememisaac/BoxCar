using MediatR;

namespace BoxCar.Catalogue.Core.Features.OptionPacks.ListOptionPacks
{
    public class GetOptionPackQuery : IRequest<GetOptionPackQueryResponse>
    {
        private int pageSize = 50;

        public int PageNumber { get; set; }

        public int PageSize { get => pageSize; set => pageSize = value > 50 ? 50 : value; }
    }
}
