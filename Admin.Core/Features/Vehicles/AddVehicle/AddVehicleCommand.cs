using BoxCar.Admin.Core.Features;
using BoxCar.Admin.Core.Features.Vehicles.AddVehicle;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BoxCar.Admin.Core.Features.Vehicles.AddVehicle
{

    public class AddVehicleCommand : IRequest<Result<AddVehicleResponse>>
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;
        
        public Guid ChassisId { get; set; }

        public Guid EngineId { get; set; }

        public Guid OptionPackId { get; set; }

        public int Price { get; set; }

    }
}
