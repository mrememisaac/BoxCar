﻿using BoxCar.Admin.Domain;

namespace BoxCar.Admin.Core.Features.Vehicles.AddVehicle
{
    public class EngineDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public FuelType FuelType { get; set; }

        public IgnitionMethod IgnitionMethod { get; set; }

        public int Strokes { get; set; }

    }
}
