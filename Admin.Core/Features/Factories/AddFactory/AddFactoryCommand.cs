using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Core.Features.Factories.AddFactory
{

    public class AddFactoryCommand : IRequest<Result<AddFactoryResponse>>
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public AddressDto Address { get; set; }
    }
}
