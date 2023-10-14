using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Core.Features.Vehicles.AddVehicle
{

    public class AddVehicleCommand : IRequest<Result<AddVehicleCommand>>
    {
        public Guid Id { get; set; }

        public string Name { get; private set; } = null!;
        
        public Guid ChassisId { get; set; }

        public Guid EngineId { get; set; }

        public Guid OptionPackId { get; set; }
    }
}
