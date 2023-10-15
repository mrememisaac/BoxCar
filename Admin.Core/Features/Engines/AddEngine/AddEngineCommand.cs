using BoxCar.Admin.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BoxCar.Admin.Core.Features.Engines.AddEngine
{

    public class AddEngineCommand : IRequest<Result<AddEngineResponse>>
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public FuelType FuelType { get; set; }

        public IgnitionMethod IgnitionMethod { get; set; }

        public int Strokes { get; set; }
        public int Price { get; set; }
    }
}
