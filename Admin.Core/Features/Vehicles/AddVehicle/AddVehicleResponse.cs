﻿namespace Admin.Core.Features.Vehicles.AddVehicle
{
    public class AddVehicleResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public Guid ChassisId { get; set; }

        public Guid EngineId { get; set; }

        public Guid OptionPackId { get; set; }
    }
}