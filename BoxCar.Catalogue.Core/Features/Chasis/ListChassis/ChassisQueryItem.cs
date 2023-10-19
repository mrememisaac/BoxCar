﻿namespace BoxCar.Catalogue.Core.Features.Chasis.ListChassis
{
    public class ChassisQueryItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = string.Empty;
        public int Price { get; set; }
    }
}
